using BookListRazor.Models;

using Microsoft.AspNetCore.Mvc;

using System.Linq;

namespace BookListRazor.Controllers
{
   [Route("apimvc/[controller]")]
   [ApiController]
   public class BookController : Controller
   {
      private readonly AppDbContext _db;

      public BookController(AppDbContext db) => _db = db;

      [HttpGet]
      public IActionResult GetAll()
      {
         return Json(new { data = _db.Books.ToList() });
      }

      [HttpGet("{id}")]
      public IActionResult Get(int id)
      {
         return Json(new { data = _db.Books.SingleOrDefault(b => b.Id == id) });
      }

      //[HttpDelete("{id}")]
      [HttpDelete]
      public IActionResult Delete(int id)
      {
         var bookToDelete = _db.Books.Find(id);

         if (bookToDelete is null) return Json(new { success = false, message = "Error while deleting." });

         _db.Books.Remove(bookToDelete);
         _db.SaveChanges();

         return Json(new { success = true, message = "Deleted book successfully." });
      }
   }
}
