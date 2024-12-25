using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using StackOverFlow.Models;

namespace StackOverFlow.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signinManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signinManager)
        {
            this.userManager = userManager;
            this.signinManager = signinManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser identityUser = new IdentityUser
                {
                    UserName = userModel.Email,
                    Email = userModel.Email,
                };   
                var result = await userManager.CreateAsync(identityUser, userModel.Password);
                if (result.Succeeded)
                {
                    await signinManager.SignInAsync(identityUser, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var Error in result.Errors)
                    {
                        ModelState.AddModelError("", Error.Description);
                    }
                }
            }
            return View(userModel);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var result = await signinManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, loginModel.RememberMe, false);
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginModel.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return LocalRedirect(loginModel.ReturnUrl); 
                    }
                }
            }
            return View(loginModel);
        }
        public async Task<ActionResult> Logout()
        {
            await signinManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
