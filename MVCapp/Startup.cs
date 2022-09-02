using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using DataAccessLayer.UnitOfWork;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MVCapp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true;

            services.AddControllersWithViews();

       

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies")
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = "http://localhost:5001";

                options.SignInScheme = "Cookies";

                options.ClientId = "mvc";
                options.ClientSecret = "secret";
                options.ResponseType = "code";

                

                options.CallbackPath = "/signin-oidc";

                options.Scope.Add("api1");
                options.Scope.Add("openid");
                
                options.Scope.Add("profile");
                options.GetClaimsFromUserInfoEndpoint = true;
                
                options.RequireHttpsMetadata = false;
                options.SaveTokens = true;
                options.Scope.Add("offline_access");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = JwtClaimTypes.GivenName,
                    ValidateIssuer = true
                };
                options.Scope.Add("roles");
                options.ClaimActions.MapUniqueJsonKey("role", "role");

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    RoleClaimType = "role"
                };

            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<Context>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                Secure = CookieSecurePolicy.Always
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute()
                    .RequireAuthorization();
            });
        }
    }
}
