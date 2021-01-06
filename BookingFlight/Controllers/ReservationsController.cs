using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingFlight.Data;
using BookingFlight.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace BookingFlight.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly FlightContext _context;
        private readonly ILogger _logger;

        public ReservationsController(FlightContext context, ILogger<FlightTicketsController> logger)
        {
            _context = context;
            _logger = logger;
        }
        [Authorize(Policy = "writepolicy")]
        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Log message in the Reservations/Index() method");
            var flightContext = _context.Reservations.Include(r => r.FlightTicket);
            return View(await flightContext.ToListAsync());
        }
        [Authorize(Policy = "readpolicy")]
        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            _logger.LogInformation("Log message in the Reservations/Details() method");
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.FlightTicket)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }
        [Authorize(Policy = "readpolicy")]
        // GET: Reservations/Create
        public IActionResult Create()
        {
            _logger.LogInformation("Log message in the Reservations/Create() method");
            ViewData["FlightTicketID"] = new SelectList(_context.FlightTickets, "FlightTicketID", "FlightNumber");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,FlightTicketID")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                _logger.LogInformation("added booking");
                await _context.SaveChangesAsync();
                var ticket = (from f in _context.Reservations
                              where f.Id == reservation.Id
                              select f).First();
                return RedirectToAction("Edit","Reservations", ticket);
            }
            ViewData["FlightTicketID"] = new SelectList(_context.FlightTickets, "FlightTicketID", "FlightTicketID", reservation.FlightTicketID);
            return View(reservation);
        }
        [Authorize(Policy = "readpolicy")]
        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            _logger.LogInformation("Log message in the Reservations/Edit() method");
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["FlightTicketID"] = new SelectList(_context.FlightTickets, "FlightTicketID", "FlightNumber", reservation.FlightTicketID);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,FlightTicketID")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    _logger.LogInformation("Edited booking");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                var ticket = (from f in _context.Reservations
                              where f.Id == reservation.Id
                              select f).First();
                return RedirectToAction("Details", "Reservations", ticket);
                //return RedirectToAction(nameof(Index));
            }
            ViewData["FlightTicketID"] = new SelectList(_context.FlightTickets, "FlightTicketID", "FlightTicketID", reservation.FlightTicketID);
            return View(reservation);
        }
        [Authorize(Policy = "writepolicy")]
        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            _logger.LogInformation("Log message in the Reservations/Delete() method");
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.FlightTicket)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(reservation);
            _logger.LogInformation("Deleted booking");
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
