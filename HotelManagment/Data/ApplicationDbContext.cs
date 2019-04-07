using HotelManagment.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HotelManagment.Models;

namespace HotelManagment.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<HotelManagment.Data.Entities.Client> Client { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<RoomReservation> RoomReservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reservation>().HasMany(r => r.RoomReservations);
            modelBuilder.Entity<Client>().HasMany(r => r.Reservations);
        }
    }
}
