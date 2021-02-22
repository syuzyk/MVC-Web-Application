using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group8.TravelExperts.Data.Domain;
using RickyTestApp.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace RickyTestApp.Controllers
{
    //Ricky wrote this code.
    public class AccountController : Controller
    {
        public IActionResult Login(string returnUrl = null)
        {
            if (returnUrl != null)
                TempData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(UserViewModel user)
        {
            var usr = CustomersAuthenticationManager.Authenticate(user.Username, user.Password);

            if (usr == null)
            {
                return View();
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, usr.Username),
                new Claim("CustomerId", usr.CustomerId.ToString()),
                new Claim("FirstName", usr.CustFirstName)
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");

            await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(claimsIdentity));

            TempData["CustomerId"] = usr.CustomerId;
            TempData["CustomerFName"] = usr.CustFirstName;

            if (TempData["ReturnUrl"] == null)
                return RedirectToAction("Index", "Home");
            else
                return Redirect(TempData["ReturnUrl"].ToString());
        }

        public async Task<IActionResult> LogoutAsync(UserViewModel user)
        {
            TempData.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); 
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SecurityQuestion(string username)
        {
            TempData["Security Question"] = CustomersAuthenticationManager.GetSecurityQuestion(username);
            TempData["Username"] = username;
            return View();
        }

        [HttpPost]
        public ActionResult GetLink(string sqAnswer1)
        {
            if (CustomersAuthenticationManager.SecurityQuestionAnsweredCorrect((string)TempData.Peek("Username"), sqAnswer1) == true)
            {
                return RedirectToAction("LinkSent", "Account");
            }
            else
            {
                TempData["msg"] = "Incorrect answer; please try again.";
                return RedirectToAction("SecurityQuestion", "Account");
            }
                
        }
    }
}
