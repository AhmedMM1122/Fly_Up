﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace Fly_Up.Models
{
    public class Enroll
    {
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter FirstName")]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter LastName")]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password \"{0}\" must have {2} characters", MinimumLength = 8)]
        [RegularExpression(@"^([a-zA-Z0-9@*#]{8,15})$", ErrorMessage = "Password must contain: Minimum 8 characters, at least 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number, and 1 Special Character")]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Please enter confirm password")]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again!")]
        [DataType(DataType.Password)]
        public string Confirmpwd { get; set; }

        public Nullable<bool> Is_Deleted { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Please Select the gender")]
        public string Gender { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail id is not valid")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        public string PhoneNumber { get; set; }

        

        public List<Enroll> Enrollsinfo { get; set; }
    }
}
