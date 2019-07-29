using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Reflection;

namespace TradeUnionCommittee.PDF.Service
{
    public class BaseSettings
    {
        private static readonly string BasePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        protected readonly Font Font;
        protected readonly Font FontBold;
        protected const string Сurrency = "грн";

        protected BaseSettings()
        {
            var pathDivider = GetPathDividerForTargetPlatform();
            var baseFont = BaseFont.CreateFont($@"{BasePath}{pathDivider}Fonts{pathDivider}TimesNewRoman.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
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

        private char GetPathDividerForTargetPlatform()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.Win32NT:
                case PlatformID.WinCE:
                    return '\\';
                case PlatformID.Unix:
                case PlatformID.MacOSX:
                    return '/';
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
