using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
   public interface IRepository<TEntity> where TEntity:class
    {

        public void Add(TEntity entity);
        public void Delete(TEntity entity);
        public List<TEntity> SearchBy(Expression <Func<TEntity, bool>> predicate);

        public List<TEntity> SearchByFirstLastName(string expression);
        public TEntity SearchById(TEntity entity);

        public List<TEntity> GetAll();
        public void Update(TEntity entity);
    }
}
