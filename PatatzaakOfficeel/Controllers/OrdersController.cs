using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PatatzaakOfficeel.Models;

namespace PatatzaakOfficeel.Controllers
{
    public class OrdersController : Controller
    {
        private readonly PatatzaakDBContext _context;

        public OrdersController(PatatzaakDBContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            int customerId = int.Parse(HttpContext.Session.GetString("CustomerId"));
            var pataatZaakDbContext = _context.Orders.Where(o => o.CustomerId == customerId).Include(o => o.Orderitems);
            return View(await pataatZaakDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Orderitems)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create()
        {

            Order order = new Order();
            List<Orderitem> orderitems = new List<Orderitem>();
            int customerId = int.Parse(HttpContext.Session.GetString("CustomerId"));

            DateTime date = DateTime.Now;
            order.CustomerId = customerId;
            order.DateTime = date;
            List<Cart> carts = _context.Carts.Where(oi => oi.CustomerId == customerId).Include(c => c.Product).ToList();

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();


            foreach (Cart cart in carts)
            {
                Orderitem orderitem = new Orderitem();
                orderitem.Name = cart.Product.Name;
                orderitem.Quantity = cart.Quantity;
                orderitem.OrderId = order.Id;

                orderitem.Price = decimal.Parse(cart.Product.Price.ToString());

                orderitems.Add(orderitem);
                _context.Carts.Remove(cart);
            }
            foreach (Orderitem orderItem in orderitems)
            {
                _context.Orderitems.Add(orderItem);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));

        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
      

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", order.CustomerId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateTime,TotalCost,CustomerId")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            if (_context.Orders == null)
            {
                return Problem("Entity set 'PatatzaakDBContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
