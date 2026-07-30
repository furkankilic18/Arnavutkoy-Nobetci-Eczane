using Entities.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.EF_Core
{
    public class RepositoryContext : DbContext
    {

        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Pharmacy> Pharmacy { get; set; }
        public DbSet<Duty> Duties { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Örnek: İlçe tablosundaki CityId, City tablosuyla ilişkilidir (EF Core bunu genelde otomatik anlar ama açıkça yazmak iyidir)
            modelBuilder.Entity<District>()
                .HasOne(d => d.City)
                .WithMany(c => c.Districts)
                .HasForeignKey(d => d.CityId);

            modelBuilder.Entity<Pharmacy>()
                .HasOne(p => p.District)
                .WithMany(d => d.Pharmacies)
                .HasForeignKey(p => p.DistrictId);

            modelBuilder.Entity<Duty>()
                .HasOne(d => d.Pharmacy)
                .WithMany(p => p.Duties)
                .HasForeignKey(d => d.PharmacyId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
