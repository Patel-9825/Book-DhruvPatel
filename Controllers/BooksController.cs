using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab5NETD.Data;
using Lab5NETD.Models;
using Microsoft.AspNetCore.Authorization;

namespace Lab5NETD.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(await _context.Books.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var finalBook = await _context.Books
                .FirstOrDefaultAsync(m => m.ID == id);
            if (finalBook == null)
            {
                return NotFound();
            }

            return View(finalBook);
        }

        // GET: Books/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,title,isbn,version,price,condition")] FinalBook finalBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(finalBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(finalBook);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var finalBook = await _context.Books.FindAsync(id);
            if (finalBook == null)
            {
                return NotFound();
            }
            return View(finalBook);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,title,isbn,version,price,condition")] FinalBook finalBook)
        {
            if (id != finalBook.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(finalBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FinalBookExists(finalBook.ID))
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
            return View(finalBook);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var finalBook = await _context.Books
                .FirstOrDefaultAsync(m => m.ID == id);
            if (finalBook == null)
            {
                return NotFound();
            }

            return View(finalBook);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var finalBook = await _context.Books.FindAsync(id);
            _context.Books.Remove(finalBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FinalBookExists(int id)
        {
            return _context.Books.Any(e => e.ID == id);
        }

        public async Task<IActionResult> Searching()
        {
            return View();
        }

        public async Task<IActionResult> ShowSearchResults(string SearchBook)
        {
            return View("Index", await _context.Books.Where(s => s.title.Contains(SearchBook)).ToListAsync());
        }
    }
}
