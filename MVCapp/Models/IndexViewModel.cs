using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCapp.Models
{
    public class IndexViewModel
    {
        public List<User>? Persons { get; set; }
         public string Search { get; set; }
}
}
