using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ProjectHandler.Models
{
    public partial class Project
    {
        public Project()
        {
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
        [Required]
        public string ProjectName { get; set; }
        public DateTime ProjectCreated { get; set; }
        public DateTime? ProjectStartDate { get; set; }
        public DateTime? ProjectCompletionDate { get; set; }
        [Required]
        public string ProjectStatus { get; set; }
        public int? ProjectPriority { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
