using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP2017_2018_PlannerInterface;
using TAP2017_2018_TravelCompanyInterface;

namespace PlannerImplementation
{
    class Trip : ITrip
    {
        public string From { get; }
        public string To { get; }
        public ReadOnlyCollection<ILegDTO> Path { get; }
        public int TotalCost { get; }
        public int TotalDistance { get; }
    }
}
