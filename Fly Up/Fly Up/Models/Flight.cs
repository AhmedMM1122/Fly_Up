using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Fly_Up.Models
{
    public class Flight
    {
        public int Id { get; set; }
        [DisplayName("Flight Name")]
        [Required(ErrorMessage = "You have to provide a valid Name!")]
        [MinLength(5, ErrorMessage = "Name must not be less than 5 Characters!")]
        [MaxLength(30, ErrorMessage = "Name must not exceed 30 Characters!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "You have to provide a valid Description!")]
        [MinLength(20, ErrorMessage = "Description must not be less than 20 Characters!")]
        [MaxLength(150, ErrorMessage = "Description must not exceed 150 Characters!")]
        public string Description { get; set; }
        [Required(ErrorMessage = "You have to provide a valid Country Name!")]
        [MinLength(5, ErrorMessage = "Country Name not be less than 5 Characters!")]
        [MaxLength(20, ErrorMessage = "Country Name must not exceed 20 Characters!")]
        public string Destinition { get; set; }
        [Required(ErrorMessage = "You have to provide a valid Price!")]
        [Range(0, 100000, ErrorMessage = "AnnualBudget must be between 0 EGP And 100000 EGP")]
        public decimal Price { get; set; }

        [DisplayName("TakeOff Date")]
        [Required(ErrorMessage = "Takeoff Time must be at least 7 days from now!")]

        public DateTime TakeoffTime { get; set; }

        [DisplayName("Capacity")]
        [Range(1, int.MaxValue, ErrorMessage = "Please provide a valid capacity.")]
        public int Capacity { get; set; }
		
        

        [DisplayName("Status")]
        public bool IsAvailable { get; set; }

		

		[ValidateNever]
        public List<Ticket> Tickets { get; set; }
        
        [DisplayName("Image")]
        [ValidateNever]
        public string ImagePath { get; set; }
    }
}
