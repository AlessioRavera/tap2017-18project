using System.Data.Entity;

namespace TravelCompanyImplementation
{
    public class TravelCompanyContext : DbContext
    {
        public DbSet<LegDB> legs { get; set; }

        public TravelCompanyContext(string travelCompanyConnectionString)
            : base(travelCompanyConnectionString)
        {
            Database.SetInitializer<TravelCompanyContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LegDB>().HasIndex(p => new {p.Cost, p.Distance, p.From, p.To, p.TransportT}).IsUnique();
        }
    }
}
