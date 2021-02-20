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
    
    public class PurchasesController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            var packagesPurchased = BookingDetailsManager.GetPurchasedPackages((int)TempData.Peek("CustomerId"));

            ViewData["Packages"] = packagesPurchased.Select(pkg => new PurchaseViewModel
            {
                BookingNo = pkg.BookingNo,
                Class = pkg.Class,
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
                BookingNo = prod.BookingNo,
                Class = prod.Class,
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
    }
}
