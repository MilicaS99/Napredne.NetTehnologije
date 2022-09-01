using DataAccessLayer.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Implementations
{
    [Table("Osobe")]
    class UserRepository : IUserRepository
    {
        private readonly Context context;

        public UserRepository(Context context)
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
            return context.Users.OfType<Person>().ToList();
           // return context.Users.ToList() ;
        }

        public List<Person> SearchBy(Expression<Func<Person, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<Person> SearchByFirstLastName(string SearchExpression)
        {
            return context.Users.Where(x => x.FirstName.Contains(SearchExpression) || x.LastName.Contains(SearchExpression)).OfType<Person>().ToList();
        }

     
        public Person SearchById(Person entity)
        {
            return context.Users.Find(entity.Id);
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
