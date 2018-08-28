using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;
using Utility;
using TransportType = System.Net.TransportType;

namespace TravelCompanyImplementation
{
    public class TravelCompany : ITravelCompany
    {
        private readonly string travelCompanyConnectionString;

        internal TravelCompany(string name, string travelCompanyConnectionString)
        {
            Name = name;
            this.travelCompanyConnectionString = travelCompanyConnectionString;
        }

        public int CreateLeg(string @from, string to, int cost, int distance, TAP2017_2018_TravelCompanyInterface.TransportType transportType)
        {
            UtilityClass.CheckNotNull(from);
            UtilityClass.CheckNotNull(to);
            UtilityClass.CheckNameLength(from);
            UtilityClass.CheckNameLength(to);
            UtilityClass.CheckOnlyAlphanumChar(from);
            UtilityClass.CheckOnlyAlphanumChar(to);
            UtilityClass.CheckNotEquals(from,to);
            UtilityClass.CheckStrictlyPositive(cost);
            UtilityClass.CheckStrictlyPositive(distance);
            UtilityClass.CheckTransportType(transportType);

            try
            {
                using (var travelCompanyDBContext = new TravelCompanyContext(travelCompanyConnectionString))
                {
                    var leg= new LegDB()
                    {
                        From = from,
                        To = to,
                        Cost = cost,
                        Distance = distance,
                        TransportT = transportType 
                    };
                    var l = travelCompanyDBContext.legs.Add(leg);
                    travelCompanyDBContext.SaveChanges();
                    return l.LegID;
                }
            }
            catch (Exception e)
            {
                throw new DbConnectionException(e.Message,e);
            }
           
        }

        public void DeleteLeg(int legToBeRemovedId)
        {
            try
            {
                using (var travelCompanyDBContext = new TravelCompanyContext(travelCompanyConnectionString))
                {
                    var elementLegDb = (from l in travelCompanyDBContext.legs
                        where l.LegID == legToBeRemovedId
                        select l).Single();

                    travelCompanyDBContext.legs.Remove(elementLegDb);
                    travelCompanyDBContext.SaveChanges();
                }
            }
            catch (InvalidOperationException)
            {
                throw new NonexistentObjectException();
            }
            catch(Exception e)
            {
                throw new DbConnectionException(e.Message, e);
            }

        }

        public ILegDTO GetLegDTOFromId(int legId)
        {
            try
            {
                using (var travelCompanyDBContext = new TravelCompanyContext(travelCompanyConnectionString))
                {
                    var elemLegDb = (from l in travelCompanyDBContext.legs
                        where l.LegID == legId
                        select l).Single();

                    return new LegDTO(elemLegDb.From, elemLegDb.To, elemLegDb.Distance, elemLegDb.Cost, elemLegDb.TransportT);
                }
            }
            catch (InvalidOperationException)
            {
                throw new NonexistentObjectException();
            }
            catch (Exception e)
            {
                throw new DbConnectionException(e.Message, e);
            }

        }

        public string Name { get; }


        public override bool Equals(object obj)
        {
            var company = obj as TravelCompany;
            return company != null &&
                   travelCompanyConnectionString == company.travelCompanyConnectionString &&
                   Name == company.Name;
        }

        public override int GetHashCode()
        {
            var hashCode = -362109777;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(travelCompanyConnectionString);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }
    }
}
