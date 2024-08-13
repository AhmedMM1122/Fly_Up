using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Fly_Up.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        [DisplayName("Ticket Name")]
        [Required(ErrorMessage = "You have to provide a valid Name!")]
        [MinLength(5, ErrorMessage = "Name must not be less than 5 Characters!")]
        [MaxLength(30, ErrorMessage = "Name Name must not exceed 20 Characters!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "You have to provide a valid Description!")]
        [MinLength(20, ErrorMessage = "Description must not be less than 20 Characters!")]
        [MaxLength(150, ErrorMessage = "Description must not exceed 150 Characters!")]
        public string Description { get; set; }
        [Required(ErrorMessage = "You have to provide a valid Rating!")]
        [Range(1, 7, ErrorMessage = "Rating must be between 1 Star And 7 Star")]
        public decimal Rating { get; set; }
        [Required(ErrorMessage = "You have to provide a valid Price!")]
        [Range(0, 100000, ErrorMessage = "AnnualBudget must be between 0 EGP And 100000 EGP")]
        public decimal Price { get; set; }
        [DisplayName("Release Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please provide a valid Release Date.")]
        public DateTime ReleaseDate { get; set; }
        

		

        [DisplayName("Flight")]
        [Range(0, double.MaxValue, ErrorMessage = "Select a valid Flight")]
        public int FlightId { get; set; }
        [ValidateNever]
        public Flight Flight { get; set; }
        [DisplayName("Image")]
        [ValidateNever]
        public string ImagePath { get; set; }
    }
}
