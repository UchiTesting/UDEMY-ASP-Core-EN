using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using BookListRazor.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
   public class EditBookModel : PageModel
   {
      private readonly AppDbContext _db;

      [BindProperty]
      public Book Book { get; set; }

      public EditBookModel(AppDbContext db) => _db = db;

      public async Task OnGet(int id)
      {
         Book = await _db.Books.FindAsync(id);
      }

      public async Task<IActionResult> OnPost()
      {
         if (ModelState.IsValid)
         {
            Book bookInDb = await _db.Books.FindAsync(Book.Id);

            bookInDb.Author = Book.Author;
            bookInDb.Title = Book.Title;
            bookInDb.ISBN = Book.ISBN;

            _db.SaveChanges();

            return RedirectToPage("Index");
         }
         else
         {
            return Page();
         }
      }
   }
}
