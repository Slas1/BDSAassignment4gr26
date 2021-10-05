using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment4.Entities
{
    public class Tag
    {
        public int TagID { get; set; }

        [Required]
        [Key]
        [StringLength(50)]
        public string Name { get; set; }

        public List<Task> Tasks { get; set; }
    }
}
