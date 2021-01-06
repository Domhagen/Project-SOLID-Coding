using BookingFlight.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingFlight.Data
{
    public class FlightContext : DbContext
    {
        public FlightContext (DbContextOptions<FlightContext> options)
            : base(options)
        {

        }

        public DbSet<FlightTicket> FlightTickets { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
