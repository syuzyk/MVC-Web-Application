using System;
using System.Collections.Generic;

#nullable disable

namespace Group8.TravelExperts.Data.Domain
{
    public partial class BookingDetail
    {
        public int BookingDetailId { get; set; }
        public double? ItineraryNo { get; set; }
        public DateTime? TripStart { get; set; }
        public DateTime? TripEnd { get; set; }
        public string Description { get; set; }
        public string Destination { get; set; }
        public decimal? BasePrice { get; set; }
        public decimal? AgencyCommission { get; set; }
        public int? BookingId { get; set; }
        public string Region { get; set; }
        public string Class { get; set; }
        public string FeeName { get; set; }
        public decimal? FeeAmt { get; set; }
        public string ProdName { get; set; }
        public string SupName { get; set; }
        public string PkgName { get; set; }
        public string IsPaid { get; set; }

        public virtual Booking Booking { get; set; }
    }
}
