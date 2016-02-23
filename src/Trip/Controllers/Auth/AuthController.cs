using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using WorldTrip.Models;
using WorldTrip.ViewModels;

namespace WorldTrip.Controllers.Auth
{
    public class AuthController : Controller
    {
        private SignInManager<TripUser> _signInManager;

        public AuthController(SignInManager<TripUser>signInManager)
        {
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Trips", "App");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm,string returnURL)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password,true,false);

                if (signInResult.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(returnURL))
                    {
                        RedirectToAction("Trips", "App");
                    }
                    else
                    {
                        return Redirect(returnURL);
                    }
                    
                }
                else
                {
                    ModelState.AddModelError("","Username or Password is incorrect");
                }
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }

            return RedirectToAction("Index", "App");
        }
    }
}
