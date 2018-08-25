using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP2017_2018_PlannerInterface;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;

namespace PlannerImplementation
{
    public class Planner : IPlanner
    {
        private HashSet<IReadOnlyTravelCompany> TravelCompanySet = new HashSet<IReadOnlyTravelCompany>();

        public void AddTravelCompany(IReadOnlyTravelCompany readonlyTravelCompany)
        {
            if(readonlyTravelCompany is null)
                throw new ArgumentNullException();

            if (!TravelCompanySet.Add(readonlyTravelCompany))
                throw new TapDuplicatedObjectException();
        }

        public void RemoveTravelCompany(IReadOnlyTravelCompany readonlyTravelCompany)
        {
            if (!TravelCompanySet.Remove(readonlyTravelCompany))
                throw new NonexistentTravelCompanyException();
        }

        public bool ContainsTravelCompany(IReadOnlyTravelCompany readonlyTravelCompany)
        {
            if (readonlyTravelCompany is null)
                throw new ArgumentNullException();
            return TravelCompanySet.Contains(readonlyTravelCompany);
        }

        public IEnumerable<IReadOnlyTravelCompany> KnownTravelCompanies()
        {
            return TravelCompanySet.AsEnumerable();
        }

        public ITrip FindTrip(string source, string destination, FindOptions options, TransportType allowedTransportTypes)
        {
            
            throw new NotImplementedException();
        }
    }
}
