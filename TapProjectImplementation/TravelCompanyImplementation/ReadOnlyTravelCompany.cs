using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TAP2017_2018_TravelCompanyInterface;

namespace TravelCompanyImplementation
{
    public class ReadOnlyTravelCompany : IReadOnlyTravelCompany
    {
        public ReadOnlyCollection<ILegDTO> FindLegs(Expression<Func<ILegDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<ILegDTO> FindDepartures(string @from, TransportType allowedTransportTypes)
        {
            throw new NotImplementedException();
        }
    }
}
