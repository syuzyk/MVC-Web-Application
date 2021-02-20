using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group8.TravelExperts.Data.Domain
{
    public class PurchaseModel
    {
        public string BookingNo { get; set; }
        [Display(Name = "Package Name")]
        public string PkgName { get; set; }
        [Display(Name = "Product Type")]
        public string ProdName { get; set; }
        [Display(Name = "Provided by")]
        public string SupName { get; set; }
        public string Destination { get; set; }
        [Display(Name = "Trip Start Date")]
        public DateTime? TripStart { get; set; }
        [Display(Name = "Trip End Date")]
        public DateTime? TripEnd { get; set; }
        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }
        [Display(Name = "Customer has paid")]
        public string IsPaid { get; set; }
    }
}
