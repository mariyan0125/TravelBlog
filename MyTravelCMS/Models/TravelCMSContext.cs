using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MyTravelCMS.Models
{
    public class TravelCMSContext : DbContext
    {
        public DbSet<Traveller> Travellers { get; set; }
        public DbSet<Tip> Tips { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}