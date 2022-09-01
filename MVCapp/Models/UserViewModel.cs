using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCapp.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ObligationViewModel Obligation { get; set; }
    }
}
