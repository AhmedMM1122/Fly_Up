using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Fly_Up.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [DisplayName("Passenger Name")]
        [Required(ErrorMessage = "Please provide the passenger's name.")]
        public string PassengerName { get; set; }

        [EmailAddress(ErrorMessage = "Please provide a valid email address.")]
        public string PassengerEmail { get; set; }

        [Phone(ErrorMessage = "Please provide a valid phone number.")]
        public string PassengerPhoneNumber { get; set; }

        [DisplayName("Flight")]
        [Range(0, int.MaxValue, ErrorMessage = "Select a valid Flight")]
        public int FlightId { get; set; }
		[ValidateNever]

		public Flight Flight { get; set; }

        [DisplayName("Ticket")]
        [Range(0, int.MaxValue, ErrorMessage = "Select a valid Ticket")]
        public int TicketId { get; set; }
        [ValidateNever]
        public Ticket Ticket { get; set; }

        [DisplayName("Booking Date")]
        public DateTime BookingDate { get; set; } = DateTime.Now;
    }
}
