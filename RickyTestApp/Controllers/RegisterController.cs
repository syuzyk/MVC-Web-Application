using Group8.TravelExperts.Data.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RickyTestApp.Controllers
{
    public class RegisterController : Controller
    {

        public ActionResult RegisterDetails()
        {
            ViewBag.cust = CustomerManager.Registration();
            List<SelectListItem> securityQuestions = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Text = "What is your mother's maiden name?",
                    Value = "What is your mother's maiden name?"
                },
                new SelectListItem
                {
                    Text = "Where did you attend high school?",
                    Value = "Where did you attend high school?"
                },
                new SelectListItem
                {
                    Text = "Why is Eric always dressed so nicely?",
                    Value = "Why is Eric always dressed so nicely?"
                },
                new SelectListItem
                {
                    Text = "What is your favourite TV show?",
                    Value = "What is your favourite TV show?"
                }
            };
            ViewBag.SecurityQuestions = securityQuestions;
            return View();
            
        }

        [HttpPost]
        
        public ActionResult RegisterDetails(Customer c)
        {
            List<SelectListItem> securityQuestions = new List<SelectListItem>()
                {
                    new SelectListItem
                    {
                        Text = "What is your mother's maiden name?",
                        Value = "What is your mother's maiden name?"
                    },
                    new SelectListItem
                    {
                        Text = "Where did you attend high school?",
                        Value = "Where did you attend high school?"
                    },
                    new SelectListItem
                    {
                        Text = "Why is Eric always dressed so nicely?",
                        Value = "Why is Eric always dressed so nicely?"
                    },
                    new SelectListItem
                    {
                        Text = "What is your favourite TV show?",
                        Value = "What is your favourite TV show?"
                    }
                };

            ViewBag.SecurityQuestions = securityQuestions;

            if (ModelState.IsValid)
            {
                try
                {
                    CustomerManager.Add(c);
                    
                    TempData["LoginPrompt"] = "<script>alert('Your account has been created. You may log in with your username and password.');</script>";
                    return RedirectToAction("Login", "Account");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

       [Authorize]
        public ActionResult Edit(int id)
        {
            Customer cust = CustomerManager.GetAuthenticatedCustomerByID(id);
            
            return View(cust);
        }


       
        [HttpPost]
        [Authorize]
        public ActionResult Edit(int id,Customer c)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CustomerManager.Update(id, c);
                    
                    return RedirectToAction("Index", "Home");
                }

                catch
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
            }
        [Authorize]
        public ActionResult EditAuth(int id)
        {
            CustomersAuthentication cust = CustomersAuthenticationManager.GetByCustomerId(id);
            return View(cust);
        }
        [Authorize]
        public ActionResult EditAuth1(int id,string user,string oldp,string newp,string confnewp)
        {
            //if (CustomersAuthenticationManager.UsernameIsTaken(user) == true)
            //{
            //    ViewBag.Message = "User Name " + user + " already exists";
            //    return View();
            //}
            //else
            //{
                var msg = "<h5 ";
                
                if (oldp != "" && newp != "" && newp != null && confnewp != "" && confnewp != null && newp == confnewp)
                {
                    if (CustomersAuthenticationManager.CheckOldPasswordThenUpdate(id, user, oldp, newp, confnewp) == true)
                        msg += "style='color:blue;'> Password changed successfully!</h5>";
                    else
                        msg += "style='color:red;'> Current password was entered incorrectly. Password was not updated.</p>";
                    
                    return Content(msg);
                }
            if (newp != "" && confnewp != "" && newp == confnewp)
                {
                    msg += "style='color:red;'> All fields must be filled in.</p>";
                    return Content(msg);
                }
            else
                {
                    msg += "style='color:red;'> New password must match.</p>";
                    return Content(msg);
                }
            //}
        }
    }


   
}

