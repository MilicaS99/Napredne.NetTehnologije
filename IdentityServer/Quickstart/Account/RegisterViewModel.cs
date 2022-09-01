using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Quickstart.UserRegistration
{
    public class RegisterViewModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Email { get; set; }
        public string ReturnUrl { get; set; }

        public string Role { get; set; }

        public List<SelectListItem> Roles = new List<SelectListItem>()
        {
             new SelectListItem() { Text = "User", Value = "User" },
              new SelectListItem() { Text = "Admin", Value = "Admin" }

    };

        

    }
}
