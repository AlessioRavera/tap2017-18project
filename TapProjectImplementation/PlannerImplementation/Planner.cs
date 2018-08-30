using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PlannerTest;
using TAP2017_2018_PlannerInterface;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;
using Utility;

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
            UtilityClass.CheckNotNull(source);
            UtilityClass.CheckNotNull(destination);
            UtilityClass.CheckOnlyAlphanumChar(source);
            UtilityClass.CheckOnlyAlphanumChar(destination);
            UtilityClass.CheckNameLength(source);
            UtilityClass.CheckNameLength(destination);
            UtilityClass.CheckTransportType(allowedTransportTypes);

            CityCostComparer cityCostComparer = new CityCostComparer();
            Dictionary<string,(int Cost, ILegDTO LegUsedToBeReahed)> cityNodesDictionary = new Dictionary<string,ValueTuple<int,ILegDTO>>();
            HashSet<string> citysVisited = new HashSet<string>();
            SortedSet<(string CityName, int Cost)> citysNotVisited = new SortedSet<ValueTuple<string, int>>(cityCostComparer);

            cityNodesDictionary.Add(source, (0, null));
            citysNotVisited.Add((source, 0));
          
            while (citysNotVisited.Any())
            {
                var cityVisiting = citysNotVisited.First();
                citysNotVisited.Remove(cityVisiting);
                citysVisited.Add(cityVisiting.CityName);

                if (cityVisiting.CityName.Equals(destination))
                    return GetBestTrip(cityNodesDictionary, source, destination);

                int costToReachCity = cityVisiting.Cost;
                foreach (var travelCompany in TravelCompanySet)
                {
                    var legsOfTravelCompanyFromCity = travelCompany.FindDepartures(cityVisiting.CityName, allowedTransportTypes);
                    foreach (var leg in legsOfTravelCompanyFromCity)
                    {
                        if (!citysVisited.Contains(leg.To))
                        {
                            int legCost = GetLegCost(leg,options);
                            
                            if (cityNodesDictionary.ContainsKey(leg.To))
                            {
                                if ((costToReachCity + legCost) < cityNodesDictionary[leg.To].Cost)
                                {
                                    cityNodesDictionary[leg.To] = (costToReachCity + legCost, leg);
                                    citysNotVisited.Remove((leg.To, cityNodesDictionary[leg.To].Cost));
                                    citysNotVisited.Add((leg.To, costToReachCity + legCost)); 
                                }
                            }
                            else
                            {
                                cityNodesDictionary.Add(leg.To, (costToReachCity + legCost, leg));
                                citysNotVisited.Add((leg.To, costToReachCity + legCost));
                            }
                        }
                    }
                }
            }
            return null;
        }

        private ITrip GetBestTrip(Dictionary<string, (int Cost, ILegDTO LegUsedToBeReahed)> cityNodesDictionary, string sourceName, string destinationName)
        {
            List <ILegDTO> finalPath= new List<ILegDTO>();
            int totalCost=0, totalDistance=0;
            string cursorCityName = destinationName;
            while (!(cityNodesDictionary[cursorCityName].LegUsedToBeReahed is null))
            {

                finalPath.Add(cityNodesDictionary[cursorCityName].LegUsedToBeReahed);
                totalCost += cityNodesDictionary[cursorCityName].LegUsedToBeReahed.Cost;
                totalDistance += cityNodesDictionary[cursorCityName].LegUsedToBeReahed.Distance;
                cursorCityName = cityNodesDictionary[cursorCityName].LegUsedToBeReahed.From;
            }
            finalPath.Reverse();
            return new Trip(sourceName, destinationName, finalPath.AsReadOnly(), totalCost, totalDistance);
            
        }

        private int GetLegCost(ILegDTO leg, FindOptions options)
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
            return legCost;
        }
    }

}
