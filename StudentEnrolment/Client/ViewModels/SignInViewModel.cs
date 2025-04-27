using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StudentEnrolment.Client.ViewModels
{
    public class SignInViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
