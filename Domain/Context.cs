using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
   public  class Context:IdentityDbContext<Person,IdentityRole<int>,int>
    {
        public Context(DbContextOptions<Context> options)
          : base(options)
        {

        }

        public DbSet<Person> Persons { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Admin> Admins { get; set; }

     public DbSet<Obligation> Obligations { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
         
          base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=IdentityServerAuth;Trusted_Connection=True");
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Person>().ToTable("Osobe");

           builder.Entity<Person>().OwnsMany(p => p.Obligations).WithOwner(o => o.Person);
        }
    }
}
