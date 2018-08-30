using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;
using Utility;

namespace TravelCompanyImplementation
{
    public class TravelCompanyFactory : ITravelCompanyFactory
    {
        private readonly string dbConnectionString;

        internal TravelCompanyFactory(string dbConnectionString)
        {
            this.dbConnectionString = dbConnectionString;
        }

        public ITravelCompany CreateNew(string travelCompanyConnectionString, string name)
        {
            UtilityClass.CheckNotNull(travelCompanyConnectionString);
            UtilityClass.CheckNotNull(name);
            UtilityClass.CheckNotEmpty(travelCompanyConnectionString);
            UtilityClass.CheckNotEmpty(name);
            UtilityClass.CheckConnectionStringLength(travelCompanyConnectionString);
            UtilityClass.CheckNameLength(name);
            UtilityClass.CheckOnlyAlphanumChar(name);

            try
            {
                using (var brokerDBContext = new TravelCompanyBrokerContext(dbConnectionString))
                {
                    var travelCompany = new TravelCompanyDB()
                    {
                        TravelCompanyName = name,
                        TravelCompanyConnectionString = travelCompanyConnectionString
                    };

                    brokerDBContext.travelCompanies.Add(travelCompany);
                    brokerDBContext.SaveChanges();

                    using (var travelCompanyDBContext = new TravelCompanyContext(travelCompanyConnectionString))
                    {
                        travelCompanyDBContext.Database.Delete();
                        travelCompanyDBContext.Database.Create();
                    }

                    return new TravelCompany(name, travelCompanyConnectionString);
                }

            }
            catch (DbUpdateException e)
            {
                if (e.ToString().Contains("KEY"))
                {
                    throw new TapDuplicatedObjectException();
                }

                throw new SameConnectionStringException();
            }
            catch (Exception e)
            {
                throw new DbConnectionException(e.Message, e);
            }

        }

        public ITravelCompany Get(string name)
        {
            UtilityClass.CheckNotNull(name);
            UtilityClass.CheckNameLength(name);
            UtilityClass.CheckOnlyAlphanumChar(name);

            try
            {
                using (var brokerDBContext = new TravelCompanyBrokerContext(dbConnectionString))
                {
                    var TCConnString = (from tc in brokerDBContext.travelCompanies
                        where tc.TravelCompanyName == name
                        select tc.TravelCompanyConnectionString).Single();

                    return new TravelCompany(name, TCConnString);
                }

            }
            catch (InvalidOperationException)
            {
                throw new NonexistentTravelCompanyException();
            }
            catch (Exception e)
            {
                throw new DbConnectionException(e.Message, e);
            }

        }
    }
}
