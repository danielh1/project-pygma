using System;

namespace Pygma.Users.ViewModels.Responses.Users
{
    public class UserVm
    {
        public int Id { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Email { get; set; }

        public bool Active { get; set; }
    }
}
