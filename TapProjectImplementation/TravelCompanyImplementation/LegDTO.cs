using System.Collections.Generic;
using TAP2017_2018_TravelCompanyInterface;

namespace TravelCompanyImplementation
{
    internal class LegDTO : ILegDTO
    {
        internal LegDTO(string from, string to, int distance, int cost, TransportType type)
        {
            this.From = from;
            this.To = to;
            this.Distance = distance;
            this.Cost = cost;
            this.Type = type;
        }

        public string From { get; }
        public string To { get; }
        public int Distance { get; }
        public int Cost { get; }
        public TransportType Type { get; }

        public override bool Equals(object obj)
        {
            var DTO = obj as LegDTO;
            return DTO != null &&
                   From == DTO.From &&
                   To == DTO.To &&
                   Distance == DTO.Distance &&
                   Cost == DTO.Cost &&
                   Type == DTO.Type;
        }

        public override int GetHashCode()
        {
            var hashCode = 807214720;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(From);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(To);
            hashCode = hashCode * -1521134295 + Distance.GetHashCode();
            hashCode = hashCode * -1521134295 + Cost.GetHashCode();
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            return hashCode;
        }
    }
}
