# Arnavutköy Nöbetçi Eczane API ve Arayüz Projesi

Bu proje, Arnavutköy ilçesindeki güncel nöbetçi eczaneleri listeleyen, harita üzerinde konumlandıran ve kullanıcıların en yakın eczaneye en hızlı şekilde ulaşmasını sağlayan katmanlı mimariye (N-Tier Architecture) sahip bir **ASP.NET Core Web API** uygulamasıdır. Proje, arka planda güçlü bir veri yönetimi sunarken ön yüzünde modern bir web arayüzü ile desteklenmektedir.

---

## 🏗️ Proje Mimarisi ve Katmanlar

Proje, yazılım mühendisliği prensiplerine uygun olarak sürdürülebilir ve modüler bir katmanlı yapı üzerine kurulmuştur:

* **Web API:** Dış dünyadan gelen HTTP isteklerini karşılayan katman (Controllers, Middleware, Extensions ve statik arayüz dosyaları).
* **Core / Entities:** Veritabanı tablolarını ve temel veri modellerini barındıran katman.
* **Repository:** Veritabanı işlemlerinin (CRUD) Entity Framework Core ile gerçekleştirildiği veri erişim katmanı.
* **Services:** İş mantığının (Business Logic) yürütüldüğü servis katmanı.
* **Presentations:** Arayüz veya sunum katmanına ait yapıların yönetildiği alan.

---

## 🛠️ Kullanılan Teknolojiler

* **Backend:** .NET 8 / ASP.NET Core Web API, Entity Framework Core (ORM), LINQ
* **Veritabanı:** Microsoft SQL Server / LocalDB
* **Frontend / Arayüz:** HTML5, CSS3, JavaScript (Vanilla JS)
* **Kütüphaneler & Araçlar:** Leaflet.js (Harita entegrasyonu), FontAwesome (İkonlar), Swagger / OpenAPI (API Dokümantasyonu)

---

## 🚀 Projeyi Kendi Bilgisayarınızda Çalıştırma Adımları

Projeyi kendi bilgisayarınızda derlemek ve ayağa kaldırmak için aşağıdaki adımları sırasıyla takip edebilirsiniz:

### 1. Gereksinimler
Bilgisayarınızda aşağıdakilerin kurulu olduğundan emin olun:
* .NET SDK (8.0 veya üzeri)
* Visual Studio 2022 (veya VS Code)
* Microsoft SQL Server / LocalDB

### 2. Projeyi Klonlayın veya İndirin
Proje dosyalarını bilgisayarınıza indirdikten sonra Visual Studio ile **`Arnavutkoy Nobetci Eczane.slnx`** çözüm dosyasını (`.slnx`) açın.

### 3. Veritabanı Bağlantı Ayarını Yapın
`Web API/appsettings.json` dosyasını açın ve `ConnectionStrings` alanını kendi SQL Server sunucu adresinize göre güncelleyin:

```json
{
  "ConnectionStrings": {
    "sqlConnection": "Server=(localdb)\MSSQLLocalDB;Database=ArnavutkoyNobetciEczaneDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  }
}
```

### 4. Veritabanını Oluşturun (Migration)
Terminali (Package Manager Console veya sistem komut satırını) açarak Repository katmanının bulunduğu dizine gidin ve veritabanını veritabanı sunucunuza yansıtmak için şu komutu çalıştırın:

```bash
dotnet ef database update --project Repository --startup-project "Web API"
```

### 5. Projeyi Başlatın
Ana dizinde terminali açarak projeyi derleyin ve çalıştırın:

```bash
dotnet run --project "Web API"
```

Proje ayağa kalktıktan sonra tarayıcınıza `https://localhost:7002/swagger` yazarak API uç noktalarını (endpoints) Swagger üzerinden test edebilirsiniz.

---

🖥️ **Kullanıcı Arayüzünü Görüntüleme ve Kullanma**

Projenin harita ve liste barındıran görsel arayüzünü test etmek için:

1. Projeyi yukarıdaki adımlarla arka planda çalışır duruma getirin (API ayakta ve çalışır olmalıdır).
2. Bilgisayarınızda proje klasörünün içindeki `Web API/wwwroot/index.html` dosyasını açın.
3. Arayüz, arka planda çalışan API ile otomatik olarak haberleşecek ve Arnavutköy'deki nöbetçi eczaneleri hem liste halinde hem de interaktif harita üzerinde sizlere sunacaktır.
