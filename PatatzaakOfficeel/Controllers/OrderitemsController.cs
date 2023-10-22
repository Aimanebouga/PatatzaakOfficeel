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
    public class OrderitemsController : Controller
    {
        private readonly PatatzaakDBContext _context;

        public OrderitemsController(PatatzaakDBContext context)
        {
            _context = context;
        }

        // GET: Orderitems
        public async Task<IActionResult> Index()
        {
            var patatzaakDBContext = _context.Orderitems.Include(o => o.Order);
            return View(await patatzaakDBContext.ToListAsync());
        }

        // GET: Orderitems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orderitems == null)
            {
                return NotFound();
            }

            var orderitem = await _context.Orderitems
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderitem == null)
            {
                return NotFound();
            }

            return View(orderitem);
        }

        // GET: Orderitems/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id");
            return View();
        }

        // POST: Orderitems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Quantity,OrderId")] Orderitem orderitem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderitem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", orderitem.OrderId);
            return View(orderitem);
        }

        // GET: Orderitems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orderitems == null)
            {
                return NotFound();
            }

            var orderitem = await _context.Orderitems.FindAsync(id);
            if (orderitem == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", orderitem.OrderId);
            return View(orderitem);
        }

        // POST: Orderitems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Quantity,OrderId")] Orderitem orderitem)
        {
            if (id != orderitem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderitem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderitemExists(orderitem.Id))
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
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", orderitem.OrderId);
            return View(orderitem);
        }

        // GET: Orderitems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orderitems == null)
            {
                return NotFound();
            }

            var orderitem = await _context.Orderitems
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderitem == null)
            {
                return NotFound();
            }

            return View(orderitem);
        }

        // POST: Orderitems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orderitems == null)
            {
                return Problem("Entity set 'PatatzaakDBContext.Orderitems'  is null.");
            }
            var orderitem = await _context.Orderitems.FindAsync(id);
            if (orderitem != null)
            {
                _context.Orderitems.Remove(orderitem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderitemExists(int id)
        {
          return (_context.Orderitems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
