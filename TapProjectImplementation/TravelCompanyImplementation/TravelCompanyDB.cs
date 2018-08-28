using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
