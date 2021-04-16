using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.Models
{
   public class Book
   {
      [Key]
      public int Id { get; set; }

      [Required]
      public string Title { get; set; }
      public string Author { get; set; }
      [MaxLength(17)]
      public string ISBN { get; set; }
   }
}
