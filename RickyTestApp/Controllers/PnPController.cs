using Group8.TravelExperts.Data.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RickyTestApp.Controllers
{
    public class PnPController : Controller
    {
        public IActionResult Index()
        {
            var packagesToSell = PackageManager.GetAllPackages();

            ViewData["Packages To Sell"] = packagesToSell.Select(pkg => new PnPViewModel
            {
                PackageId = pkg.PackageId,
                PkgName = pkg.PkgName,
                ProdName = pkg.ProdName,
                SupName = pkg.SupName,
                Destination = pkg.Destination,
                TripStart = pkg.TripStart,
                TripEnd = pkg.TripEnd,
                BasePrice = pkg.BasePrice,
                FeeName = pkg.FeeName,
                FeeAmt = pkg.FeeAmt,
                TotalPrice = pkg.TotalPrice
            });

            return View();
        }

        [Authorize]
        public ActionResult Add(int packageId, int customerId)
        {
            try
            {
                BookingDetailsManager.AddPackageOrder(packageId, customerId);
                return RedirectToAction("Index", "Purchases");
            }
            catch
            {
                return View();
            }
        }
    }
 }

