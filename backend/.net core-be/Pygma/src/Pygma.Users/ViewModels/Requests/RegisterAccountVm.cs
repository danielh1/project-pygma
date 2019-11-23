using System.ComponentModel.DataAnnotations;

namespace Pygma.Users.ViewModels.Requests
{
    public class RegisterAccountVm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}