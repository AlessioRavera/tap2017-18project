using System;
using System.Collections.Generic;
using System.Linq;
using PlannerTest;
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
            Dictionary<string, Tuple<int, ILegDTO>> CityNodesDictionary = new Dictionary<string, Tuple<int, ILegDTO>>();
            Dictionary<string, int> CityNodeNOT_Visited = new Dictionary<string, int>();
            HashSet<string> CityNodeVisited = new HashSet<string>();
            CityNodesDictionary.Add(source,new Tuple<int, ILegDTO>(0,null));
            CityNodeNOT_Visited.Add(source, 0);
            

            while (CityNodeNOT_Visited.Any())
            {
                var cityVisiting = CityNodeNOT_Visited.Aggregate((l, r) => l.Value < r.Value ? l : r); 
                CityNodeNOT_Visited.Remove(cityVisiting.Key);
                CityNodeVisited.Add(cityVisiting.Key);

                if (cityVisiting.Key == destination) 
                    return GetBestTrip(CityNodesDictionary, source, destination);

                int costToReachCity = cityVisiting.Value; 
                foreach (var TravelCompany in TravelCompanySet)
                {
                    var legsOfTravelCompanyFromCity = TravelCompany.FindDepartures(cityVisiting.Key, allowedTransportTypes);
                    foreach (var leg in legsOfTravelCompanyFromCity)
                    {
                        if (!CityNodeVisited.Contains(leg.To))
                        {
                            int legCost = 0;
                            switch (options)
                            {
                                case FindOptions.MinimumDistance:
                                    legCost = leg.Distance;
                                    break;

                                case FindOptions.MinimumCost:
                                    legCost = leg.Cost;
                                    break;

                                case FindOptions.MinimumHops:
                                    legCost++;
                                    break;
                            }
                            if(CityNodesDictionary.ContainsKey(leg.To))
                            {
                                
                                if ((costToReachCity + legCost) < CityNodesDictionary[leg.To].Item1)
                                {  
                                    CityNodesDictionary.Remove(leg.To);
                                    CityNodesDictionary.Add(leg.To,new Tuple<int, ILegDTO>(costToReachCity+legCost,leg));
                                    CityNodeNOT_Visited[leg.To] = costToReachCity + legCost;
                                }

                            }
                            else
                            {
                                CityNodesDictionary.Add(leg.To,new Tuple<int, ILegDTO>(costToReachCity+legCost,leg));
                                CityNodeNOT_Visited.Add(leg.To, costToReachCity + legCost);
                            }
                        }

                            
                    }
                }
            }
            return null; 
        }

        private ITrip GetBestTrip(Dictionary<string, Tuple<int, ILegDTO>> CityNodesDictionary, string sourceName, string destinationName)
        {
            List <ILegDTO> finalPath= new List<ILegDTO>();
            int totalCost=0, totalDistance=0;
            string cursorCityName = destinationName;
            while (!(CityNodesDictionary[cursorCityName].Item2 is null))
            {

                finalPath.Add(CityNodesDictionary[cursorCityName].Item2);
                totalCost += CityNodesDictionary[cursorCityName].Item2.Cost;
                totalDistance += CityNodesDictionary[cursorCityName].Item2.Distance;
                cursorCityName = CityNodesDictionary[cursorCityName].Item2.From;
            }

            finalPath.Reverse();
            return new Trip(sourceName, destinationName, finalPath.AsReadOnly(), totalCost, totalDistance);
            
        }
    }

}
