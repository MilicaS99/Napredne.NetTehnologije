
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Domain
{
    public class Person:IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Obligation> Obligations { get; set; }
    }
}
