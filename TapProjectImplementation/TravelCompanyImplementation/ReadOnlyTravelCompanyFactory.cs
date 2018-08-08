using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP2017_2018_TravelCompanyInterface;

namespace TravelCompanyImplementation
{
    class ReadOnlyTravelCompanyFactory : IReadOnlyTravelCompanyFactory
    {
        public IReadOnlyTravelCompany Get(string name)
        {
            throw new NotImplementedException();
        }
    }
}
