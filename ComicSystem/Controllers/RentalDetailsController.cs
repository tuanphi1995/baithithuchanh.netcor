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

        // GET: RentalDetails/Delete/{id}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentalDetail = await _context.RentalDetails
                .Include(rd => rd.ComicBook)
                .Include(rd => rd.Rental)
                .FirstOrDefaultAsync(m => m.RentalDetailID == id);

            if (rentalDetail == null)
            {
                return NotFound();
            }

            return View(rentalDetail);
        }

        // POST: RentalDetails/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rentalDetail = await _context.RentalDetails.FindAsync(id);
            if (rentalDetail != null)
            {
                _context.RentalDetails.Remove(rentalDetail);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", "Rentals", new { id = rentalDetail.RentalID });
        }
    }
}
