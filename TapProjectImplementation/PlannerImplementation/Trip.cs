using System.Collections.ObjectModel;
using TAP2017_2018_PlannerInterface;
using TAP2017_2018_TravelCompanyInterface;

namespace PlannerTest
{
    class Trip : ITrip
    {
        public Trip(string from, string to, ReadOnlyCollection<ILegDTO> path, int totalCost, int totalDistance)
        {
            From = from;
            To = to;
            Path = path;
            TotalCost = totalCost;
            TotalDistance = totalDistance;
        }

        public string From { get; }
        public string To { get; }
        public ReadOnlyCollection<ILegDTO> Path { get; }
        public int TotalCost { get; }
        public int TotalDistance { get; }
    }
}
