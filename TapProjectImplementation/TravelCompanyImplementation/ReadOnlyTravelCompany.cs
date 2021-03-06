﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;
using Utility;

namespace TravelCompanyImplementation
{
    public class ReadOnlyTravelCompany : IReadOnlyTravelCompany
    {
        private readonly string Name, TravelCompanyConnectionString;
        internal ReadOnlyTravelCompany(string name, string travelCompanyConnectionString)
        {
            this.Name = name;
            this.TravelCompanyConnectionString = travelCompanyConnectionString;
        }

        public ReadOnlyCollection<ILegDTO> FindLegs(Expression<Func<ILegDTO, bool>> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException();

            try
            {
                using (var travelCompanyDBContext = new TravelCompanyContext(TravelCompanyConnectionString))
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
            catch (Exception e)
            {
                throw new DbConnectionException(e.Message,e);
            }
        }

        public ReadOnlyCollection<ILegDTO> FindDepartures(string @from, TransportType allowedTransportTypes)
        {
            UtilityClass.CheckNotNull(from);
            UtilityClass.CheckOnlyAlphanumChar(from);
            UtilityClass.CheckNameLength(from);
            UtilityClass.CheckTransportType(allowedTransportTypes);

            try
            {
                using (var travelCompanyDBContext = new TravelCompanyContext(TravelCompanyConnectionString))
                {
                    List<ILegDTO> legsDTO = new List<ILegDTO>();
                    var elementsLegDb = from l in travelCompanyDBContext.legs
                        where(l.From == @from && allowedTransportTypes.HasFlag(l.TransportT))
                        select l;

                    foreach (var leg in elementsLegDb)
                    {
                        legsDTO.Add(new LegDTO(leg.From,leg.To, leg.Distance, leg.Cost,leg.TransportT));
                    }
                    return legsDTO.AsReadOnly();
                }
                
            }
            catch (Exception e)
            {
                throw new DbConnectionException(e.Message, e);
            }

        } 
        public override bool Equals(object obj)
        {
            var company = obj as ReadOnlyTravelCompany;
            return company != null &&
                   Name == company.Name &&
                   TravelCompanyConnectionString == company.TravelCompanyConnectionString;
        }

        public override int GetHashCode()
        {
            var hashCode = 874148471;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TravelCompanyConnectionString);
            return hashCode;
        }
    }
}
