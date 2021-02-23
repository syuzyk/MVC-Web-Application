﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

#nullable disable

namespace Group8.TravelExperts.Data.Domain
{
    //Stephen and Tristen added these validations
    public partial class CustomersAuthentication
    {
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Please enter a username.")]
        [StringLength(25, ErrorMessage = "Please enter a username between 5 and 25 characters.")]
        [MinLength(5, ErrorMessage = "Please enter a username between 5 and 25 characters.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter a password.")]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Please re-enter your password.")]
        [Compare("Password", ErrorMessage = "Passwords do not match. Please try again.")]
        public string ConfirmPassword { get; set; }
        public string SecurityQuestion1 { get; set; }
        [Required(ErrorMessage = "Please submit an answer.")]
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

        public static string GetSecurityQuestion(string username)
        {
            TravelExpertsContext context = new TravelExpertsContext();

            CustomersAuthentication target = context.CustomersAuthentications.SingleOrDefault(c => c.Username == username);

            string question;

            if (target == null)
                question = null;
            else
                question = target.SecurityQuestion1;

            return question;
        }

        public static bool SecurityQuestionAnsweredCorrect(string username, string answer)
        {
            TravelExpertsContext context = new TravelExpertsContext();
            CustomersAuthentication target = context.CustomersAuthentications.SingleOrDefault(c => c.Username == username);

            if (target.SQAnswer1 == answer)
                return true;
            else
                return false;
        }

        public static CustomersAuthentication GetByCustomerId(int id)
        {
            var context = new TravelExpertsContext();
            var cust = context.CustomersAuthentications.SingleOrDefault(c => c.CustomerId == id);
            
            return cust;
        }

        public static bool UsernameIsTaken(string user)
        {
            var context = new TravelExpertsContext();
            var ca = context.CustomersAuthentications.ToList();
            var usernames = ca.Select(a => a.Username).Distinct();
            if (usernames.Contains(user, StringComparer.OrdinalIgnoreCase))
                return true;
            else
                return false;
        }

        public static bool CheckOldPasswordThenUpdate(int id, string user, string oldp, string newp)
        {
            var context = new TravelExpertsContext();
            var u = context.CustomersAuthentications.SingleOrDefault(cp => cp.CustomerId == id);
         
                if (u.Password == oldp)
                {
                    u.Username = user;
                    u.Password = newp;
                    /*u.Password = confnewp*/;
                    context.SaveChanges();
                    return true;
                }
                else
                    return false;
           
        }
    }         
}