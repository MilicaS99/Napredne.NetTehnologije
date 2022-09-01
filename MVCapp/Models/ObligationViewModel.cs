using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCapp.Models
{
    public class ObligationViewModel
    {

        public int ObligationId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }


        public DateTime Deadline { get; set; }

        public int Sn { get; set; }
    }
}
