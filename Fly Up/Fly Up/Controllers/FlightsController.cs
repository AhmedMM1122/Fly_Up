using Fly_Up.Data;
using Fly_Up.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fly_Up.Controllers
{
    public class FlightsController : Controller
    {
        ApplicationDbContext _context;
        IWebHostEnvironment _webHostEnvironment;
        public FlightsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
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
                return View("Index", _context.Flights.ToList());
            }
            else
            {
                return View("Index", _context.Flights.Where(d => d.Name.Contains(search) || d.Description.Contains(search)).ToList());

            }
        }
        [HttpGet]
        public IActionResult CheckAvailabilty(string? search)
        {
			ViewBag.Search = search;

			if (string.IsNullOrEmpty(search) == true)
			{
				return View("Avail", _context.Flights.Where(t => t.IsAvailable).ToList());
			}
			else
			{
				return View("Avail", _context.Flights.Where(d => d.Name.Contains(search) || d.Description.Contains(search)&&d.IsAvailable).ToList());

			}
		}
        [HttpGet]
        public IActionResult GetCreateView()
        {
            return View("Create");
        }
        [HttpPost]
        public IActionResult AddNew(Flight fli, IFormFile? imageFormFile)
        {

            DateTime minAllowedTakeoffTime = DateTime.Now.AddDays(7);

            if (fli.TakeoffTime < minAllowedTakeoffTime)
            {
                ModelState.AddModelError(string.Empty, $"Takeoff Time must be at least 7 days from now. Minimum allowed date is {minAllowedTakeoffTime:d}.");
            }
            if (ModelState.IsValid == true)
            {
                if (imageFormFile != null)
                {
                    string imgExtention = Path.GetExtension(imageFormFile.FileName);
                    Guid imgGuid = Guid.NewGuid();
                    string imgName = imgGuid + imgExtention;
                    string imgPath = "\\img\\" + imgName;
                    fli.ImagePath = imgPath;
                    string imgFullPath = _webHostEnvironment.WebRootPath + imgPath;
                    FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
                    imageFormFile.CopyTo(imgFileStream);
                    imgFileStream.Dispose();
                }
                else
                {
                    fli.ImagePath = "\\Images\\no-image-icon.png";
                }
                _context.Flights.Add(fli);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
            else
            {
                return View("Create", fli);
            }
        }

        [HttpGet]
        public IActionResult GetEditView(int id)
        {
            Flight flight = _context.Flights.FirstOrDefault(d => d.Id == id);
            if (flight == null)
                return NotFound();
            return View("Edit", flight);
        }

        [HttpPost]
        public IActionResult EditCurrent(Flight fli, IFormFile? imageFormFile)
        {
            DateTime minAllowedTakeoffTime = DateTime.Now.AddDays(7);

            if (fli.TakeoffTime < minAllowedTakeoffTime)
            {
                ModelState.AddModelError(string.Empty, $"Takeoff Time must be at least 7 days from now. Minimum allowed date is {minAllowedTakeoffTime:d}.");
            }
            if (ModelState.IsValid == true)
            {
                if (imageFormFile != null)
                {
                    if (fli.ImagePath != "\\Images\\no-image-icon.png")
                    {
                        string oldImgFullPath = _webHostEnvironment.WebRootPath + fli.ImagePath;
                        System.IO.File.Delete(oldImgFullPath);
                    }
                    string imgExtention = Path.GetExtension(imageFormFile.FileName);
                    Guid imgGuid = Guid.NewGuid();
                    string imgName = imgGuid + imgExtention;
                    string imgPath = "\\Images\\" + imgName;
                    fli.ImagePath = imgPath;
                    string imgFullPath = _webHostEnvironment.WebRootPath + imgPath;
                    FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
                    imageFormFile.CopyTo(imgFileStream);
                    imgFileStream.Dispose();
                }

                _context.Flights.Update(fli);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
            else
            {
                return View("Edit", fli);
            }
        }

        [HttpGet]
        public IActionResult GetDeleteView(int id)
        {
            Flight fli = _context.Flights.Include(d => d.Tickets).FirstOrDefault(d => d.Id == id);
            ViewBag.Currentfli = fli;

            if (fli == null)
                return NotFound();
            return View("Delete", fli);
        }

        [HttpPost]
        public IActionResult DeleteCurrent(int id)
        {
			
			Flight flight = _context.Flights.Find(id);


            if (flight != null && flight.ImagePath != "\\Images\\no-image-icon.png")
            {
                string imgFullPath = _webHostEnvironment.WebRootPath + flight.ImagePath;
                System.IO.File.Delete(imgFullPath);
            }
			

			_context.Flights.Remove(flight);
			_context.SaveChanges();

            return RedirectToAction("GetIndexView");
        }
       

        
    }
}
