using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace TravelCompanyImplementation
{
    public class TravelCompanyBrokerContext : DbContext
    {
        public DbSet<TravelCompanyDB> travelCompanies { get; set; }


        public TravelCompanyBrokerContext(string dbConnectionString)
            : base(dbConnectionString)
        {
            Database.SetInitializer<TravelCompanyBrokerContext>(null);
        }
       
    
    }

    

}   
    
