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
   }
}
