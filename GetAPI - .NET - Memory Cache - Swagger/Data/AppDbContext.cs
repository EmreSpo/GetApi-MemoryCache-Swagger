using Microsoft.EntityFrameworkCore;

namespace YourNamespace.Data
{
    /// <summary>
    /// Veritabanı bağlamını temsil eder
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// AppDbContext sınıfının bir örneğini oluştur
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
        /// Model yapılandırmasını tanımla
        /// </summary>
        /// <param name="modelBuilder">Model yapılandırıcı</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Company konfigürasyonu
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                // Diğer konfigürasyonlar ekle
            });

            // Geo konfigürasyonu
            modelBuilder.Entity<Geo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Lat).IsRequired();
                entity.Property(e => e.Lng).IsRequired();
                // Diğer konfigürasyonlar eklenebilir
            });

            // Address konfigürasyonu
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Geo)
                      .WithOne()
                      .HasForeignKey<Address>(e => e.Id);
                // Diğer konfigürasyonlar eklenebilir
            });

            // User konfigürasyonu
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Address)
                      .WithOne()
                      .HasForeignKey<User>(e => e.Id);
                entity.HasOne(e => e.Company)
                      .WithOne()
                      .HasForeignKey<User>(e => e.Id);
                // Diğer konfigürasyonlar eklenebilir
            });
        }
    }
}
