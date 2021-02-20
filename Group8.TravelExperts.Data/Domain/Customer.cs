using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



#nullable disable

namespace Group8.TravelExperts.Data.Domain
{
    public partial class Customer
    {
        public Customer()
        {
            Bookings = new HashSet<Booking>();
            CreditCards = new HashSet<CreditCard>();
            CustomersRewards = new HashSet<CustomersReward>();
        }

        public int CustomerId { get; set; }
        [Required (ErrorMessage ="PLEASE ENETER YOUR FIRST NAME")]
        public string CustFirstName { get; set; }
        [Required(ErrorMessage = "PLEASE ENETER YOUR LAST NAME")]
        public string CustLastName { get; set; }
        [Required(ErrorMessage = "PLEASE ENETER YOUR ADDRESS")]
        public string CustAddress { get; set; }
        [Required(ErrorMessage = "PLEASE ENETER YOUR CITY")]
        public string CustCity { get; set; }
        [Required(ErrorMessage = "PLEASE ENETER YOUR PROVIENCE")]
        public string CustProv { get; set; }
        [Required(ErrorMessage = "PLEASE ENETER YOUR POSTAL CODE")]
        public string CustPostal { get; set; }
        [Required(ErrorMessage = "PLEASE ENETER YOUR PHONE NUMBER")]
        public string CustHomePhone { get; set; }
       
        public string CustBusPhone { get; set; }
        
        public string CustFax { get; set; }
        [Required(ErrorMessage = "PLEASE ENETER YOUR E-MAIL ADDRESS")]
        public string CustEmail { get; set; }
        public int? AgentId { get; set; }

        public virtual Agent Agent { get; set; }
        public virtual CustomersAuthentication CustomersAuthentication { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<CreditCard> CreditCards { get; set; }
        public virtual ICollection<CustomersReward> CustomersRewards { get; set; }
    }
}
