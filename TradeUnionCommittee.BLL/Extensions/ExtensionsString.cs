namespace TradeUnionCommittee.BLL.Extensions
{
    public static class ExtensionsString
    {
        public static string AddMaskForCityPhone(this string phone)
        {
            return phone.Insert(3, "-").Insert(6, "-");
        }
    }
}
