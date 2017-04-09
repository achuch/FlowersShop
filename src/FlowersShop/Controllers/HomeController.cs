using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FlowersShop.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlowersShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _environment;

        public HomeController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
            if (_context != null)
            {
                var list = _context.ProductViewModel.ToList();
                return View(list);
            }
            else
            {
                return View();
            }
            
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult File(ICollection<IFormFile> files)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "PhotosOfProducts");
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}
