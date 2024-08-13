using Fly_Up.Data;
using Fly_Up.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fly_Up.Controllers
{
    public class TicketsController : Controller
    {
        ApplicationDbContext _context;
        IWebHostEnvironment _webHostEnvironment;
        public TicketsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


		[HttpGet]
		public IActionResult GetIndexView(string? search)
		{
			ViewBag.Search = search;

			List<Ticket> tickets;

			if (string.IsNullOrWhiteSpace(search))
			{
				tickets = _context.Tickets.Include(t => t.Flight).ToList();
			}
			else
			{
				tickets = _context.Tickets
					.Where(d => d.Name.Contains(search) || d.Description.Contains(search))
					.Include(t => t.Flight)
					.ToList();
			}

			return View("Index", tickets);
		}

		[HttpGet]
        public IActionResult GetDetailsView(int id)
        {
            Ticket tic = _context.Tickets.Include(e => e.Flight).FirstOrDefault(e => e.Id == id);
            if (tic == null)
            {
                return NotFound();
            }
            else
            {
                return View("Details", tic);
            }
        }
        [HttpGet]
        public IActionResult GetCreateView()
        {

            ViewBag.AllFlights = _context.Flights.ToList();

            return View("Create");
        }

        [HttpPost]
        public IActionResult AddNew(Ticket tic, IFormFile? imageFormFile)
        {

			var flight = _context.Flights.Include(f => f.Tickets).FirstOrDefault(f => f.Id == tic.FlightId);
			if (tic.ReleaseDate > flight.TakeoffTime)
            {
                ModelState.AddModelError(string.Empty, $"Takeoff Time must be at least 7 days from now.");
            }
            if (ModelState.IsValid == true && flight.IsAvailable)
            {
                if (imageFormFile != null)
                {
                    string imgExtention = Path.GetExtension(imageFormFile.FileName);
                    Guid imgGuid = Guid.NewGuid();
                    string imgName = imgGuid + imgExtention;
                    string imgPath = "\\Images\\" + imgName;
                    tic.ImagePath = imgPath;
                    string imgFullPath = _webHostEnvironment.WebRootPath + imgPath;
                    FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
                    imageFormFile.CopyTo(imgFileStream);
                    imgFileStream.Dispose();
                }
                else
                {
                    tic.ImagePath = "\\Images\\no-image-icon.png";
                }
                _context.Tickets.Add(tic);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
            else
            {
                ViewBag.AllFlights = _context.Flights.ToList();

                return View("Create", tic);
            }
        }

        [HttpGet]
        public IActionResult GetEditView(int id)
        {
            Ticket ticket = _context.Tickets.FirstOrDefault(d => d.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.AllFlights = _context.Flights.ToList();

                return View("Edit", ticket);
            }
        }

        [HttpPost]
        public IActionResult EditCurrent(Ticket tic, IFormFile? imageFormFile)
        {
			var flight = _context.Flights.Include(f => f.Tickets).FirstOrDefault(f => f.Id == tic.FlightId);
			if (tic.ReleaseDate > flight.TakeoffTime)
            {
                ModelState.AddModelError(string.Empty, $"Takeoff Time must be at least 7 days from now.");
            }
            if (ModelState.IsValid == true && flight.IsAvailable)
            {
                if (imageFormFile != null)
                {
                    if (tic.ImagePath != "\\Images\\no-image-icon.png")
                    {
                        string oldImgFullPath = _webHostEnvironment.WebRootPath + tic.ImagePath;
                        System.IO.File.Delete(oldImgFullPath);
                    }
                    string imgExtention = Path.GetExtension(imageFormFile.FileName);
                    Guid imgGuid = Guid.NewGuid();
                    string imgName = imgGuid + imgExtention;
                    string imgPath = "\\Images\\" + imgName;
                    tic.ImagePath = imgPath;
                    string imgFullPath = _webHostEnvironment.WebRootPath + imgPath;
                    FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
                    imageFormFile.CopyTo(imgFileStream);
                    imgFileStream.Dispose();
                }
				var existingTicket = _context.Tickets.Find(tic.Id);
				_context.Entry(existingTicket).CurrentValues.SetValues(tic);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
            else
            {
                ViewBag.AllFlights = _context.Flights.ToList();

                return View("Edit", tic);
            }
        }

        [HttpGet]
        public IActionResult GetDeleteView(int id)
        {
            Ticket tic = _context.Tickets.Include(e => e.Flight).FirstOrDefault(e => e.Id == id);
            if (tic == null)
            {
                return NotFound();
            }
            else
            {
                return View("Delete", tic);
            }
        }

        [HttpPost]
        public IActionResult DeleteCurrent(int id)
        {

            Ticket ticket = _context.Tickets.FirstOrDefault(d => d.Id == id);

            if (ticket != null && ticket.ImagePath != "\\Images\\no-image-icon.png")
            {
                string imgFullPath = _webHostEnvironment.WebRootPath + ticket.ImagePath;
                System.IO.File.Delete(imgFullPath);
            }

            _context.Tickets.Remove(ticket);
            _context.SaveChanges();
            return RedirectToAction("GetIndexView");


        }
        

        
    }
}
