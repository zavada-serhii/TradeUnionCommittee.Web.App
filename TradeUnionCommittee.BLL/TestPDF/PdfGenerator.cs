using System;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.PDF;
using TradeUnionCommittee.BLL.TestPDF.Models;

namespace TradeUnionCommittee.BLL.TestPDF
{
    internal class PdfGenerator
    {
        private readonly Font _font;
        private readonly Font _fontBold;
        private string _currency = "грн";
        private PdfWriter _writer;

        public PdfGenerator()
        {
            var baseFont = BaseFont.CreateFont(PathToFonts.PathToUkrainianFont, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            _font = new Font(baseFont, 14, Font.NORMAL);
            _fontBold = new Font(baseFont, 12, Font.BOLD);
        }

        //----------------------------------------------------------------------------------------------

        public void Generate(TestReportModel model, PdfType type)
        {
            var document = new Document();
            _writer = PdfWriter.GetInstance(document, new FileStream($@"{model.PathToSave}{model.FullNameEmployee}.{model.Type}.{Guid.NewGuid()}.pdf", FileMode.Create));
            document.Open();

            switch (type)
            {
                case PdfType.Report:
                    new ReportGenerator(_font, _fontBold, _currency).Generate(model, document);
                    break;
                case PdfType.Search:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            
            document.Close();
            _writer.Close();
        }
    }
}