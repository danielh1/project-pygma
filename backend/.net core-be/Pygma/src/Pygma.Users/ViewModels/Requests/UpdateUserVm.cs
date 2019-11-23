namespace Pygma.Users.ViewModels.Requests
{
    public class UpdateUserVm
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string ContactPhone { get; set; }
        
        public string ContactEmail { get; set; }

        public bool Active { get; set; }

        public int? PartnerId { get; set; }
    }
}
