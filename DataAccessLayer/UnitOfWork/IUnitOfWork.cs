using DataAccessLayer.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.UnitOfWork
{
    /// <summary>
    /// ttreba da omoguci pristup svim repozitorijumima
    /// preko jednog unit of work-a pristupamo svim repozitorijumima i tada odredjujemo kada cemo da sacuvamo sve u bazu
    /// </summary>
   public  interface IUnitOfWork
    {
        public IAdminRepository AdminRepository { get; set; }
        public IUserRepository UserRepository { get; set; }

        public IPersonRepository PersonRepository { get; set; }

    



        public void Save();
    }
}
