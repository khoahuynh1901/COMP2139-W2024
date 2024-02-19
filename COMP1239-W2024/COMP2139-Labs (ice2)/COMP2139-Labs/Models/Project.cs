using System.ComponentModel.DataAnnotations;

namespace COMP2139_Labs.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }
        public string? status { get; set; }


        public List<ProjectTask>? Tasks { get; set; }
    }
}


/*
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
//using System.Web.Mvc;

namespace COMP2139_Labs.Models
{
    public class Project
    {
        //'?' makes it nullable
        //[Required(ErrorMessage = "Project ID is required")]
        //[MaxLength(10)]
        [Key]
        [DisplayName("Project ID:")]
        public int ProjectId { get; set; }
        
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string? Description { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End Date is required")]
        public DateTime EndDate { get; set; }
        public string? status { get; set; }
        
        //collection of related tasks
        public List<ProjectTask>? Tasks { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate < StartDate)
            {
                yield return new ValidationResult("Enddate bust be later than startdate", new[] { nameof(EndDate), nameof(StartDate) });
            }
        }
    }
}

*/