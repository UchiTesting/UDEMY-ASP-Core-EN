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
   public class IndexModel : PageModel
   {
      private AppDbContext _db;

      public IndexModel(AppDbContext db)
      {
         _db = db;
      }

      public IEnumerable<Book> Books { get; set; }
      public async Task OnGet()
      {
         Books = await _db.Books.ToListAsync();
      }
   }
}
