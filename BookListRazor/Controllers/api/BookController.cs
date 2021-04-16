using BookListRazor.Models;

using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookListRazor.Controllers.api
{
   [Route("api/[controller]")]
   [ApiController]
   public class BookController : ControllerBase
   {
      private AppDbContext _db;

      public BookController(AppDbContext db)
      {
         _db = db;
      }

      // GET: api/<BookController>
      [HttpGet]
      public async Task<IActionResult> GetAll()
      {
         var list = await _db.Books.ToListAsync();

         if (list.Count < 1) return NotFound();

         return Ok(new { data = list });
      }

      // GET api/<BookController>/5
      [HttpGet("{id}")]
      public async Task<IActionResult> Get(int id)
      {
         var book = await _db.Books.FindAsync(id);

         if (book is null) return NotFound();

         return Ok(book);
      }

      [HttpPost]
      public async Task<IActionResult> Post(Book book)
      {

         if (!ModelState.IsValid) return BadRequest();

         await _db.Books.AddAsync(book);
         await _db.SaveChangesAsync();

         return Created(new System.Uri($"{Request.GetDisplayUrl()}/{book.Id}"), book);
      }
   }
}
