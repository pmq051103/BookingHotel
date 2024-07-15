using System.ComponentModel.DataAnnotations;
using BookingHotel.Models;

namespace BookingHotel.ViewModels
{
    public class AccountViewModels
    {
        [Required]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Invalid length")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Invalid Username .")]
        public string Username { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Invalid length")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Invalid length")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^(0|\+84)(\d{9,10})$", ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }
        public List<Request> Requests { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Invalid length")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Invalid Username .")]
        public string Username { get; set; }


        [Required]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Invalid length")]
        [DataType(DataType.Password)]
        public string PasswordCurrent { get; set; }



        [Required]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Invalid length")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Invalid length")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
    public class LoginViewModel
    {
        [Required]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Invalid length")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Invalid Username .")]
        public string Username { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Invalid length")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
