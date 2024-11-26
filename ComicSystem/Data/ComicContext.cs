using Microsoft.EntityFrameworkCore;
using ComicSystem.Models;

namespace ComicSystem.Data
{
    public class ComicContext : DbContext
    {
        public ComicContext(DbContextOptions<ComicContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<ComicBook> ComicBooks { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<RentalDetail> RentalDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Định nghĩa độ chính xác cho kiểu dữ liệu decimal
            modelBuilder.Entity<ComicBook>()
                .Property(c => c.PricePerDay)
                .HasPrecision(18, 2);

            modelBuilder.Entity<RentalDetail>()
                .Property(r => r.PricePerDay)
                .HasPrecision(18, 2);
        }
    }
}
