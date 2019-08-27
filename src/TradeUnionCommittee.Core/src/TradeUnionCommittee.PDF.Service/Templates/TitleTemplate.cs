using iTextSharp.text;
using System;
using System.Text;
using TradeUnionCommittee.PDF.Service.Helpers;

namespace TradeUnionCommittee.PDF.Service.Templates
{
    public class TitleTemplate
    {
        private readonly PdfHelper _pdfHelper;
        private readonly Document _document;

        public TitleTemplate(PdfHelper pdfHelper, Document document)
        {
            _pdfHelper = pdfHelper;
            _document = document;
        }

        public void AddTitle(string value)
        {
            var builder = new StringBuilder();

            var rect = new Rectangle(577, 825, 18, 15) { BorderColor = BaseColor.Black, BorderWidth = 1};
            rect.EnableBorderSide(1);
            rect.EnableBorderSide(2);
            rect.EnableBorderSide(4);
            rect.EnableBorderSide(8);

            for (var i = 0; i < 5; i++)
            {
                builder.Append(Environment.NewLine);
            }

            builder.Append(value);

            _document.Add(rect);
            _document.Add(_pdfHelper.AddParagraph(builder.ToString(), Element.ALIGN_CENTER));
            _document.NewPage();
        }
    }
}