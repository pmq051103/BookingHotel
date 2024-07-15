using System.ComponentModel.DataAnnotations;

namespace BookingHotel.Models
{
    public class Account
    {
        
        public int accountID {  get; set; }

        [Required]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Invalid length")]
        [Display(Name = "UserName")]
        public string username { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Invalid length")]
        [Display(Name = "Password")]
        public string password { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^(0|\+84)(\d{9,10})$", ErrorMessage = "Invalid phone number")]
        public string phoneNumber { get; set; }

        [Required]
        [Display(Name = "Role")]
        public int role { get; set; }

        public ICollection<Request> Requests { get; set; }
        public ICollection<Enrollment> Enrollment { get; set; }

    }
}
