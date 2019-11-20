using System.ComponentModel.DataAnnotations;
using Pygma.Data.Domain.Entities.Base;

namespace Pygma.Data.Domain.Entities
{
    public class User : EntityBase
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [EmailAddress]
        [MaxLength(50)]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public bool Active { get; set; }
    }
}
