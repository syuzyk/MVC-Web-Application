using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;



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
        [Required(ErrorMessage = "PLEASE ENTER YOUR FIRST NAME")]
        public string CustFirstName { get; set; }
        [Required(ErrorMessage = "PLEASE ENTER YOUR LAST NAME")]
        public string CustLastName { get; set; }
        [Required(ErrorMessage = "PLEASE ENTER YOUR ADDRESS")]
        public string CustAddress { get; set; }
        [Required(ErrorMessage = "PLEASE ENTER YOUR CITY")]
        public string CustCity { get; set; }
        [Required(ErrorMessage = "PLEASE ENTER YOUR PROVINCE")]
        [MaxLength(2)]
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

    public class CustomerManager
    {
        public static List<Customer> Registration()
        {
            var context = new TravelExpertsContext();
            var customers = context.Customers.Include(c => c.CustomersAuthentication).ToList();
            return customers;
        }

        public static void Add(Customer c)
        {
            var context = new TravelExpertsContext();
            context.Customers.Add(c);
            context.SaveChanges();
        }

        public static Customer GetAuthenticatedCustomerByID(int id)
        {
            var context = new TravelExpertsContext();
            var cust = context.Customers.Include(ca => ca.CustomersAuthentication).SingleOrDefault(c => c.CustomerId == id);
            return cust;
        }

        public static void Update(int id, Customer c)
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


            context.SaveChanges();
        }
    }
}
