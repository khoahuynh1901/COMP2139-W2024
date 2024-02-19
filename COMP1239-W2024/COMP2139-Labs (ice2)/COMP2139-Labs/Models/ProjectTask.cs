using System.ComponentModel.DataAnnotations;

namespace COMP2139_Labs.Models
{
    public class ProjectTask
    {
        [Key]
        public int ProjectTaskId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public int ProjectId{ get; set; } //Foreign key

        //navgation property
        //this propety allows for easy access to the related project entity from the task entity
        public Project? Project { get; set; }
    }
}
