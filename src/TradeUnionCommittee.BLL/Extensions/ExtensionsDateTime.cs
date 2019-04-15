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

        public static int? CalculateAgeForNull(this DateTime? birthDate)
        {
            return birthDate?.CalculateAge();
        }

        public static bool Between(this DateTime input, DateTime startDate, DateTime endDate)
        {
            return input >= startDate && input <= endDate;
        }
    }
}