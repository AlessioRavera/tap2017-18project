using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TAP2017_2018_TravelCompanyInterface;

namespace TravelCompanyImplementation
{
    public class TravelCompanyDB
    {
        [Column]
        [StringLength(DomainConstraints.NameMaxLength,MinimumLength = DomainConstraints.NameMinLength)]
        [Key]
        public string TravelCompanyName { get; set; }
        [Column]
        [Index(IsUnique = true)]
        [StringLength(DomainConstraints.ConnectionStringMaxLength, MinimumLength = DomainConstraints.ConnectionStringMinLength)]
        public string TravelCompanyConnectionString { get; set; }
    }
}
