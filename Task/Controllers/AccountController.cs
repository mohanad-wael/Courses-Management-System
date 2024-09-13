using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task.Models;
using Task.ModelViews;

namespace Task.Controllers
{
    public class AccountController : Controller
    {
		#region Fields
		private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
		#endregion

		#region Constructor
		public AccountController(UserManager<ApplicationUser> _userManager,SignInManager<ApplicationUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }
		#endregion

		#region Registration
		[HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegisterViewModel registerVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userModel= new ApplicationUser();
                userModel.Email = registerVM.Email;
                userModel.FullName= $"{registerVM.FirstName} {registerVM.LastName}";
                userModel.UserName = registerVM.UserName;
                userModel.PasswordHash = registerVM.Password;

                IdentityResult result= await userManager.CreateAsync(userModel,registerVM.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(userModel, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(registerVM);
        }
		#endregion

		#region Login

		[HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginVM)
		{
            if(ModelState.IsValid)
            {
				ApplicationUser userModel = await userManager.FindByEmailAsync(loginVM.Email);

                if (userModel != null)
                {
                    bool passwordCheck= await userManager.CheckPasswordAsync(userModel, loginVM.Password);
                    if (passwordCheck)
                    {
						Microsoft.AspNetCore.Identity.SignInResult result= await signInManager.PasswordSignInAsync(userModel, loginVM.Password, loginVM.RememberMe, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
							ModelState.AddModelError("", "Email or password is incorrect.");
						}
                    }
                    else
                    {
						ModelState.AddModelError("", "Email or password is incorrect.");
					}
                }
                else
                {
					ModelState.AddModelError("", "Email or password is incorrect.");
				}


            }
			return View(loginVM);
		}
		#endregion

		#region SignOut
		public async Task<IActionResult> SignOut()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction("Login");
		}
		#endregion

	}
}
