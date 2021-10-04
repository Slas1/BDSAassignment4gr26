using System;
//using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment4.Entities
{
    public class Task
    {

        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        public User? AssignedTo { get; set; }
        
        [StringLength(50)]
        public string? Description { get; set; }
        
        [Required]
        public State State { get; set; }

        //public IList<TaskTag> TaskTags { get; set; }
        // public ICollection<Tag> Tags { get; set; }

        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Task>()
                .Property(e => e.State)
                .HasConversion<string>();
        }
        */


        /*
        Id : int
        Title : string(100), required
        AssignedTo : optional reference to User entity
        Description : string(max), optional
        State : enum (New, Active, Resolved, Closed, Removed), required
        Tags : many-to-many reference to Tag entity
        */
    }
}
