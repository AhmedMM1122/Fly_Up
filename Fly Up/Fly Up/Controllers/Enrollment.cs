using Fly_Up.Models;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Fly_Up.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Fly_Up.Controllers
{
    public class Enrollment : Controller
    {
        ApplicationDbContext _context;
        IWebHostEnvironment _webHostEnvironment;
        public Enrollment(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        // POST: Enroll/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Enroll enroll)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(enroll);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View("Index",enroll);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        

        public IActionResult Login(Enroll model)
        {
            ModelState.Remove("FirstName");
            ModelState.Remove("LastName");
            ModelState.Remove("Confirmpwd");
            ModelState.Remove("Gender");
            ModelState.Remove("PhoneNumber");
            ModelState.Remove("SecurityAnwser");
            ModelState.Remove("Enrollsinfo");
            ModelState.Remove("Is_Deleted");

            if (ModelState.IsValid)
            {
                Enroll end = _context.Users.FirstOrDefault(d => d.Email == model.Email && d.Password == model.Password);

                if (end != null)
                {
                    
                    return RedirectToAction("Index", "Home"); // Redirect to the home page after successful login
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password");
                    
                }
            }

            return View();
        }

        

	}


}

