using System;
using System.Collections.Generic;

#nullable disable

namespace Group8.TravelExperts.Data.Domain
{
    public partial class CustomersAuthentication
    {
        public int CustomerId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
