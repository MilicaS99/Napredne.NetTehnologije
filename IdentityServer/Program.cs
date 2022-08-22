using Domain;
using IdentityServer.Quickstart;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().MigrateDatabase().Run();

            CreateHostBuilder(args).Build().Run();

            /*  var host= CreateHostBuilder(args).Build();

               using (var scope = host.Services.CreateScope())
               {
                   var userManager = scope.ServiceProvider
                       .GetRequiredService<UserManager<IdentityUser>>();

                   var user = new IdentityUser("bob");

                   userManager.CreateAsync(user, "bob").GetAwaiter().GetResult();
                   userManager.AddClaimAsync(user.Id,new Claim("admin","admin"))
                       .GetAwaiter().GetResult();


               }

               host.Run();*/
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
