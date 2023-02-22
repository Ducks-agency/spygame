using System.Runtime.InteropServices.JavaScript;
using DucksAgency.Spygame.Clientportal.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace DucksAgency.Spygame.Clientportal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpGet("check")]
        public IActionResult OnGet()
        {
            return Ok("ok");
        }

        [HttpPost ("login")]
        public async Task<IActionResult> LoginAsync(User user)
        {
            try
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                return Ok();
            }

            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
