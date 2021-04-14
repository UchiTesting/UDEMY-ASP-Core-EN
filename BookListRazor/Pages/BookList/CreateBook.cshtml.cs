using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BookListRazor.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
   public class CreateBookModel : PageModel
   {
      private readonly AppDbContext _db;

      public CreateBookModel(AppDbContext db)
      {
         _db = db;
      }

      public Book Book { get; set; }
      public void OnGet()
      {
      }
   }
}
