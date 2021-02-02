using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace cleanArchitecture.Domain.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First name")]
        [MaxLength(200, ErrorMessage = "{0} length must be 200 characters")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last name")]
        [MaxLength(200, ErrorMessage = "{0} length must be 200 characters")]
        public string Lastname { get; set; }

        [Required]
        [Display(Name = "Email Id")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [MaxLength(50, ErrorMessage = "{0} length must be 50 characters")]
        public string Email { get; set; }

        public DateTime Dateofbirth { get; set; }

        public string Profilepicture { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "State is required")]
        public int StateId { get; set; }

        [Required(ErrorMessage = "City is required")]
        public int CityId { get; set; }

        public decimal? Age { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        [Display(Name = "Gender")]
        public decimal? Gender { get; set; }

        public string Address { get; set; }

        [Required]
        [Display(Name = "Contact Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string Phoneno { get; set; }


        public string Hobbies { get; set; }

        [Required(ErrorMessage = "Please Select atleast one Hobby")]
        [Display(Name = "Hobbies")]
        public int[] HobbiesArray { get; set; }

        public string convertedDate { get; set; }

    }
}
