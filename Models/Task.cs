using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ProjectHandler.Models
{
    public partial class Task
    {
        public int Id { get; set; }
        [Required]
        public string TaskName { get; set; }
        [Required]
        public string TaskStatus { get; set; }
        public int? TaskPriority { get; set; }
        public string TaskDescription { get; set; }
        [Required]
        public int? ProjectId { get; set; }

        public virtual Project Project { get; set; }
    }
}
