using DataAccessLayer.Implementations;
using DataAccessLayer.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork

    {
        private readonly Context context;
        public UnitOfWork(Context context)
        {
            this.context = context;
            UserRepository = new UserRepository(context);
            AdminRepository = new AdminRepository(context);
            PersonRepository = new PersonRepository(context);
            
           
        

        }

        public IUserRepository UserRepository { get; set; }
        public IAdminRepository AdminRepository { get; set; }
        public IPersonRepository PersonRepository { get; set; }

     
     

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
