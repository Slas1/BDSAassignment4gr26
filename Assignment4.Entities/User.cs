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

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        public ICollection<Task> Tasks { get; set; }

        /*
        Id : int
        Name : string(100), required
        Email : string(100), required, unique
        Tasks : list of Task entities belonging to User
        */
    }
}
