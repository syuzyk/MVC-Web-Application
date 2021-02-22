using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        //Handles the logout ~ TS
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

        public ActionResult SecurityQuestion(string username)
        {
            if (username == null)
            {
                TempData["UsernameError"] = "<tr style='color:red'><td>Please enter a username.</td></tr>"; 
                return RedirectToAction("ForgotPassword");
            }
            else
            {
                string question = CustomersAuthenticationManager.GetSecurityQuestion(username);

                if (question == null)
                {
                    TempData["UsernameError"] = "<tr style='color:red'><td>Invalid username.<br />Please check your spelling.</td></tr>";
                    return RedirectToAction("ForgotPassword");
                }
                else
                {
                    TempData["Username"] = username;
                    TempData["Security Question"] = question;
                    return View();
                }
            }
        }

        public ActionResult GetLink(string attempt)
        {
            if (attempt == null)
            {
                TempData["AnswerResponse"] = "<tr style='color:red'><td>Please enter an answer.</td></tr>";
                return RedirectToAction("SecurityQuestion", new { username = (string)TempData.Peek("Username") });
            }
            else if (CustomersAuthenticationManager.SecurityQuestionAnsweredCorrect((string)TempData.Peek("Username"), attempt) == true)
            {
                return RedirectToAction("LinkSent", "Account");
            }
            else
            {
                TempData["AnswerResponse"] = "<tr style='color:red'><td>Incorrect answer; please try again.</td></tr>";
                return RedirectToAction("SecurityQuestion", new { username = (string)TempData.Peek("Username") });
            }   
        }

        public ActionResult LinkSent()
        {
            return View();
        }
    }
}
