using HotelApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace HotelApp.DataAccess
{
    public class HotelAppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=HotelAppDb;Trusted_Connection=True; TrustServerCertificate=True");
        }
        public DbSet<Client> Client { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
    }
    
}
