using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class User
    {
        [Key] //Not required
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        public string Email { set; get; }
        [Required]
        public string Password { set; get; }
    }
}
