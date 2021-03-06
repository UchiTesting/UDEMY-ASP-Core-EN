using BookListMVC.Models;

using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.Controllers
{
   public class BooksController : Controller
   {
      private readonly AppDbContext _db;
      public BooksController(AppDbContext db)
      {
         _db = db;
      }

      public IActionResult Index()
      {
         return View();
      }

      [BindProperty]
      public Book Book { get; set; }

      public IActionResult Upsert(int? id)
      {
         Book = new Book();

         if (id is null) return View(Book);

         Book = _db.Books.SingleOrDefault(b => b.Id == id);

         if (Book is null) return NotFound();

         return View(Book);
      }

      #region API calls
      [HttpGet]
      public async Task<IActionResult> GetAll()
      {
         return Json(new { data = await _db.Books.ToListAsync() });
      }

      [HttpGet("{id}")]
      public async Task<IActionResult> Get(int id)
      {
         return Json(new { data = await _db.Books.SingleOrDefaultAsync(b => b.Id == id) });
      }

      [HttpPost]
      public async Task<IActionResult> Post(Book book)
      {
         if (!ModelState.IsValid) return BadRequest();

         await _db.Books.AddAsync(book);
         await _db.SaveChangesAsync();

         return Created(new System.Uri($"{Request.GetDisplayUrl()}/{book.Id}"), book);
      }

      [HttpDelete]
      public async Task<IActionResult> Delete(int id)
      {
         var bookToDelete = await _db.Books.FindAsync(id);

         if (bookToDelete is null) return Json(new { success = false, message = "Error while deleting." });

         _db.Books.Remove(bookToDelete);
         await _db.SaveChangesAsync();

         return Json(new { success = true, message = "Deleted book successfully." });
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public IActionResult Upsert()
      {
         if (ModelState.IsValid)
         {
            if (Book.Id == 0)
            {
               _db.Books.Add(Book);
            }
            else
            {
               _db.Books.Update(Book);
            }

            _db.SaveChanges();
            return RedirectToAction("Index");
         }

         return View(Book);
      }
      #endregion
   }
}
