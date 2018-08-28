using System;
using System.Linq;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;
using Utility;

namespace TravelCompanyImplementation
{
    public class ReadOnlyTravelCompanyFactory : IReadOnlyTravelCompanyFactory
    {
        private readonly string dbConnectionString;

        internal ReadOnlyTravelCompanyFactory(string dbConnectionString)
        {
            this.dbConnectionString = dbConnectionString;
        }

        public IReadOnlyTravelCompany Get(string name)
        {
            UtilityClass.CheckNotNull(name);
            UtilityClass.CheckNotEmpty(name);
            UtilityClass.CheckNameLength(name);
            UtilityClass.CheckOnlyAlphanumChar(name);

            try
            {
                using (var brokerDBContext = new TravelCompanyBrokerContext(dbConnectionString))
                {
                    var TCConnString = (from tc in brokerDBContext.travelCompanies
                        where tc.TravelCompanyName == name
                        select tc.TravelCompanyConnectionString).Single();

                    return new ReadOnlyTravelCompany(name, TCConnString);
                }
            }
            catch (InvalidOperationException)
            {
                throw new NonexistentObjectException();
            }
            catch (Exception e)
            {
                throw new DbConnectionException(e.Message,e);
            }
        }
    }
}
