using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Data;
using ComicSystem.Models;
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
                .Include(r => r.RentalDetails)
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
                .FirstOrDefaultAsync(m => m.RentalID == id);

            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // GET: Rentals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rentals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerID,Status")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                rental.RentalDate = DateTime.Now;
                _context.Add(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rental);
        }
    }
}
