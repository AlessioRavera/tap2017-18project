using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP2017_2018_TravelCompanyInterface;

namespace TravelCompanyImplementation
{
    public class TravelCompany : ITravelCompany
    {
        private string travelCompanyConnectionString;

        public TravelCompany(string name, string travelCompanyConnectionString)
        {
            Name = name;
            this.travelCompanyConnectionString = travelCompanyConnectionString;
        }

        public int CreateLeg(string @from, string to, int cost, int distance, TransportType transportType)
        {
            throw new NotImplementedException();
        }

        public void DeleteLeg(int legToBeRemovedId)
        {
            throw new NotImplementedException();
        }

        public ILegDTO GetLegDTOFromId(int legId)
        {
            throw new NotImplementedException();
        }

        public string Name { get; }

        public override bool Equals(object obj)
        {
            var item = obj as TravelCompany;

            if (item == null)
            {
                return false;
            }

            return this.Name.Equals(item.Name) && this.travelCompanyConnectionString.Equals(item.travelCompanyConnectionString);
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + Name.GetHashCode();
            hash = (hash * 7) + travelCompanyConnectionString.GetHashCode();
            return hash;
        }
    }
}
