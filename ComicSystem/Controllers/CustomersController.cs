using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Thêm namespace này
using ComicSystem.Data;
using ComicSystem.Models;
using System.Threading.Tasks;

namespace ComicSystem.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ComicContext _context;

        public CustomersController(ComicContext context)
        {
            _context = context;
        }

        // GET: Customers/Index
        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customers.ToListAsync(); // Sửa lỗi ToListAsync
            return View(customers);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View(new Customer());
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,PhoneNumber")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.RegistrationDate = DateTime.Now;
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/{id}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id); // Sửa lỗi nếu FindAsync bị thiếu
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerID,FullName,PhoneNumber,RegistrationDate")] Customer customer)
        {
            if (id != customer.CustomerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) // Sửa lỗi DbUpdateConcurrencyException
                {
                    if (!CustomerExists(customer.CustomerID))
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

            return View(customer);
        }

        // GET: Customers/Delete/{id}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerID == id); // Sửa lỗi FirstOrDefaultAsync
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerID == id);
        }
    }
}
