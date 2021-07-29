using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Book
    {
        [Key] //not required 
        public int Id { set; get; }
        [Required]
        public string Name{ set; get; }
        public string Author { set; get; }
        public string ISBN { set; get; }
    }
}
