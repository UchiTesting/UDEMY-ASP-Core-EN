using BookListRazor.Models;

using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Linq;
//using System.Web.Http;

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
      public IActionResult Get()
      {
         var list = _db.Books.ToList();

         if (list.Count < 1) return NotFound();

         return Ok(list);
      }

      // GET api/<BookController>/5
      [HttpGet("{id}")]
      public IActionResult Get(int id)
      {
         var book = _db.Books.Find(id);

         if (book is null) return NotFound();

         return Ok(book);
      }
   }
}
