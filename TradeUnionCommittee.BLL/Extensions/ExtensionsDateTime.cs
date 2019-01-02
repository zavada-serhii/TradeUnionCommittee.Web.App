using System;
using System.Collections.Generic;
using System.Linq;

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

        internal static IEnumerable<string> GetListPartitionings(this DateTime startDate, DateTime endDate)
        {
            if (startDate.Year == endDate.Year && startDate.Month == endDate.Month)
            {
                return new List<string> { $"journal_y{startDate.Year}_m{startDate.Month}" };
            }
            var listDateTime = Enumerable.Range(0, endDate.Subtract(startDate).Days + 1).Select(d => startDate.AddDays(d));
            return listDateTime.Select(dateTime => $"journal_y{dateTime.Year}_m{dateTime.Month}").ToList().Distinct();
        }
    }
}