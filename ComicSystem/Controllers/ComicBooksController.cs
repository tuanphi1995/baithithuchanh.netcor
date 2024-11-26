using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Data;
using ComicSystem.Models;
using System.Threading.Tasks;

namespace ComicSystem.Controllers
{
    public class ComicBooksController : Controller
    {
        private readonly ComicContext _context;

        public ComicBooksController(ComicContext context)
        {
            _context = context;
        }

        // GET: ComicBooks/Index
        public async Task<IActionResult> Index()
        {
            var comicBooks = await _context.ComicBooks.ToListAsync();
            return View(comicBooks);
        }

        // GET: ComicBooks/Create
        public IActionResult Create()
        {
            return View(new ComicBook());
        }

        // POST: ComicBooks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Author,PricePerDay")] ComicBook comicBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comicBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comicBook);
        }

        // GET: ComicBooks/Edit/{id}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comicBook = await _context.ComicBooks.FindAsync(id);
            if (comicBook == null)
            {
                return NotFound();
            }

            return View(comicBook);
        }

        // POST: ComicBooks/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComicBookID,Title,Author,PricePerDay")] ComicBook comicBook)
        {
            if (id != comicBook.ComicBookID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comicBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComicBookExists(comicBook.ComicBookID))
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

            return View(comicBook);
        }

        // GET: ComicBooks/Delete/{id}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comicBook = await _context.ComicBooks
                .FirstOrDefaultAsync(m => m.ComicBookID == id);
            if (comicBook == null)
            {
                return NotFound();
            }

            return View(comicBook);
        }

        // POST: ComicBooks/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comicBook = await _context.ComicBooks.FindAsync(id);
            if (comicBook != null)
            {
                _context.ComicBooks.Remove(comicBook);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComicBookExists(int id)
        {
            return _context.ComicBooks.Any(e => e.ComicBookID == id);
        }
    }
}
