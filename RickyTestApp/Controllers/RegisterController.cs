using Group8.TravelExperts.Data.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var context = new TravelExpertsContext();
            var cutomers = context.Customers.Include(c => c.CustomersAuthentication).ToList();
            ViewBag.cust = cutomers;
            return View();
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterDetails(Customer c)
        {
            
                try
                {
                    var context = new TravelExpertsContext();
                    context.Customers.Add(c);
                    context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                    return View();
                }
            

        }

       [Authorize]
        public ActionResult Edit(int id)
        {
            var context = new TravelExpertsContext();
            var cust = context.Customers.Include(ca => ca.CustomersAuthentication).SingleOrDefault(c => c.CustomerId == id);
            return View(cust);
        }


       
        [HttpPost]
        [Authorize]
        public ActionResult Edit(int id,Customer c)
        {

            try
            {

                var context = new TravelExpertsContext();
                var u = context.Customers.SingleOrDefault(cp => cp.CustomerId == id);

                u.CustFirstName = c.CustFirstName;
                u.CustLastName = c.CustLastName;
                u.CustAddress = c.CustAddress;
                u.CustCity = c.CustCity;
                u.CustProv = c.CustProv;
                u.CustPostal = c.CustPostal;
                u.CustHomePhone = c.CustHomePhone;
                u.CustBusPhone = c.CustBusPhone;
                u.CustFax = c.CustFax;
                u.CustEmail = c.CustEmail;

                // u.CustomersAuthentication.Username = c.CustomersAuthentication.Username;
                //u.CustomersAuthentication.Password = c.CustomersAuthentication.Password;
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
               
            }
        [Authorize]
        public ActionResult EditAuth(int id)
        {
            var context = new TravelExpertsContext();
            var cust = context.CustomersAuthentications.SingleOrDefault(c => c.CustomerId == id);
            return View(cust);
        }
        [Authorize]
        public ActionResult EditAuth1(int id,string user,string oldp,string newp)
        {
            var context = new TravelExpertsContext();
            var u = context.CustomersAuthentications.SingleOrDefault(cp => cp.CustomerId == id);
            var msg = "<h5 ";
            if(u.Password == oldp)
            {
                u.Username = user;
                u.Password = newp;
                msg += "style='color:blue;'> Password changed successfully!</h5>";
            }
            else
            {
                msg += "style='color:red;'> Current password was entered incorrectly. Password was not updated.</p>";
            }
            context.SaveChanges();
            return Content(msg);
         
        }
    }


    //// GET: RegisterController/Delete/5
    //public ActionResult Delete(int id)
    //{
    //    return View();
    //}

    //// POST: RegisterController/Delete/5
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult Delete(int id, IFormCollection collection)
    //{
    //    try
    //    {
    //        return RedirectToAction(nameof(Index));
    //    }
    //    catch
    //    {
    //        return View();
    //    }
    //}
}

