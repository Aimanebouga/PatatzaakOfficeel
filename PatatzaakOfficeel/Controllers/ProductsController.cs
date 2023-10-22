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
    public class ProductsController : Controller
    {
        private readonly PatatzaakDBContext _context;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        public ProductsController(PatatzaakDBContext context , Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;

        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var patatzaakDBContext = _context.Products.Include(p => p.Category);
            return View(await patatzaakDBContext.ToListAsync());
        }
		public async Task<IActionResult> CustomerMenu()
		{
            List<string> categoriesNames = _context.Categories
                .Select(category => category.Name)
                .ToList();

            ViewData["categoriesNames"] = categoriesNames;
            var patatzaakDBContext = _context.Products.Include(p => p.Category);
			return View(await patatzaakDBContext.ToListAsync());

		}

		// GET: Products/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, [Bind("Id,CategoryID,Name,Description,ImageURL,Price,Discount")] Product product)
        {
            if (ModelState.IsValid)
            {
                string fileName = string.Empty;
                if (file != null)
                {
                    string images = Path.Combine(_environment.WebRootPath, "images");
                    fileName = file.FileName;
                    string fullPath = Path.Combine(images, fileName);
                    file.CopyTo(new FileStream(fullPath, FileMode.Create));

                    product.ImageURL = fileName;
                }
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryID);
            return View(product);
        }
        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryID);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile file, [Bind("Id,CategoryID,Name,Description,ImageURL,Price,Discount")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }


            try
            {
                string fileName = null;

                if (file != null)
                {
                    string images = Path.Combine(_environment.WebRootPath, "images");
                    fileName = file.FileName;
                    string fullPath = Path.Combine(images, fileName);

                    string currentImageName = _context.Products
                         .Where(p => p.Id == id)
                         .Select(p => p.ImageURL)
                         .FirstOrDefault();

                  
                    if (!string.IsNullOrEmpty(currentImageName))
                    {
                        string currentImagePath = Path.Combine(images, currentImageName);
                        if (System.IO.File.Exists(currentImagePath))
                        {
                            System.IO.File.Delete(currentImagePath);
                        }
                    }

                    
                    file.CopyTo(new FileStream(fullPath, FileMode.Create));
                }
                else
                {
                  
                    string currentImageName = _context.Products
                         .Where(p => p.Id == id)
                         .Select(p => p.ImageURL)
                         .FirstOrDefault();
                    fileName = currentImageName;
                }

                
                product.ImageURL = fileName;
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id))
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


        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'PatatzaakDBContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
