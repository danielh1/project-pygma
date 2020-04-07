using System.ComponentModel.DataAnnotations;

namespace Pygma.Users.ViewModels.Requests
{
    public class RegisterAccountVm
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}