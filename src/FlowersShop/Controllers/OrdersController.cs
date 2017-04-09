using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlowersShop.Data;
using FlowersShop.Models;
using Microsoft.AspNetCore.Identity;

namespace FlowersShop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> ShowShoppingBag()
        {
            var order = await _context.Order.Where(u => (u.ApplicationUserId == _userManager.GetUserId(User) && u.IsFinished == false))
                .Include(o => o.ProductToOrders)
                    .ThenInclude(o => o.Product)
                .ToListAsync();
            Order order2 = new Order();
            if (order.Count != 0)
            {
                order2 = order.First();
            }
            
            return View(order.Count == 0 ? null : order2);
        }

        public IActionResult FinishOrder(Order order)
        {
            return View(order);
        }
        public IActionResult FinishOrder2(Order order)
        {
            Order orderFromDb = _context.Order.First(o => o.Id == order.Id);
            orderFromDb.IsFinished = true;
            orderFromDb.DateOfFinished = DateTime.Now;
            orderFromDb.AddressCity = order.AddressCity;
            orderFromDb.AddressStreet = order.AddressStreet;
            orderFromDb.AdressHouseNumber = order.AdressHouseNumber;
            orderFromDb.AddressLocalNumber = order.AddressLocalNumber;
            orderFromDb.AddressZipCode = orderFromDb.AddressZipCode;

            _context.Update(orderFromDb);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        // GET: Orders
        public IActionResult Index()
        {
            //var applicationDbContext = await _context.Order.Include(o => o.ApplicationUser).ToListAsync();
            //var order = _context.Order.First(u => (u.ApplicationUserId == _userManager.GetUserId(User) && u.IsFinished == false));
            var orders =
                _context.Order.Where(o => o.IsFinished == true && o.IsRealized == false)
                    .Include(o => o.ProductToOrders)
                    .ThenInclude(o => o.Product)
                    .ToList();
            return View(orders);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.Include(m => m.ProductToOrders).SingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ApplicationUserId,DateOfRealize,DateTime,IsFinished,IsRealized,TotalPrice")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", order.ApplicationUserId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.SingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", order.ApplicationUserId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IsRealized")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (order.IsRealized == true)
                    {
                        order.DateOfRealize = DateTime.Now;
                    }
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", order.ApplicationUserId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.SingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.SingleOrDefaultAsync(m => m.Id == id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
