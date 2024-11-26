using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Data;
using ComicSystem.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ComicSystem.Controllers
{
    public class RentalDetailsController : Controller
    {
        private readonly ComicContext _context;

        public RentalDetailsController(ComicContext context)
        {
            _context = context;
        }

        // GET: RentalDetails/Create
        public IActionResult Create(int rentalId)
        {
            ViewData["RentalID"] = rentalId;
            ViewBag.ComicBooks = _context.ComicBooks.ToList();
            return View(new RentalDetail { RentalID = rentalId });
        }

        // POST: RentalDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentalID,ComicBookID,Quantity")] RentalDetail rentalDetail)
        {
            var comicBook = await _context.ComicBooks.FindAsync(rentalDetail.ComicBookID);
            if (comicBook == null)
            {
                ModelState.AddModelError("ComicBookID", "Sách không tồn tại.");
                return View(rentalDetail);
            }

            rentalDetail.PricePerDay = comicBook.PricePerDay;

            if (ModelState.IsValid)
            {
                _context.Add(rentalDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Rentals", new { id = rentalDetail.RentalID });
            }
            return View(rentalDetail);
        }
    }
}
