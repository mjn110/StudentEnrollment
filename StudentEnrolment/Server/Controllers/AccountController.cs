using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using StudentEnrolment.Client.ViewModels;
using StudentEnrolment.Shared.Models;

namespace StudentEnrolment.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return Unauthorized();
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("SignOut")]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpGet("GetAuthenticationState")]
        public IActionResult GetAuthenticationState() // Removed async as it is not needed
        {
            var user = _signInManager.Context.User; // Directly access the ClaimsPrincipal
            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                return Ok($"{user.Identity.Name} is authenticated.");
            }
            else
            {
                return Unauthorized("The user is NOT authenticated.");
            }
        }
    }
}
