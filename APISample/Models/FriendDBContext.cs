using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace APISample.Models
{
    public class FriendDBContext : DbContext
    {
        public FriendDBContext()
            : base("myConnection")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Friend> Friends { get; set; }
    }
}