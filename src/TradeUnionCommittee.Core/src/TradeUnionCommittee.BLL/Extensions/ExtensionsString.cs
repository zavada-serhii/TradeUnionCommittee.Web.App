using System;

namespace TradeUnionCommittee.BLL.Extensions
{
    public static class ExtensionsString
    {
        public static string AddMaskForCityPhone(this string phone)
        {
            return phone.Insert(3, "-").Insert(6, "-");
        }

        public static bool IsEqual(this string input, string value)
        {
           return input.Equals(value, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}