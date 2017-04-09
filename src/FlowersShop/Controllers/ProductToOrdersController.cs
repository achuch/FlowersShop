using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlowersShop.Data;
using FlowersShop.Models;
using FlowersShop.Models.ProductsViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FlowersShop.Controllers
{
    public class ProductToOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductToOrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
        public async Task<IActionResult> Create(ProductsInOrderViewModel productsInOrderViewModel)
        {
            ProductToOrder productToOrder = productsInOrderViewModel.ProductToOrder;
            if (ModelState.IsValid)
            {
                var oneProduct = _context.ProductViewModel.First(s => s.Id == productToOrder.ProductId);
                if (oneProduct.Amount < productToOrder.AmountOfProduct)
                {
                    return RedirectToAction("Details","Product",new {id = oneProduct.Id, message = "A01"});
                }
                productToOrder.Product = oneProduct;
                productToOrder.ProductId = 0;
                productToOrder.TotalPriceForThisProduct = productToOrder.AmountOfProduct * oneProduct.Price;
                oneProduct.Amount = oneProduct.Amount - productToOrder.AmountOfProduct;
                _context.Update(oneProduct);

                var orderList = _context.Order.ToList();
                Order order = new Order { ApplicationUserId = _userManager.GetUserId(User), DateTime = DateTime.Now, IsFinished = false, IsRealized = false, TotalPrice = 0 };
                if (orderList.Count == 0)
                {
                   _context.Add(order);
                    productToOrder.Order = order;
                    productToOrder.Order.TotalPrice = productToOrder.TotalPriceForThisProduct;
                   _context.Update(productToOrder.Order);
                }
                else
                {
                    var userId = _userManager.GetUserId(User);
                    orderList = _context.Order.Where(o => o.ApplicationUserId == userId && o.IsFinished == false).ToList();
                    productToOrder.Order = orderList.Count == 0 ? order : orderList.First();
                    productToOrder.Order.TotalPrice = productToOrder.TotalPriceForThisProduct + productToOrder.Order.TotalPrice;
                    //_context.Update(productToOrder.Order);
                    //productToOrder.Order = order;
                }
               
                _context.Add(productToOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Product", new { id = oneProduct.Id, message = "B01" });
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
