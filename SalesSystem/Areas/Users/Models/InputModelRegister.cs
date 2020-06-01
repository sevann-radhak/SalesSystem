using System.ComponentModel.DataAnnotations;

namespace SalesSystem.Areas.Users.Models
{
    public class InputModelRegister
    {
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public string DNI { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [EmailAddress(ErrorMessage = "This field must to be a valid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(100, ErrorMessage = "The field {0} must to have between {2}", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\(?([0-9]{2})\)?[-. ]?([0-9]{2})[-. ]?([0-9]{5})$", ErrorMessage = "Invalid format")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
