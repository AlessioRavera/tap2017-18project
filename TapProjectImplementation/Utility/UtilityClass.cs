using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
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

        public static void CheckNameLength(string s)
        {
            if (s.Length > DomainConstraints.NameMaxLength || s.Length < DomainConstraints.NameMinLength)
                throw new ArgumentException();
        }

        public static void CheckOnlyAlphanumChar(String s)
        {
            Regex r = new Regex("^[a-zA-Z0-9]*$");
            if (!r.IsMatch(s))
            {
                throw new ArgumentException();
            }
        }

    }
}
