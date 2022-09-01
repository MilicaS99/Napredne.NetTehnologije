using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IPersonRepository : IRepository<Person>
    {
       Person SearchByUsernamePassword(string username, string password);
        void AddObl(List<Obligation> o);
        void DeleteObl(Obligation obligations);
    }
}
