using Fly_Up.Models;
using Microsoft.EntityFrameworkCore;

namespace Fly_Up.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Enroll> Users { get; set; }
     

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

    }
}
