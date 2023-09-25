namespace Entities.Models.ViewModels
{
    public class UserViewModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string GivenName { get; set; }

        public string Surname { get; set; }

        public int RoleRefId { get; set; }
        public Role? Role { get; set; }
    }
}
