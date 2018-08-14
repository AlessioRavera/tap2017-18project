using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP2017_2018_TravelCompanyInterface;
using Utility;

namespace TravelCompanyImplementation
{
    class TravelCompanyBrokerFactory : ITravelCompanyBrokerFactory
    {
        public ITravelCompanyBroker CreateNewBroker(string dbConnectionString)
        {
            UtilityClass.CheckNotNull(dbConnectionString);
            UtilityClass.CheckNotEmpty(dbConnectionString);
            UtilityClass.CheckConnectionStringLength(dbConnectionString);
            throw new NotImplementedException();
        }

        public ITravelCompanyBroker GetBroker(string dbConnectionString)
        {
            UtilityClass.CheckNotNull(dbConnectionString);
            UtilityClass.CheckNotEmpty(dbConnectionString);
            UtilityClass.CheckConnectionStringLength(dbConnectionString);
            throw new NotImplementedException();
        }
    }
}
