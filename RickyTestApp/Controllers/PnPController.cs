using Group8.TravelExperts.Data.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RickyTestApp.Controllers
{
    //Ricky wrote this code.
    public class PnPController : Controller
    {
        public IActionResult Index()
        {
            var packagesToSell = PackageManager.GetAllPackages();

            ViewData["Packages To Sell"] = packagesToSell.Select(pkg => new PnPViewModel
            {
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

            var productsToSell = ProductsDisplayedManager.GetAllProductsDisplayed();

            ViewData["Products To Sell"] = productsToSell.Select(prod => new PnPViewModel
            {
                PkgName = prod.PkgName,
                ProdName = prod.ProdName,
                SupName = prod.SupName,
                Destination = prod.Destination,
                TripStart = prod.TripStart,
                TripEnd = prod.TripEnd,
                BasePrice = prod.BasePrice,
                FeeName = prod.FeeName,
                FeeAmt = prod.FeeAmt,
                TotalPrice = prod.TotalPrice
            });

            return View();
        }

        public ActionResult OrderPackage(int customerId, string pkgName, decimal basePrice, DateTime tripStart, DateTime tripEnd)
        {
            try
            {
                BookingDetailsManager.AddPackageOrder(customerId, pkgName, basePrice, tripStart, tripEnd);
                return RedirectToAction("Index", "Purchases");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult OrderProduct(int customerId, string prodName, string supName, string destination, decimal basePrice, string feeName, decimal feeAmt, DateTime tripStart, DateTime tripEnd)
        {
            try
            {
                BookingDetailsManager.AddProductOrder(customerId, prodName, supName, destination, basePrice, feeName, feeAmt, tripStart, tripEnd);
                return RedirectToAction("Index", "Purchases");
            }
            catch
            {
                return View();
            }
        }
    }
 }

