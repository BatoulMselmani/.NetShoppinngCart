using Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cms.Controllers
{
   // [Authorize] // need to login
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private IPasswordHasher<AppUser> passwordHasher;
        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IPasswordHasher<AppUser> passwordHasher)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.passwordHasher = passwordHasher;
        }
        
        
        
        //Get /account/register
        [AllowAnonymous] //allow anyone to access
        public IActionResult Register() => View();

        //Post /account/register
        [HttpPost]
        [AllowAnonymous] //allow anyone to access
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser
                {
                    UserName = user.UserName,
                    Email = user.Email
                };
                IdentityResult result = await userManager.CreateAsync(appUser, user.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach(IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError(" ", error.Description);
                    }
                }
            }
            return View(user);
        }
        //Get /account/login
        [AllowAnonymous] //allow anyone to access
        public IActionResult Login(string returnUrl)
        {
            Login login = new Login
            {
                ReturnUrl = returnUrl
            };
            return View(login);

        }

        //Post /account/login
        [HttpPost]
        [AllowAnonymous] //allow anyone to access
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = await userManager.FindByEmailAsync(login.Email);
                if(appUser != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync
                            (appUser, login.Password, false, false);
                    if(result.Succeeded) 
                       return Redirect(login.ReturnUrl ?? "/");
                  
                }
                ModelState.AddModelError("", "Login Failed, wrong credemtials");

               
            }
            return View(login);
        }

        //Get /account/logout
         
      public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return Redirect("/"); //return to home

        }

        //Get /account/edit

        public async Task<IActionResult> Edit()
        {
            AppUser appUser = await userManager.FindByNameAsync(User.Identity.Name);
            UserEdit user = new UserEdit(appUser);
            

            return View(user);

        }

        //Post /account/edit
        [HttpPost]
        [AllowAnonymous] //allow anyone to access
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEdit user)
        {
            AppUser appUser = await userManager.FindByNameAsync(User.Identity.Name);

            if (ModelState.IsValid)
            {
                appUser.Email = user.Email;

                if (user.Password != null)
                {
                    appUser.PasswordHash = passwordHasher.HashPassword(appUser, user.Password);


                }
                IdentityResult result = await userManager.UpdateAsync(appUser);
                if(result.Succeeded)
                    TempData["Success"] = "Your information has been edited!";
                  

            }
            return View();
        }


    }
}
