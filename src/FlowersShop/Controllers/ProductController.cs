using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlowersShop.Data;
using FlowersShop.Models;
using FlowersShop.Models.ProductsViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace FlowersShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _environment;

        public ProductController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: ProductViewModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductViewModel.ToListAsync());
        }

        public List<ProductViewModel> GetProductsList()
        {
            return _context.ProductViewModel.ToList();
        }

        // GET: ProductViewModels/Details/5
        public async Task<IActionResult> Details(int? id, string message)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productViewModel = await _context.ProductViewModel.SingleOrDefaultAsync(m => m.Id == id);
            if (productViewModel == null)
            {
                return NotFound();
            }
            
            var viewmodel = new ProductsInOrderViewModel {ProductViewModel = productViewModel, ProductToOrder = new ProductToOrder()};

            if (message == "A01")
            {
                message =
                    "Niestety, aktualnie nie mamy teraz takiej iloœci danego produktu w magazynie. Zmniejsz iloœc lub skontaktuj siê z obs³ug¹";
                viewmodel.Message = message;
            }
            if (message == "B01")
            {
                message =
                    "Dziêkujemy, produkt zosta³ dodany do koszyka";
                viewmodel.Message = message;
            }

            return View(viewmodel);
        }

        // GET: ProductViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult AddFile(int? id)
        {
            var prod = _context.ProductViewModel.First(p => p.Id == id);
            return View(prod);
        }

        [HttpPost]
        public IActionResult AddFile(int? id, ICollection<IFormFile> files)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "PhotosOfProducts");
            ProductViewModel prod = _context.ProductViewModel.First(p => p.Id == id);
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                        prod.File = file.FileName;
                        _context.Update(prod);
                        _context.SaveChanges();

                    }
                }
            }
            return RedirectToAction("Index");
        }

        // POST: ProductViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Name,Price,Type,File")] ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction("AddFile", new {id = productViewModel.Id});
            }
            return View(productViewModel);
        }

        // GET: ProductViewModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productViewModel = await _context.ProductViewModel.SingleOrDefaultAsync(m => m.Id == id);
            if (productViewModel == null)
            {
                return NotFound();
            }
            return View(productViewModel);
        }

        // POST: ProductViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Name,Price,Type,Amount,File")] ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductViewModelExists(productViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("AddFile", new {id = productViewModel.Id});
            }
            return View(productViewModel);
        }

        // GET: ProductViewModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productViewModel = await _context.ProductViewModel.SingleOrDefaultAsync(m => m.Id == id);
            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        // POST: ProductViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productViewModel = await _context.ProductViewModel.SingleOrDefaultAsync(m => m.Id == id);
            _context.ProductViewModel.Remove(productViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProductViewModelExists(int id)
        {
            return _context.ProductViewModel.Any(e => e.Id == id);
        }
    }
}
