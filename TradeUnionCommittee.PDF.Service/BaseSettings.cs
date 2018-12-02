using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Reflection;

namespace TradeUnionCommittee.PDF.Service
{
    public class BaseSettings
    {
        private static string BasePath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private readonly string _pathToContainer = $@"{BasePath}\PDF Container";
        protected string PathToFile => $@"{_pathToContainer}\{Guid.NewGuid()}.pdf";
        protected readonly Font Font;
        protected readonly Font FontBold;
        protected const string Сurrency = "грн";

        protected BaseSettings()
        {
            var baseFont = BaseFont.CreateFont($@"{BasePath}\Fonts\TimesNewRoman.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            Font = new Font(baseFont, 14, Font.NORMAL);
            FontBold = new Font(baseFont, 12, Font.BOLD);
            CheckDirectory();
        }

        protected void AddEmptyParagraph(IElementListener document, int count)
        {
            for (var i = 0; i < count; i++)
            {
                document.Add(new Phrase(Environment.NewLine));
            }
        }

        protected void AddCell(PdfPTable table, Font font, int colspan, string value)
        {
            table.AddCell(new PdfPCell(new Phrase(value, font))
            {
                PaddingTop = 5,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Colspan = colspan
            });
        }

        private void CheckDirectory()
        {
            var dirInfo = new DirectoryInfo(_pathToContainer);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
        }
    }
}
