using DataAccessLayer.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Implementations
{
    public class AdminRepository : IAdminRepository
    {
        private readonly Context context;
        public AdminRepository(Context context)
        {
            this.context = context;
        }
        public void Add(Person entity)
        {
            throw new NotImplementedException();
        }

        public void AddObl(List<Obligation> o)
        {
            throw new NotImplementedException();
        }

        public void Delete(Person entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteObl(Obligation obligations)
        {
            throw new NotImplementedException();
        }

        public List<Person> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Person> SearchBy(Expression<Func<Person, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<Person> SearchByFirstLastName(string expression)
        {
            throw new NotImplementedException();
        }

        public Person SearchById(Person entity)
        {
            throw new NotImplementedException();
        }

        public Person SearchByUsernamePassword(string username, string password)
        {
            throw new NotImplementedException();
        }

        public void Update(Person entity)
        {
            throw new NotImplementedException();
        }
    }
}
