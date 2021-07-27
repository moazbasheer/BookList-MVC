using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class User
    {
        [Key]
        public int Id { set; get; }
        [Required]
        public string name { set; get; }
        [Required]
        public string email { set; get; }
        [Required]
        public string password { set; get; }
    }
}
