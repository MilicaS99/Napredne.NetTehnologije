using AutoMapper;
using Domain;
using IdentityServer.Configuration;
using IdentityServer4;
using IdentityServerHost.Quickstart.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security.Google;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .SetIsOriginAllowedToAllowWildcardSubdomains()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
            //proba
            var connectionString = Configuration.GetConnectionString("sqlConnection");

            services.AddDbContext<Context>(config =>
            {
                config.UseSqlServer(connectionString);
                //config.UseInMemoryDatabase("Memory");
            });

            // AddIdentity registers the services

          

            services.AddIdentity<Person, IdentityRole<int>>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            
            })
                .AddEntityFrameworkStores<Context>().AddDefaultTokenProviders();

            // kraj probe

           services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "IdentityServer.Cookie";
                config.LoginPath = "/Account/Login";
                config.LogoutPath = "/Account/Logout";
                config.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                config.SlidingExpiration = true;
                
            });
            services.AddIdentityServer(options =>
             {
             }).AddAspNetIdentity<Person>()
                     .AddInMemoryIdentityResources(InMemoryConfig.IdentityResources)
                     .AddInMemoryApiScopes(InMemoryConfig.ApiScopes)
                     .AddInMemoryClients(InMemoryConfig.Clients)
                     //.AddTestUsers(TestUsers.Users)
                     .AddDeveloperSigningCredential();

            
            services.AddAuthentication().AddGoogle("Google", options =>
             {

                  // options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                  options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                 options.ClientId = "649453208247-5e306d85tffordccmfdt9m1ojah1vjf5.apps.googleusercontent.com";

                 options.ClientSecret = "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";

                

             });

            
          

            #region baza
            /*var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddIdentityServer()
                .AddTestUsers(TestUsers.Users)
                .AddDeveloperSigningCredential() //not something we want to use in a production environment
                .AddConfigurationStore(opt =>
                {
                    opt.ConfigureDbContext = c => c.UseSqlServer(Configuration.GetConnectionString("sqlConnection"),
                        sql => sql.MigrationsAssembly(migrationAssembly));
                })
                .AddOperationalStore(opt =>
                {
                    opt.ConfigureDbContext = o => o.UseSqlServer(Configuration.GetConnectionString("sqlConnection"),
                        sql => sql.MigrationsAssembly(migrationAssembly));
                });*/
            #endregion

            //services.AddDbContext<Context>();
            // services.AddIdentity<Person, IdentityRole<int>>().AddEntityFrameworkStores<Context>();

            // services.AddAutoMapper();

            

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseCors("AllowAllOrigins");

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                Secure = CookieSecurePolicy.Always,
  

            }) ;

            app.UseRouting();

            app.UseIdentityServer();


         
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
