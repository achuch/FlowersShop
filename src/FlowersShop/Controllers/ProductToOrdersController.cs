using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlowersShop.Data;
using FlowersShop.Models;

namespace FlowersShop.Controllers
{
    public class ProductToOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductToOrdersController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: ProductToOrders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProductToOrder.Include(p => p.Order).Include(p => p.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProductToOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productToOrder = await _context.ProductToOrder.SingleOrDefaultAsync(m => m.Id == id);
            if (productToOrder == null)
            {
                return NotFound();
            }

            return View(productToOrder);
        }

        // GET: ProductToOrders/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Set<Order>(), "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.ProductViewModel, "Id", "Id");
            return View();
        }

        // POST: ProductToOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AmountOfProduct,OrderId,ProductId,TotalPriceForThisProduct")] ProductToOrder productToOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productToOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["OrderId"] = new SelectList(_context.Set<Order>(), "Id", "Id", productToOrder.OrderId);
            ViewData["ProductId"] = new SelectList(_context.ProductViewModel, "Id", "Id", productToOrder.ProductId);
            return View(productToOrder);
        }

        // GET: ProductToOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productToOrder = await _context.ProductToOrder.SingleOrDefaultAsync(m => m.Id == id);
            if (productToOrder == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Set<Order>(), "Id", "Id", productToOrder.OrderId);
            ViewData["ProductId"] = new SelectList(_context.ProductViewModel, "Id", "Id", productToOrder.ProductId);
            return View(productToOrder);
        }

        // POST: ProductToOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AmountOfProduct,OrderId,ProductId,TotalPriceForThisProduct")] ProductToOrder productToOrder)
        {
            if (id != productToOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productToOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductToOrderExists(productToOrder.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["OrderId"] = new SelectList(_context.Set<Order>(), "Id", "Id", productToOrder.OrderId);
            ViewData["ProductId"] = new SelectList(_context.ProductViewModel, "Id", "Id", productToOrder.ProductId);
            return View(productToOrder);
        }

        // GET: ProductToOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productToOrder = await _context.ProductToOrder.SingleOrDefaultAsync(m => m.Id == id);
            if (productToOrder == null)
            {
                return NotFound();
            }

            return View(productToOrder);
        }

        // POST: ProductToOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productToOrder = await _context.ProductToOrder.SingleOrDefaultAsync(m => m.Id == id);
            _context.ProductToOrder.Remove(productToOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProductToOrderExists(int id)
        {
            return _context.ProductToOrder.Any(e => e.Id == id);
        }
    }
}
