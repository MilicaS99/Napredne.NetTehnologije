using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Quickstart.UserRegistration
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string ReturnUrl { get; set; }

        [Required]
        public string Role { get; set; }

        public List<SelectListItem> Roles = new List<SelectListItem>()
        {
             new SelectListItem() { Text = "User", Value = "User" },
              new SelectListItem() { Text = "Admin", Value = "Admin" }

    };

        

    }
}
