using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace Zircon_Hobbies.Models

{
    public class AccountController : Controller
    {

        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();

            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

    }
}
