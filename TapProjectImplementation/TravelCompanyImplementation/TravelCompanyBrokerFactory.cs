using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;
using Utility;

namespace TravelCompanyImplementation
{
    public class TravelCompanyBrokerFactory : ITravelCompanyBrokerFactory
    {
        public ITravelCompanyBroker CreateNewBroker(string dbConnectionString)
        {
            UtilityClass.CheckNotNull(dbConnectionString);
            UtilityClass.CheckNotEmpty(dbConnectionString);
            UtilityClass.CheckConnectionStringLength(dbConnectionString);
            using (var brokerDBContext = new TravelCompanyBrokerContext(dbConnectionString))
            {
                brokerDBContext.Database.Delete();
                brokerDBContext.Database.Create();
            }
            return new TravelCompanyBroker(dbConnectionString);
        }

        public ITravelCompanyBroker GetBroker(string dbConnectionString)
        {
            UtilityClass.CheckNotNull(dbConnectionString);
            UtilityClass.CheckNotEmpty(dbConnectionString);
            UtilityClass.CheckConnectionStringLength(dbConnectionString);
            using (var brokerDBContext = new TravelCompanyBrokerContext(dbConnectionString))
            {
                if(!brokerDBContext.Database.Exists())
                    throw new NonexistentObjectException();
            }
            return new TravelCompanyBroker(dbConnectionString);
        }
    }

}
