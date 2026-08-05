// ---- Güncel tarihi üst alana yazdır ----
const dateOptions = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
document.getElementById('date-text').innerText = new Date().toLocaleDateString('tr-TR', dateOptions);

// ---- Haritayı Arnavutköy merkezli başlat ----
const map = L.map('map', { zoomControl: true }).setView([41.1856, 28.7386], 13);
L.tileLayer('https://{s}.basemaps.cartocdn.com/rastertiles/voyager/{z}/{x}/{y}{r}.png', {
  attribution: '&copy; OpenStreetMap contributors &copy; CARTO'
}).addTo(map);

const listContainer = document.getElementById('pharmacy-list');

// ---- Backend API adresin (7002 portu) ----
const apiUrl = 'https://localhost:7002/api/pharmacies/todays-duties?city=İstanbul&district=Arnavutköy';
let markers = [];

function clearMarkers(){
  markers.forEach(m => map.removeLayer(m));
  markers = [];
}

function renderList(data){
  listContainer.innerHTML = '';

  if(!data || data.length === 0){
    listContainer.innerHTML = `
      <div class="state-msg">
        <i class="fa-solid fa-circle-info"></i>
        Bugün için ilçemizde nöbetçi eczane kaydı bulunamadı.
      </div>`;
    return;
  }

  clearMarkers();

  data.forEach((pharmacy, idx) => {
    let lat = 41.1856, lng = 28.7386;
    if(pharmacy.location){
      const coords = pharmacy.location.split(',');
      lat = parseFloat(coords[0].trim());
      lng = parseFloat(coords[1].trim());
    }

    // ---- Kart ----
    // DÜZELTME: ${pharmacy.name} yanındaki "Eczanesi" kelimesi kaldırıldı.
    const card = document.createElement('div');
    card.className = 'pharmacy-card';
    card.dataset.idx = idx;
    card.innerHTML = `
      <div class="card-top">
        <h3><span class="rx-icon"><i class="fa-solid fa-mortar-pestle"></i></span>${pharmacy.name}</h3>
        <span class="open-tag">NÖBETÇİ</span>
      </div>
      <div class="card-row"><i class="fa-solid fa-phone"></i><a href="tel:${pharmacy.phone.replace(/\s/g,'')}">${pharmacy.phone}</a></div>
      <div class="card-row"><i class="fa-solid fa-location-dot"></i><span>${pharmacy.address}</span></div>
      <button class="route-btn"><i class="fa-solid fa-map-location-dot"></i> Google Haritalar'da Git</button>
    `;

    card.querySelector('.route-btn').addEventListener('click', (e) => {
      e.stopPropagation();
      window.open(`https://www.google.com/maps/dir/?api=1&destination=${lat},${lng}`, '_blank');
    });

    // ---- Yeni Yuvarlak İçinde "E" Marker'ı ----
    const customDivIcon = L.divIcon({
      className: 'e-marker-container',
      html: "<div class='e-marker'>E</div>",
      iconSize: [32, 32],
      iconAnchor: [16, 16],
      popupAnchor: [0, -16]
    });

    const marker = L.marker([lat, lng], { icon: customDivIcon }).addTo(map);
    
    // Popup İçeriği
    // DÜZELTME: Harita bilgi balonundaki ${pharmacy.name} yanındaki "Eczanesi" kelimesi kaldırıldı.
    marker.bindPopup(`
      <div style="text-align:center;font-family:'Manrope',sans-serif;">
        <strong style="color:#0a2647;font-size:14px;">${pharmacy.name}</strong><br>
        <span style="color:#5c7086;font-size:12.5px;">${pharmacy.phone}</span><br>
        <a href="https://www.google.com/maps/dir/?api=1&destination=${lat},${lng}" target="_blank"
           style="display:inline-block;margin-top:8px;background:#1a365d;color:#fff;padding:6px 12px;border-radius:6px;font-size:11.5px;font-weight:700;text-decoration:none;">Yol Tarifi</a>
      </div>
    `);
    markers.push(marker);

    // Karta tıklanınca haritada göster
    card.addEventListener('click', () => {
      document.querySelectorAll('.pharmacy-card').forEach(c => c.classList.remove('active'));
      card.classList.add('active');
      map.flyTo([lat, lng], 16, { animate: true, duration: 1.3 });
      marker.openPopup();
    });

    listContainer.appendChild(card);
  });
}

// API'den veri çek. Hata varsa (örneğin API kapalıysa) bakım mesajını göster.
fetch(apiUrl)
  .then(response => {
      if(!response.ok) { throw new Error('Sunucu Yanıt Vermedi'); }
      return response.json();
  })
  .then(data => renderList(data))
  .catch(error => {
    console.error("API Bağlantı Hatası:", error);
    // API kapalıyken gösterilecek Kırmızı Bakım Mesajı
    listContainer.innerHTML = `
      <div class="state-msg" style="background-color: #f8d7da; border-color: #f5c2c7; color: #842029; padding: 25px;">
        <i class="fa-solid fa-server" style="color: #842029; font-size: 28px;"></i>
        <strong style="font-size: 16px; display:block; margin-bottom:8px;">Hizmet Kesintisi</strong>
        Şu an sunucu bakımı veya teknik bir bağlantı sorunu nedeniyle nöbetçi eczane verilerine ulaşılamamaktadır.<br>Lütfen daha sonra tekrar deneyiniz.
      </div>`;
  });