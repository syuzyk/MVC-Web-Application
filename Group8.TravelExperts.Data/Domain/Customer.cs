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
        [Required (ErrorMessage ="PLEASE ENTER YOUR FIRST NAME")]
        public string CustFirstName { get; set; }
        [Required(ErrorMessage = "PLEASE ENTER YOUR LAST NAME")]
        public string CustLastName { get; set; }
        [Required(ErrorMessage = "PLEASE ENTER YOUR ADDRESS")]
        public string CustAddress { get; set; }
        [Required(ErrorMessage = "PLEASE ENTER YOUR CITY")]
        public string CustCity { get; set; }
        [Required(ErrorMessage = "PLEASE ENTER YOUR PROVINCE")]
        public string CustProv { get; set; }
        [Required(ErrorMessage = "PLEASE ENTER YOUR POSTAL CODE")]
        [RegularExpression(@"^[A-Za-z]\d[A-Za-z] ?\d[A-Za-z]\d$", ErrorMessage = "PLEASE ENTER A VAILD POSTAL CODE")]
        public string CustPostal { get; set; }
        [Required(ErrorMessage = "PLEASE ENTER YOUR PHONE NUMBER")]
        [RegularExpression(@"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$", ErrorMessage = "PLEASE ENTER A VAILD PHONE NUMBER")]
        public string CustHomePhone { get; set; }
       
        public string CustBusPhone { get; set; }
        
        public string CustFax { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "PLEASE ENTER A VAILD EMAIL ADDRESS")]                                                                                                         
        public string CustEmail { get; set; }
        public int? AgentId { get; set; }

        public virtual Agent Agent { get; set; }
        public virtual CustomersAuthentication CustomersAuthentication { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<CreditCard> CreditCards { get; set; }
        public virtual ICollection<CustomersReward> CustomersRewards { get; set; }
    }
}
