using System.Collections.Generic;

namespace PlannerImplementation
{
    internal class CityCostComparer : IComparer<(string CityName, int Cost)>
    {
       
        public int Compare((string CityName, int Cost) c1, (string CityName, int Cost) c2)
        {
            int comp = c1.Cost.CompareTo(c2.Cost);
            if (comp == 0)
            {
                return c1.CityName.CompareTo(c2.CityName);
            }
            return comp;
        }
    }
}
