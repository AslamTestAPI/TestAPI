using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APISample.Models
{
    public class Friend
    {
        public int FriendId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Notes { get; set; }
    }
}