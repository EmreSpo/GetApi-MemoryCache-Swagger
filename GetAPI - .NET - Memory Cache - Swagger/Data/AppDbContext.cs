using Microsoft.EntityFrameworkCore;
using YourNamespace.Models; // Address, Company, Geo ve User sınıflarının burada tanımlandığından emin olun

namespace YourNamespace.Data
{
    /// <summary>
    /// Veritabanı bağlamını temsil eder
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// AppDbContext sınıfının bir örneğini oluşturur
        /// </summary>
        /// <param name="options">Veritabanı bağlamı seçenekleri</param>
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Kullanıcılar tablosu.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Şirketler tablosu.
        /// </summary>
        public DbSet<Company> Companies { get; set; }

        /// <summary>
        /// Adresler tablosu.
        /// </summary>
        public DbSet<Address> Addresses { get; set; }

        /// <summary>
        /// Coğrafi bilgiler tablosu.
        /// </summary>
        public DbSet<Geo> Geos { get; set; }

        /// <summary>
        /// Model yapılandırmasını tanımlar
        /// </summary>
        /// <param name="modelBuilder">Model yapılandırıcı</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User konfigürasyonu
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Address)
                      .WithMany(a => a.Users)
                      .HasForeignKey(e => e.AddressId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Company)
                      .WithMany(c => c.Users)
                      .HasForeignKey(e => e.CompanyId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Address konfigürasyonu
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Street).IsRequired();
                entity.Property(e => e.City).IsRequired();
                entity.Property(e => e.Zipcode).IsRequired();

                // Mapping for Geo
                entity.HasOne(a => a.Geo)
                      .WithOne(g => g.Address)
                      .HasForeignKey<Geo>(g => g.AddressId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Company konfigürasyonu
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });

            // Geo konfigürasyonu
            modelBuilder.Entity<Geo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Lat).IsRequired();
                entity.Property(e => e.Lng).IsRequired();
            });
        }
    }
}
