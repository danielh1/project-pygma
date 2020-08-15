using System.ComponentModel.DataAnnotations;
using Pygma.Data.Domain.Entities.Base;

namespace Pygma.Data.Domain.Entities
{
    public class User : EntityBase
    {
        private string _password;

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [EmailAddress]
        [MaxLength(50)]
        [Required]
        public string Email { get; set; }

        [MaxLength(50)]
        [Required]
        [DataType(DataType.Password)]
        public string Password
        {
            get => Encryption.Decrypt(_password);
            set => _password = Encryption.Encrypt(value);
        }

        [Required]
        public string Role { get; set; }

        [Required]
        public bool Active { get; set; }
    }
}
