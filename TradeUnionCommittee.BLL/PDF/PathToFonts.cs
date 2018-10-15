using System.IO;
using System.Reflection;

namespace TradeUnionCommittee.BLL.PDF
{
    public class PathToFonts
    {
        private static readonly string BasePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static string PathToUkrainianFont = $@"{BasePath}\Fonts\TimesNewRoman.ttf";
    }
}
