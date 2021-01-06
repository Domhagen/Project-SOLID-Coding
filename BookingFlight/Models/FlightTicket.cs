using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingFlight.Models
{
    public class FlightTicket
    {
        public int FlightTicketID { get; set; }
        public int FlightNumber { get; set; }
        public string FlightDestination { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
