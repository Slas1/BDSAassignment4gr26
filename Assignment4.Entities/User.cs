using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment4.Entities
{
    public class User
    {
        public int UserID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Key]
        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        public List<Task> Tasks { get; set; }
    }
}
