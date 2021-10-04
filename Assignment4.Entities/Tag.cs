using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment4.Entities
{
    public class Tag
    {
        public int TagID { get; set; }

        [Required]
        //[Index(IsUnique = true)]
        [StringLength(50)]
        public string Name { get; set; }

        //public IList<TaskTag> TaskTags { get; set; }
        //public ICollection<Task> Tasks { get; set; }

        /*
        Id : int
        Name : string(50), required, unique
        Tasks : many-to-many reference to Task entity
        */
    }
}
