using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP2017_2018_TravelCompanyInterface;

namespace TravelCompanyImplementation
{
    public class TravelCompanyBroker : ITravelCompanyBroker
    {
        private readonly string dbConnectionString;

        public TravelCompanyBroker(string dbConnectionString)
        {
            this.dbConnectionString = dbConnectionString;
        }

        public ITravelCompanyFactory GetTravelCompanyFactory()
        {
            return new TravelCompanyFactory(dbConnectionString);
        }

        public IReadOnlyTravelCompanyFactory GetReadOnlyTravelCompanyFactory()
        {
            return new ReadOnlyTravelCompanyFactory(dbConnectionString);
        }

        public ReadOnlyCollection<string> KnownTravelCompanies()
        {
            using (var brokerDBContext = new TravelCompanyBrokerContext(dbConnectionString))
            {
                List<string> travelCompanies = new List<string>();
                var query = from tc in brokerDBContext.travelCompanies
                    select tc.TravelCompanyName;
                foreach (var item in query)
                    travelCompanies.Add(item);
                return new ReadOnlyCollection<string>(travelCompanies);
            }
            
        }

        public override bool Equals(object obj)
        {
            var broker = obj as TravelCompanyBroker;
            return broker != null &&
                   dbConnectionString == broker.dbConnectionString;
        }

        public override int GetHashCode()
        {
            return 758876710 + EqualityComparer<string>.Default.GetHashCode(dbConnectionString);
        }
    }
}
