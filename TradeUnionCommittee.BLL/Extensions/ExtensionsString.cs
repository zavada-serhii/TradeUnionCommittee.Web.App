using System;

namespace TradeUnionCommittee.BLL.Extensions
{
    public static class ExtensionsString
    {
        public static string AddMaskForCityPhone(this string phone)
        {
            return phone.Insert(3, "-").Insert(6, "-");
        }

        public static bool EqualsTo(this string str1, string str2)
        {
           return str1.Equals(str2, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}