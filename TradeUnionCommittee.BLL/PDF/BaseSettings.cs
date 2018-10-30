using System;
using System.IO;
using System.Reflection;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace TradeUnionCommittee.BLL.PDF
{
    public class BaseSettings
    {
        private readonly string _basePathToFont = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        protected readonly Font Font;
        protected readonly Font FontBold;
        protected const string Сurrency = "грн";

        protected BaseSettings()
        {
            var baseFont = BaseFont.CreateFont($@"{_basePathToFont}\PDF\Fonts\TimesNewRoman.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            Font = new Font(baseFont, 14, Font.NORMAL);
            FontBold = new Font(baseFont, 12, Font.BOLD);
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
    }
}
