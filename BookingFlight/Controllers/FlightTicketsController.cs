using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingFlight.Data;
using BookingFlight.Models;

namespace BookingFlight.Controllers
{
    public class FlightTicketsController : Controller
    {
        private readonly FlightContext _context;

        public FlightTicketsController(FlightContext context)
        {
            _context = context;
        }

        // GET: FlightTickets
        public async Task<IActionResult> Index()
        {
            return View(await _context.FlightTickets.ToListAsync());
        }

        // GET: FlightTickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightTicket = await _context.FlightTickets
                .FirstOrDefaultAsync(m => m.FlightTicketID == id);
            if (flightTicket == null)
            {
                return NotFound();
            }

            return View(flightTicket);
        }

        // GET: FlightTickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FlightTickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FlightTicketID,FlightNumber,FlightDestination")] FlightTicket flightTicket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flightTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(flightTicket);
        }

        // GET: FlightTickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightTicket = await _context.FlightTickets.FindAsync(id);
            if (flightTicket == null)
            {
                return NotFound();
            }
            return View(flightTicket);
        }

        // POST: FlightTickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FlightTicketID,FlightNumber,FlightDestination")] FlightTicket flightTicket)
        {
            if (id != flightTicket.FlightTicketID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flightTicket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightTicketExists(flightTicket.FlightTicketID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(flightTicket);
        }

        // GET: FlightTickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightTicket = await _context.FlightTickets
                .FirstOrDefaultAsync(m => m.FlightTicketID == id);
            if (flightTicket == null)
            {
                return NotFound();
            }

            return View(flightTicket);
        }

        // POST: FlightTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flightTicket = await _context.FlightTickets.FindAsync(id);
            _context.FlightTickets.Remove(flightTicket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightTicketExists(int id)
        {
            return _context.FlightTickets.Any(e => e.FlightTicketID == id);
        }
    }
}
