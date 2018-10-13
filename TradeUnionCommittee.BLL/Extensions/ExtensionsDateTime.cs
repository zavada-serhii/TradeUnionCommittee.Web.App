using System;

namespace TradeUnionCommittee.BLL.Extensions
{
    public static class ExtensionsDateTime
    {
        public static int CalculateAge(this DateTime birthDate)
        {
            var yearsPassed = DateTime.Now.Year - birthDate.Year;
            if (DateTime.Now.Month < birthDate.Month || (DateTime.Now.Month == birthDate.Month && DateTime.Now.Day < birthDate.Day))
            {
                yearsPassed--;
            }
            return yearsPassed;
        }
    }
}