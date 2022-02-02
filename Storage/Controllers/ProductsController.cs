#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Storage.Data;
using Storage.Models.Entities;
using Storage.Models.ViewModels;

namespace Storage.Controllers
{
    public class ProductsController : Controller
    {
        private readonly StorageContext _db;

        public ProductsController(StorageContext context)
        {
            _db = context;
        }

        // GET: Products (called at start and go back)
        public async Task<IActionResult> Index()
        {
            var model = new IndexViewModel 
            {
                Products = await _db.Product.ToListAsync(),
                Categories = await GetCategoriesAsync()
            };

            return View(model);
        } 

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _db.Product
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
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Orderdate,Category,Shelf,Count,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Add(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _db.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Orderdate,Category,Shelf,Count,Description")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(product);
                    await _db.SaveChangesAsync();
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _db.Product
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
            var product = await _db.Product.FindAsync(id);
            _db.Product.Remove(product);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _db.Product.Any(e => e.Id == id);
        }

        // GET: Product(id) into InventoryViewModel
        public async Task<IActionResult> Inventory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _db.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new InventoryViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Count = product.Count,
                InventoryValue = (product.Price * product.Count).ToString("N0")
            };

            return View(viewModel);
        }

        // GET: Products filtered by IndexViewModel
        public async Task<IActionResult> Filter(IndexViewModel viewModel)
        {
            var found = string.IsNullOrWhiteSpace(viewModel.Product) ?
                    _db.Product :
                    _db.Product.Where(p => p.Name.Contains(viewModel.Product));

            found = viewModel.Category == null ?
                    found :
                    found.Where(c => c.Category.Equals(viewModel.Category));

            var model = new IndexViewModel
            {
                Products = await found.ToListAsync(),
                Categories = await GetCategoriesAsync()
            };

            return View(nameof(Index), model);
        }

        // Used Categories in _db
        private async Task<IEnumerable<SelectListItem>> GetCategoriesAsync()
        {
            return await _db.Product
                .Select(p => p.Category)
                .Distinct()
                .Select(c => new SelectListItem
                {
                    Text = c.ToString(),
                    Value = c.ToString()
                })
                .ToListAsync();
        }
    }
}
