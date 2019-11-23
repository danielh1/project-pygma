namespace Pygma.Users.ViewModels.Responses
{
    public class UserListVm
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Email { get; set; }
        
        public string ContactPhone { get; set; }
        
        public string ContactEmail { get; set; }

        public bool Active { get; set; }
    }
}
