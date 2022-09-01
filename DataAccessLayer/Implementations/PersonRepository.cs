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
    public  class PersonRepository : IPersonRepository
    {
        private readonly Context context;

        public PersonRepository(Context context)
        {
            this.context = context;
        }
        public void Add(Person entity)
        {
            throw new NotImplementedException();
        }
        public void AddObl(List<Obligation> entities)
        {
            context.AddRange(entities);
        }

        public void Delete(Person entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteObl(Obligation entity)
        {
            context.Remove(entity);
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
            return context.Persons.Find(entity.Id);
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
