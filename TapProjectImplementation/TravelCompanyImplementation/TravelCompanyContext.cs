using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCompanyImplementation
{
    class TravelCompanyContext : DbContext
    {
        public DbSet<LegDB> legs { get; set; }

        public TravelCompanyContext(string travelCompanyConnectionString)
            : base(travelCompanyConnectionString)
        {
            Database.SetInitializer<TravelCompanyContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LegDB>().HasIndex(p => new {p.Cost, p.Distance, p.From, p.To, p.transportType});
        }
    }
}
