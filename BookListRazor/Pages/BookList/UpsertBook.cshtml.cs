using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BookListRazor.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
   public class UpsertBookModel : PageModel
   {
      private readonly AppDbContext _db;

      [BindProperty]
      public Book Book { get; set; }

      public UpsertBookModel(AppDbContext db)
      {
         _db = db;
      }

      public async Task<IActionResult> OnGet(int? id)
      {
         if (id is null)
         {
            Book = new Book();
         }
         else
         {
            Book = await _db.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (Book == null) return NotFound();
         }

         return Page();
      }

      public async Task<IActionResult> OnPost()
      {

         if (!ModelState.IsValid) return Page();

         if (Book.Id == 0)
         {
            await _db.Books.AddAsync(Book);
         }
         else
         {
            Book bookInDb = await _db.Books.FindAsync(Book.Id);

            // Update values by mapping
            bookInDb.Author = Book.Author;
            bookInDb.Title = Book.Title;
            bookInDb.ISBN = Book.ISBN;
         }

         await _db.SaveChangesAsync();
         return RedirectToPage("Index");
      }
   }
}
