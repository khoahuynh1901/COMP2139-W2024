using GBC_Travel_Group53.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace GBC_Travel_Group53.Controllers
{
    public class Customers : Controller
    {
        private readonly DatabaseContext _ctx;
        public Customers(DatabaseContext ctx) {
            _ctx= ctx;
        }

        public IActionResult Bookings()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Bookings(Person person) {
        
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                _ctx.Add(person);
                _ctx.SaveChanges();
                TempData["msg"] = "You have successfully added";
                return RedirectToAction("Bookings");            }
            catch (Exception ex)
            {
                TempData["msg"] = "You have not successfully added";
                return View();
            }

        }

        public IActionResult Summary()
        {
            var person = _ctx.Person.ToList();
            return View(person);
        }

       
        
    }
}
