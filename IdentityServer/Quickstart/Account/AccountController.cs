
using AutoMapper;
using Domain;
using IdentityModel;
using IdentityServer;
using IdentityServer.Quickstart.Account;
using IdentityServer.Quickstart.UserRegistration;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Test;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServerHost.Quickstart.UI
{

   
    [SecurityHeaders]
    [AllowAnonymous]

    public class AccountController : Controller
    {
        
        private readonly TestUserStore _users;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IEventService _events;

        private readonly SignInManager<Person> _signInManager;
        private readonly Microsoft.AspNetCore.Identity.UserManager<Person> _userManager;
        private readonly IIdentityServerInteractionService _interactionService;

        public AccountController(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events, UserManager<Person> userManager,
            SignInManager<Person> signInManager,
            IIdentityServerInteractionService interactionService,
            TestUserStore users = null)
        {
           
            _users = users ?? new TestUserStore(TestUsers.Users);
           
            _interaction = interaction;
            _clientStore = clientStore;
            _schemeProvider = schemeProvider;
            _events = events;
            _signInManager = signInManager;
            _userManager = userManager;
            _interactionService = interactionService;
        }

    
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            #region staro
            // build a model so we know what to show on the login page


            /*  var vm = await BuildLoginViewModelAsync(returnUrl);

                if (vm.IsExternalLoginOnly)
                {
                 // we only have one option for logging in and it's an external provider
                 return RedirectToAction("Challenge", "External", new { scheme = vm.ExternalLoginScheme, returnUrl });

                }

                return View(vm);*/
            #endregion
            var externalProviders = await _signInManager.GetExternalAuthenticationSchemesAsync();
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl,
              
                ///ExternalProviderss = externalProviders,nije reseno
               
       
            });  

        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            

            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Succeeded)
            {

                return Redirect(model.ReturnUrl);
            }
            else
            {
               
                ModelState.AddModelError(string.Empty, "Wrong credentials!");
               
                model.EnableLocalLogin = true;
              
            }
           

            return View(model);
        }


       
        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            
            var vm = await BuildLogoutViewModelAsync(logoutId);

            if (vm.ShowLogoutPrompt == false)
            {
               
                return await Logout(vm);
            }

            return View(vm);

           

        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            var vm = new RegisterViewModel() { ReturnUrl = returnUrl };
            return View(vm);
        }

        public async Task<IActionResult> ExternalLogin(string provider, string returnUrl )
        {

             var redirectUrii = Url.Action(nameof(ExteranlLoginCallback), "Account", new { ReturnUrl = returnUrl });

        
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrii);

       
           
            return Challenge(properties, provider);
        }

      
        public async Task<IActionResult> ExteranlLoginCallback(string returnUrl)
        {

            var info = await _signInManager.GetExternalLoginInfoAsync();/////ovde se pravi problem jer vraca null

            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }
            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (signInResult.Succeeded)
            {
                return Redirect(returnUrl);
            }
            /*if (signInResult.IsLockedOut)
            {
                //return RedirectToAction(nameof(ForgotPassword));
            }*/
            else
            {
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["Provider"] = info.LoginProvider;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                return View("ExternalLogin", new ExternalLoginModel { Email = email });
            }




        }

        public async Task<IActionResult> ExternalRegister(ExternalRegisterViewModel vm)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction("Login");
            }
            Person user = new Person();
            user.UserName = vm.Username;
            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                return View(vm);
            }

           result = await _userManager.AddLoginAsync(user, info);

            if (!result.Succeeded)
            {
                return View(vm);
            }
          await  _signInManager.SignInAsync(user, false);

            return Redirect(vm.ReturnUrl);
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (vm.Role == "User")
            {
                User user = new User();
                user.UserName = vm.Username;
                user.FirstName = vm.FirstName;
                user.LastName = vm.LastName;
                user.Email = vm.Email;
                user.DateOfRegistration = DateTime.Now;
                var result = await _userManager.CreateAsync(user, vm.Password);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
                var res = await _userManager.AddToRoleAsync(user, vm.Role);
                if (result.Succeeded)
                {
                    // await _signInManager.SignInAsync(user, false);

                    return Redirect(vm.ReturnUrl);///odmah na mvc vraca, akad ga redirektujem na login opet tad nece
                }
            }

            if (vm.Role == "Admin")
            {
                Admin user = new Admin();
                user.UserName = vm.Username;
                user.FirstName = vm.FirstName;
                user.LastName = vm.LastName;
                user.Email = vm.Email;
                var result = await _userManager.CreateAsync(user, vm.Password);

                
                var res = await _userManager.AddToRoleAsync(user, vm.Role);
                if (result.Succeeded)
                {
                    // await _signInManager.SignInAsync(user, false);

                    return Redirect(vm.ReturnUrl);///odmah na mvc vraca, akad ga redirektujem na login opet tad nece
                }
            }




            return View();
        }

        /// <summary>
        /// Handle logout page postback
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogoutInputModel model)
        {
            #region staro
            // build a model so the logged out page knows what to display
            /* var vm = await BuildLoggedOutViewModelAsync(model.LogoutId);

             if (User?.Identity.IsAuthenticated == true)
             {
                 // delete local authentication cookie
                 await HttpContext.SignOutAsync();

                 // raise the logout event
                 await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
             }

             // check if we need to trigger sign-out at an upstream identity provider
             if (vm.TriggerExternalSignout)
             {
                 // build a return URL so the upstream provider will redirect back
                 // to us after the user has logged out. this allows us to then
                 // complete our single sign-out processing.
                 string url = Url.Action("Logout", new { logoutId = vm.LogoutId });

                 // this triggers a redirect to the external provider for sign-out
                 return SignOut(new AuthenticationProperties { RedirectUri = url }, vm.ExternalAuthenticationScheme);
             }

             return View("LoggedOut", vm);*/
            #endregion
            await _signInManager.SignOutAsync();

            var logoutRequest = await _interactionService.GetLogoutContextAsync(model.LogoutId);

            if (string.IsNullOrEmpty(logoutRequest.PostLogoutRedirectUri))
            {
                return RedirectToAction("Login", "Account");
            }

            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        /*STARO
        /*****************************************/
        /* helper APIs for the AccountController */
        /*****************************************/

        #region STARO
        
      /* private async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl)
       {
           var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
           if (context?.IdP != null && await _schemeProvider.GetSchemeAsync(context.IdP) != null)
           {
               var local = context.IdP == IdentityServer4.IdentityServerConstants.LocalIdentityProvider;

               // this is meant to short circuit the UI and only trigger the one external IdP
               var vm = new LoginViewModel
               {
                   EnableLocalLogin = local,
                   ReturnUrl = returnUrl,
                   Username = context?.LoginHint,
               };

               if (!local)
               {
                   vm.ExternalProviders = new[] { new ExternalProvider { AuthenticationScheme = context.IdP } };
               }

               return vm;
           }

           var schemes = await _schemeProvider.GetAllSchemesAsync();

           var providers = schemes
               .Where(x => x.DisplayName != null)
               .Select(x => new ExternalProvider
               {
                   DisplayName = x.DisplayName ?? x.Name,
                   AuthenticationScheme = x.Name
               }).ToList();

           var allowLocal = true;
           if (context?.Client.ClientId != null)
           {
               var client = await _clientStore.FindEnabledClientByIdAsync(context.Client.ClientId);
               if (client != null)
               {
                   allowLocal = client.EnableLocalLogin;

                   if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
                   {
                       providers = providers.Where(provider => client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();
                   }
               }
           }

           return new LoginViewModel
           {
               AllowRememberLogin = AccountOptions.AllowRememberLogin,
               EnableLocalLogin = allowLocal && AccountOptions.AllowLocalLogin,
               ReturnUrl = returnUrl,
               Username = context?.LoginHint,
               ExternalProviders = providers.ToArray()
           };
       }

       private async Task<LoginViewModel> BuildLoginViewModelAsync(LoginInputModel model)
       {
           var vm = await BuildLoginViewModelAsync(model.ReturnUrl);
           vm.Username = model.Username;
           vm.RememberLogin = model.RememberLogin;
           return vm;
       }*/
        
       private async Task<LogoutViewModel> BuildLogoutViewModelAsync(string logoutId)
       {
           var vm = new LogoutViewModel { LogoutId = logoutId, ShowLogoutPrompt = AccountOptions.ShowLogoutPrompt };

           if (User?.Identity.IsAuthenticated != true)
           {
               // if the user is not authenticated, then just show logged out page
               vm.ShowLogoutPrompt = false;
               return vm;
           }

           var context = await _interaction.GetLogoutContextAsync(logoutId);
           if (context?.ShowSignoutPrompt == false)
           {
               // it's safe to automatically sign-out
               vm.ShowLogoutPrompt = false;
               return vm;
           }

           // show the logout prompt. this prevents attacks where the user
           // is automatically signed out by another malicious web page.
           return vm;
       }
        /*
       private async Task<LoggedOutViewModel> BuildLoggedOutViewModelAsync(string logoutId)
       {
           // get context information (client name, post logout redirect URI and iframe for federated signout)
           var logout = await _interaction.GetLogoutContextAsync(logoutId);

           var vm = new LoggedOutViewModel
           {
               AutomaticRedirectAfterSignOut = AccountOptions.AutomaticRedirectAfterSignOut,
               PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
               ClientName = string.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId : logout?.ClientName,
               SignOutIframeUrl = logout?.SignOutIFrameUrl,
               LogoutId = logoutId
           };

           if (User?.Identity.IsAuthenticated == true)
           {
               var idp = User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
               if (idp != null && idp != IdentityServer4.IdentityServerConstants.LocalIdentityProvider)
               {
                   var providerSupportsSignout = await HttpContext.GetSchemeSupportsSignOutAsync(idp);
                   if (providerSupportsSignout)
                   {
                       if (vm.LogoutId == null)
                       {
                           // if there's no current logout context, we need to create one
                           // this captures necessary info from the current logged in user
                           // before we signout and redirect away to the external IdP for signout
                           vm.LogoutId = await _interaction.CreateLogoutContextAsync();
                       }

                       vm.ExternalAuthenticationScheme = idp;
                   }
               }
           }

           return vm;
       }*/
        #endregion
    }
}