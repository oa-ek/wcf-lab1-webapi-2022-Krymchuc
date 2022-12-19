using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Real_State_Catalog.Core
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options)
            : base(options)
        {
        }

        public DbSet<Accommodation> Accommodations { get; set; }
        public DbSet<Amenity> Amenity { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<OfferDetail> OfferDetails { get; set; }
    }
}