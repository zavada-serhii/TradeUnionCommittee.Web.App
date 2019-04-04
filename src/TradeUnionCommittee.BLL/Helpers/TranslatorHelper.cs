namespace TradeUnionCommittee.BLL.Helpers
{
    internal class TranslatorHelper
    {
        internal static string ConvertToEnglishLang(string param)
        {
            switch (param)
            {
                case "Адміністратор":
                    return "Admin";
                case "Бухгалтер":
                    return "Accountant";
                case "Заступник":
                    return "Deputy";
                default:
                    return string.Empty;
            }
        }

        internal static string ConvertToUkrainianLang(string param)
        {
            switch (param)
            {
                case "Admin":
                    return "Адміністратор";
                case "Accountant":
                    return "Бухгалтер";
                case "Deputy":
                    return "Заступник";
                default:
                    return string.Empty;
            }
        }

        internal static string ConvertToUkraineGender(string sex)
        {
            switch (sex)
            {
                case "Male":
                    return "Чоловіча";
                case "Female":
                    return "Жіноча";
                default:
                    return string.Empty;
            }
        }
    }
}