using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;
using Utility;

namespace TravelCompanyImplementation
{
    public class ReadOnlyTravelCompanyFactory : IReadOnlyTravelCompanyFactory
    {
        private string dbConnectionString;

        public ReadOnlyTravelCompanyFactory(string dbConnectionString)
        {
            this.dbConnectionString = dbConnectionString;
        }

        public IReadOnlyTravelCompany Get(string name)
        {
            UtilityClass.CheckNotNull(name);
            UtilityClass.CheckNotEmpty(name);
            UtilityClass.CheckNameLength(name);
            UtilityClass.CheckOnlyAlphanumChar(name);
            using (var brokerDBContext = new TravelCompanyBrokerContext(dbConnectionString))
            {
                var query = from tc in brokerDBContext.travelCompanies
                    where tc.TravelCompanyName == name
                    select tc.TravelCompanyConnectionString;
                if (!query.Any())
                    throw new NonexistentTravelCompanyException();
                return new ReadOnlyTravelCompany(name, query.FirstOrDefault());
            }
        }
    }
}
