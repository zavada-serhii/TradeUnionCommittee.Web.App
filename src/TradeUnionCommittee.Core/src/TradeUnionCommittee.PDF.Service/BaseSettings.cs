using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Reflection;

namespace TradeUnionCommittee.PDF.Service
{
    public abstract class BaseSettings
    {
        protected readonly Font Font;
        protected readonly Font FontBold;
        protected const string Сurrency = "грн";

        protected BaseSettings()
        {
            var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var pathToFont = Path.Combine(basePath, "Fonts", "TimesNewRoman.ttf");

            var baseFont = BaseFont.CreateFont(pathToFont, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            Font = new Font(baseFont, 14, Font.NORMAL);
            FontBold = new Font(baseFont, 12, Font.BOLD);
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

        protected Paragraph AddParagraph(string str, int alignment)
        {
            return new Paragraph(str, Font) { Alignment = alignment };
        }

        protected Paragraph AddBoldParagraph(string str, int alignment)
        {
            return new Paragraph(str, FontBold) {Alignment = alignment };
        }

        protected void AddEmptyParagraph(IElementListener document, int count)
        {
            for (var i = 0; i < count; i++)
            {
                document.Add(new Phrase(Environment.NewLine));
            }
        }
    }
}
