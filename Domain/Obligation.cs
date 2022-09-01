using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Obligation
    {
        public int ObligationId { get; set; }


        public int PersonId { get; set; }

        public Person Person { get; set; }
        public  string  Name { get; set; }

        public string Description { get; set; }


        public DateTime Deadline { get; set; }
    }
}
