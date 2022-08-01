using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyProject.Models
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public string Name { get; set; }
        public int age { get; set; }
    }
}
