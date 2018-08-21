using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;
using Utility;

namespace TravelCompanyImplementation
{
    public class TravelCompanyFactory : ITravelCompanyFactory
    {
        private string dbConnectionString;

        public TravelCompanyFactory(string dbConnectionString)
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
                    using (var travelCompanyDBContext= new TravelCompanyContext(travelCompanyConnectionString))
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

        }

        public ITravelCompany Get(string name)
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
                if(!query.Any())
                    throw new NonexistentTravelCompanyException();
                return new TravelCompany(name, query.FirstOrDefault());
            }
        }
    }
}
