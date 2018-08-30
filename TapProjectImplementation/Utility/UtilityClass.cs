using System;
using System.Text.RegularExpressions;
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

        public static void CheckOnlyAlphanumChar(string s)
        {
            Regex r = new Regex("^[a-zA-Z0-9]*$");
            if (!r.IsMatch(s))
            {
                throw new ArgumentException();
            }
        }

        public static void CheckStrictlyPositive(int val)
        {
            if(val <= 0)
                throw new ArgumentException();
        }

        public static void CheckTransportType(TransportType t)
        {
            if (t==TransportType.None)
                throw new ArgumentException();
        }

        public static void CheckNotEquals(string s1, string s2)
        {
            if(s1==s2)
                throw  new ArgumentException();
        }


    }
}
