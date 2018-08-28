using System;
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

            try
            {
                using (var brokerDBContext = new TravelCompanyBrokerContext(dbConnectionString))
                {
                    brokerDBContext.Database.Delete();
                    brokerDBContext.Database.Create();
                }

                return new TravelCompanyBroker(dbConnectionString);
            }
            catch (Exception e)
            {
                throw new DbConnectionException(e.Message,e);
            }
            
        }

        public ITravelCompanyBroker GetBroker(string dbConnectionString)
        {
            UtilityClass.CheckNotNull(dbConnectionString);
            UtilityClass.CheckNotEmpty(dbConnectionString);
            UtilityClass.CheckConnectionStringLength(dbConnectionString);

            try
            {
                using (var brokerDBContext = new TravelCompanyBrokerContext(dbConnectionString))
                {
                    if (!brokerDBContext.Database.Exists())
                        throw new NonexistentObjectException();
                }

                return new TravelCompanyBroker(dbConnectionString);
            }
            
            catch (NonexistentObjectException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DbConnectionException(e.Message,e);
            }
        }
    }

}
