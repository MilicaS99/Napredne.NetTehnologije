using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCapp.Models
{
    public class UserObligationViewModel
    {
        public UserViewModel User { get; set; }

        public List<Obligation> Obligations { get; set; } = new List<Obligation>();
    }
}
