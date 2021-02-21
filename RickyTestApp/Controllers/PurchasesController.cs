using Group8.TravelExperts.Data.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RickyTestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RickyTestApp.Controllers
{
    //Ricky wrote this code.
    public class PurchasesController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            var packagesPurchased = BookingDetailsManager.GetPurchasedPackages((int)TempData.Peek("CustomerId"));

            ViewData["Packages"] = packagesPurchased.Select(pkg => new PurchaseViewModel
            {
                BookingDetailId = pkg.BookingDetailId,
                BookingNo = pkg.BookingNo,
                Destination = pkg.Destination,
                IsPaid = pkg.IsPaid,
                PkgName = pkg.PkgName,
                ProdName = pkg.ProdName,
                SupName = pkg.SupName,
                TotalPrice = pkg.TotalPrice,
                TripEnd = pkg.TripEnd,
                TripStart = pkg.TripStart
            });

            var productsPurchased = BookingDetailsManager.GetPurchasedProducts((int)TempData.Peek("CustomerId"));

            ViewData["Products"] = productsPurchased.Select(prod => new PurchaseViewModel
            {
                BookingDetailId = prod.BookingDetailId,
                BookingNo = prod.BookingNo,
                Destination = prod.Destination,
                IsPaid = prod.IsPaid,
                PkgName = prod.PkgName,
                ProdName = prod.ProdName,
                SupName = prod.SupName,
                TotalPrice = prod.TotalPrice,
                TripEnd = prod.TripEnd,
                TripStart = prod.TripStart
            });

            TempData["Amount Owing"] = BookingDetailsManager.GetTotalOwing((int)TempData.Peek("CustomerId"));

            return View();
        }

        [HttpPost]
        public IActionResult Add(PurchaseModel purchase)
        {
            return View();
        }

        public ActionResult CancelProductOrder (int bookingDetailsId, string prodName, string supName, DateTime tripStart)
        {
            DateTime today = DateTime.Today;
            int daysUntilTripStart = (tripStart - today).Days;
            if (daysUntilTripStart < 4)
                TempData["msg"] = "<script>alert('Today is too close to the trip's start date. Please contact your agent.');</script>";
                   
            else
            {
                try
                {
                    BookingDetailsManager.DeleteOrder(bookingDetailsId);
                    TempData["msg"] = "<script>alert('Your order for " + prodName + " provided by " + supName + " has been canceled.');</script>";
                }
                catch
                {
                    TempData["msg"] = "<script>alert('An issue occured when cancelling your order. Please try again later or contact your agent.');</script>";
                } 
            }
                
            return RedirectToAction("Index", "Purchases");
        }

        public ActionResult CancelPackageOrder(int bookingDetailsId, string pkgName, DateTime tripStart)
        {
            DateTime today = DateTime.Today;
            int daysUntilTripStart = (tripStart - today).Days;
            if (daysUntilTripStart < 4)
                TempData["msg"] = "<script>alert('Today is too close to the trip's start date. Please contact your agent.');</script>";

            else
            {
                try
                {
                    BookingDetailsManager.DeleteOrder(bookingDetailsId);
                    TempData["msg"] = "<script>alert('Your order for " + pkgName + " has been canceled.');</script>";
                }
                catch
                {
                    TempData["msg"] = "<script>alert('An issue occured when cancelling your order. Please try again later or contact your agent.');</script>";
                }
            }

            return RedirectToAction("Index", "Purchases");
        }

        public ActionResult RequestRefund(int bookingDetailsId)
        {
            try
            {
                BookingDetailsManager.RequestRefund(bookingDetailsId);
                TempData["msg"] = "<script>alert('Your request for a refund has been submitted.');</script>";
            }
            catch
            {
                TempData["msg"] = "<script>alert('An issue occured when requesting your refund. Please try again later or contact your agent.');</script>";
            }

            return RedirectToAction("Index", "Purchases");
        }
    }
}
