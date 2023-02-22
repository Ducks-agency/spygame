using System.Net;
using System.Security.Claims;
using DucksAgency.Spygame.Clientportal.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DucksAgency.Spygame.Clientportal.Pages
{
    public partial class LoginModel : PageModel
    {
        [BindProperty]
        public string UserName { get; set; } = String.Empty;

        [CascadingParameter]
        private Task<AuthenticationState>? authenticationStateTask { get; set; }

        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        public void OnGet()
        {
        
        }

        public async Task<IActionResult> OnPost()
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, UserName),
            }, CookieAuthenticationDefaults.AuthenticationScheme);
            var user = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);

            //if (HttpContext!.User.Identity!.IsAuthenticated)
            // return new RedirectToPageResult("/room");
            return LocalRedirect("~/room");
            //return Page();
        }

        public async Task SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
