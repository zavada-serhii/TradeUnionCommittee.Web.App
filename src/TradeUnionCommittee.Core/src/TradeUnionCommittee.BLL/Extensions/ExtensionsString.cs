using System;
using System.Linq;

namespace TradeUnionCommittee.BLL.Extensions
{
    public static class ExtensionsString
    {
        public static string AddMaskForCityPhone(this string phone) => phone.Insert(3, "-").Insert(6, "-");

        /// <summary>
        /// Returns equality ignore culture and case
        /// </summary>
        /// <param name="input"></param>
        /// <param name="array"></param>
        /// <returns>bool</returns>
        public static bool IsEqual(this string input, params string[] array) => array.Contains(input, StringComparer.InvariantCultureIgnoreCase);
    }
}