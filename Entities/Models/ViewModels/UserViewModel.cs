using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.ViewModels
{
    public class UserViewModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public int RoleRefId { get; set; }
        public Role? Role { get; set; }
    }
}
