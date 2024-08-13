using Fly_Up.Data;
using Fly_Up.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;

public class BookingController : Controller
{
    ApplicationDbContext _context;
    IWebHostEnvironment _webHostEnvironment;

    public BookingController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
	{
		_context = context;
		_webHostEnvironment = webHostEnvironment;
	}

    [HttpGet]
    public IActionResult GetIndexView(string? search)
    {
        ViewBag.Search = search;

        if (string.IsNullOrEmpty(search) == true)
        {
            return View("Index", _context.Bookings.ToList());
        }
        else
        {
            return View("Index", _context.Bookings.Where(d => d.PassengerName.Contains(search) || d.PassengerEmail.Contains(search)).ToList());

        }
    }
	[HttpGet]
	public IActionResult GetCreateView()
	{
		ViewBag.AllFlights = _context.Flights.ToList();
		ViewBag.AllTickets = _context.Tickets.ToList();
		return View("BookingForm");
		
	}
    [HttpPost]
    public IActionResult BookFlight(Booking model)
    {
        var flight = _context.Flights.Include(f => f.Tickets ).FirstOrDefault(f => f.Id == model.FlightId);
        if (flight == null || !flight.IsAvailable)
        {
            ModelState.AddModelError("", "Sorry, the selected flight is not available for booking.");
            ViewBag.AllFlights = _context.Flights.ToList();
            ViewBag.AllTickets = _context.Tickets.ToList();
            return View("BookingForm", model);
        }

        var ticket = _context.Tickets.FirstOrDefault(t => t.Id == model.TicketId && t.FlightId == model.FlightId);

        if (ticket == null)
        {
            ModelState.AddModelError("", "Invalid ticket selection.");
            ViewBag.AllFlights = _context.Flights.ToList();
            ViewBag.AllTickets = _context.Tickets.ToList();
            return View("BookingForm", model);
        }

        

        ticket.Flight = flight;

        _context.Bookings.Add(model);
        _context.SaveChanges();

        return RedirectToAction("GetIndexView");
    }





}
