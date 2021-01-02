using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingFlight.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        public int FlightTicketID { get; set; }
        public FlightTicket FlightTicket { get; set; }
    }
}
