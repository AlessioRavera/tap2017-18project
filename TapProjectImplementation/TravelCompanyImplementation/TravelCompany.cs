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
    }
}
