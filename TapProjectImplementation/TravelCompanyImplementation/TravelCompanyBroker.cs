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
        private string dbConnectionString;

        public TravelCompanyBroker(string dbConnectionString)
        {
            this.dbConnectionString = dbConnectionString;
        }

        public ITravelCompanyFactory GetTravelCompanyFactory()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyTravelCompanyFactory GetReadOnlyTravelCompanyFactory()
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<string> KnownTravelCompanies()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            var item = obj as TravelCompanyBroker;

            if (item == null)
            {
                return false;
            }

            return this.dbConnectionString.Equals(item.dbConnectionString);
        }

        public override int GetHashCode()
        {
            return this.dbConnectionString.GetHashCode();
        }
    }
}
