using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace TravelCompanyImplementation
{
    public class LegDB
    {
        [Column]
        [Key]
        public int LegID { get; set; }
        [Column]
        public string From { get; set; }
        [Column]
        public string To { get; set; }
        [Column]
        public int Cost { get; set; }
        [Column]
        public int Distance { get; set; }
        [Column]
        public TransportType transportType { get; set; }
    }
}