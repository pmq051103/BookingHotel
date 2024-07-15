using BookingHotel.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingHotel.Data
{
    public class HotelContext : DbContext
    {
        public HotelContext(DbContextOptions<HotelContext> options) : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<RoomTypeDetail> RoomTypeDetails { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<RoomTypeImage> roomTypeImages { get; set; }
        public DbSet<ComboSale> ComboSales { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Menu> Menus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().ToTable("Room");
            modelBuilder.Entity<RoomType>().ToTable("RoomType");
            modelBuilder.Entity<RoomTypeDetail>().ToTable("RoomTypeDetail");
            modelBuilder.Entity<Service>().ToTable("Service");
            modelBuilder.Entity<RoomTypeImage>().ToTable("RoomTypeImage");
            modelBuilder.Entity<ComboSale>().ToTable("ComboSale");
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Request>().ToTable("Request");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Menu>().ToTable("Menu");
        }
    }
}
