using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP2017_2018_TravelCompanyInterface;

namespace Utility
{
    public static class UtilityClass
    {
        public static void CheckNotNull(string s)
        {
            if (s == null)
                throw new ArgumentNullException();

        }

        public static void CheckNotEmpty(string s)
        {
            if (s == String.Empty)
                throw new ArgumentException();
        }

        public static void CheckConnectionStringLength(string s)
        {
            if (s.Length > DomainConstraints.ConnectionStringMaxLength || s.Length < DomainConstraints.ConnectionStringMinLength)
                throw new ArgumentException();
        }
    }
}
