using System;
// using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment4.Entities
{
    public class Task
    {
        public int TaskID { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        public User? AssignedTo { get; set; }
        
        [StringLength(50)]
        public string? Description { get; set; }
        
        [Required]
        public State State { get; set; }

        public List<Tag> Tags { get; set; }
    }

    public enum State 
        {
             New, Active, Resolved, Closed, Removed
        }
}
