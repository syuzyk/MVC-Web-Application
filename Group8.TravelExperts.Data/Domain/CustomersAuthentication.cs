using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

#nullable disable

namespace Group8.TravelExperts.Data.Domain
{
    public partial class CustomersAuthentication
    {
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "PLEASE ENTER A USERNAME")]
        [StringLength(25, ErrorMessage = "PLEASE ENTER USERNAME WITH LESS THAN 25 CHARECTER AND MORE THAN 5 CHARECTER")]
        [MinLength(5, ErrorMessage = "PLEASE ENTER USERNAME WITH LESS THAN 25 CHARECTER AND MORE THAN 5 CHARECTER")]
        public string Username { get; set; }
        [Required(ErrorMessage = "PLEASE ENTER A PASSWORD")]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "PLEASE RE-ENTER YOUR PASSWORD")]
        [Compare("Password", ErrorMessage = "Passwords do not match. Try again.")]
        public string ConfirmPassword { get; set; }
        public string SecurityQuestion1 { get; set; }
        [Required(ErrorMessage = "Please submit an answer")]
        public string SQAnswer1 { get; set; }
        public virtual Customer Customer { get; set; }
    }

    //Ricky added this code.
    public class CustomersAuthenticationManager
    {
        public static ClaimsModel Authenticate(string username, string password)
        {
            TravelExpertsContext context = new TravelExpertsContext();
            var user = context.CustomersAuthentications.SingleOrDefault(usr => usr.Username == username && usr.Password == password);

            if (user != null)
            {
                int customerId = user.CustomerId;

                var customer = context.Customers.SingleOrDefault(cust => cust.CustomerId == customerId);
                string fName = customer.CustFirstName;

                ClaimsModel claims = new ClaimsModel()
                {
                    Username = username,
                    CustomerId = customerId,
                    CustFirstName = fName
                };

                return claims;
            }
            else
                return null;  
        }
    }         
}