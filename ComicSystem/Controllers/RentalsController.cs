using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Data;
using ComicSystem.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ComicSystem.Controllers
{
    public class RentalsController : Controller
    {
        private readonly ComicContext _context;

        public RentalsController(ComicContext context)
        {
            _context = context;
        }

        // GET: Rentals/Index
        public async Task<IActionResult> Index()
        {
            var rentals = await _context.Rentals
                .Include(r => r.Customer)
                .ToListAsync();
            return View(rentals);
        }

        // GET: Rentals/Details/{id}
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.RentalDetails)
                    .ThenInclude(rd => rd.ComicBook)
                .FirstOrDefaultAsync(r => r.RentalID == id);

            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // GET: Rentals/Create
        public IActionResult Create()
        {
            ViewBag.Customers = _context.Customers.ToList();
            return View();
        }

        // POST: Rentals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerID,RentalDate,ReturnDate,Status")] Rental rental)
        {
            if (!_context.Customers.Any(c => c.CustomerID == rental.CustomerID))
            {
                ModelState.AddModelError("CustomerID", "Khách hàng không tồn tại.");
            }

            if (ModelState.IsValid)
            {
                rental.RentalDate = rental.RentalDate == default ? DateTime.Now : rental.RentalDate;
                rental.ReturnDate = rental.ReturnDate == default ? DateTime.Now.AddDays(7) : rental.ReturnDate;

                _context.Add(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Customers = _context.Customers.ToList();
            return View(rental);
        }

        // GET: Rentals/Edit/{id}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }

            ViewBag.Customers = _context.Customers.ToList();
            return View(rental);
        }

        // POST: Rentals/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RentalID,CustomerID,RentalDate,ReturnDate,Status")] Rental rental)
        {
            if (id != rental.RentalID)
            {
                return NotFound();
            }

            if (!_context.Customers.Any(c => c.CustomerID == rental.CustomerID))
            {
                ModelState.AddModelError("CustomerID", "Khách hàng không tồn tại.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rental);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalExists(rental.RentalID))
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

            ViewBag.Customers = _context.Customers.ToList();
            return View(rental);
        }

        // GET: Rentals/Delete/{id}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(r => r.RentalID == id);

            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // POST: Rentals/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rental = await _context.Rentals
                .Include(r => r.RentalDetails)
                .FirstOrDefaultAsync(r => r.RentalID == id);

            if (rental != null)
            {
                // Xóa chi tiết phiếu thuê trước khi xóa phiếu thuê
                _context.RentalDetails.RemoveRange(rental.RentalDetails);
                _context.Rentals.Remove(rental);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool RentalExists(int id)
        {
            return _context.Rentals.Any(e => e.RentalID == id);
        }
    }
}
