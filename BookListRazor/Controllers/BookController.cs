using BookListRazor.Models;

using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

namespace BookListRazor.Controllers
{
   [Route("apimvc/[controller]")]
   [ApiController]
   public class BookController : Controller
   {
      private readonly AppDbContext _db;

      public BookController(AppDbContext db) => _db = db;

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

      //[HttpDelete("{id}")]
      [HttpDelete]
      public async Task<IActionResult> Delete(int id)
      {
         var bookToDelete = await _db.Books.FindAsync(id);

         if (bookToDelete is null) return Json(new { success = false, message = "Error while deleting." });

         _db.Books.Remove(bookToDelete);
         await _db.SaveChangesAsync();

         return Json(new { success = true, message = "Deleted book successfully." });
      }
   }
}
