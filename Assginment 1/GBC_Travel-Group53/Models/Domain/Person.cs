using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GBC_Travel_Group53.Models.Domain
{
    public class Person
    {
		[Key]
		[Required]
        public string PassportNumber { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Phone { get; set; }
		[Required]
		public string Email { get; set; }

		[Required(ErrorMessage = "Destination is required")]
		public string Destination { get; set; }

		[Display(Name = "Check In")]
		[Required(ErrorMessage = "Check-in date is required")]
		public DateTime CheckIn { get; set; }

		[Display(Name = "Check Out")]
		[Required(ErrorMessage = "Check-out date is required")]
		public DateTime CheckOut { get; set; }

		[Required(ErrorMessage = "Number of rooms is required")]
		[Range(1, int.MaxValue, ErrorMessage = "Please select at least one room")]
		public int Rooms { get; set; }

		[Required(ErrorMessage = "Number of adults is required")]
		[Range(1, int.MaxValue, ErrorMessage = "Please select at least one adult")]
		public int Adults { get; set; }

		[Required(ErrorMessage = "Number of children is required")]
		[Range(0, int.MaxValue, ErrorMessage = "Please enter a valid number of children")]
		public int Children { get; set; }



	}
}
