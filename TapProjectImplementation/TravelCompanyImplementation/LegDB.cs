using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using TAP2017_2018_TravelCompanyInterface;
using TransportType = TAP2017_2018_TravelCompanyInterface.TransportType;

namespace TravelCompanyImplementation
{
    public class LegDB
    {
        [Column]
        [Key]
        public int LegID { get; set; }
        [Column]
        [StringLength(DomainConstraints.NameMaxLength, MinimumLength = DomainConstraints.NameMinLength)]
        public string From { get; set; }
        [Column]
        [StringLength(DomainConstraints.NameMaxLength, MinimumLength = DomainConstraints.NameMinLength)]
        public string To { get; set; }
        [Column]
        public int Cost { get; set; }
        [Column]
        public int Distance { get; set; }
        [Column]
        public TransportType TransportT { get; set; }
    }
}