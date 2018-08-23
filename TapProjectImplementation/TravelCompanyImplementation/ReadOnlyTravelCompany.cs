using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;
using Utility;

namespace TravelCompanyImplementation
{
    public class ReadOnlyTravelCompany : IReadOnlyTravelCompany
    {
        private readonly string Name, travelCompanyConnectionString;
        public ReadOnlyTravelCompany(string name, string travelCompanyConnectionString)
        {
            this.Name = name;
            this.travelCompanyConnectionString = travelCompanyConnectionString;
        }

        public ReadOnlyCollection<ILegDTO> FindLegs(Expression<Func<ILegDTO, bool>> predicate)
        {
            try
            {
                using (var travelCompanyDBContext = new TravelCompanyContext(travelCompanyConnectionString))
                {
                    List<ILegDTO> legsDTO = new List<ILegDTO>();
                    var elementsLegDb = from l in travelCompanyDBContext.legs
                        select l;
                    var del = predicate.Compile();
                    foreach (var leg in elementsLegDb)
                    {
                        LegDTO l = new LegDTO(leg.From, leg.To, leg.Distance, leg.Cost, leg.TransportT);
                        if(del.Invoke(l))
                            legsDTO.Add(l);
                    } 
                    return new ReadOnlyCollection<ILegDTO>(legsDTO);
                }

            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public ReadOnlyCollection<ILegDTO> FindDepartures(string @from, TransportType allowedTransportTypes)
        {
            UtilityClass.CheckNotNull(from);
            UtilityClass.CheckOnlyAlphanumChar(from);
            UtilityClass.CheckNotEmpty(from);
            UtilityClass.CheckNameLength(from);
            UtilityClass.CheckTransportType(allowedTransportTypes);

            try
            {
                using (var travelCompanyDBContext = new TravelCompanyContext(travelCompanyConnectionString))
                {
                    List<ILegDTO> legsDTO = new List<ILegDTO>();
                    var elementsLegDb = from l in travelCompanyDBContext.legs
                        where(l.From == @from && allowedTransportTypes.HasFlag(l.TransportT))
                        select l;

                    foreach (var leg in elementsLegDb)
                    {
                        legsDTO.Add(new LegDTO(leg.From,leg.To, leg.Distance, leg.Cost,leg.TransportT));
                    }
                    return new ReadOnlyCollection<ILegDTO>(legsDTO);
                }
                
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
            
        } 
        public override bool Equals(object obj)
        {
            var company = obj as ReadOnlyTravelCompany;
            return company != null &&
                   Name == company.Name &&
                   travelCompanyConnectionString == company.travelCompanyConnectionString;
        }

        public override int GetHashCode()
        {
            var hashCode = 874148471;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(travelCompanyConnectionString);
            return hashCode;
        }
    }
}
