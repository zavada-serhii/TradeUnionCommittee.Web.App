using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using TradeUnionCommittee.BLL.PDF;
using TradeUnionCommittee.BLL.TestPDF.Models;

namespace TradeUnionCommittee.BLL.TestPDF
{
    internal class PdfGenerator
    {
        private readonly Font _font;
        private readonly Font _fontBold;
        private const string Currency = "грн";
        private PdfWriter _writer;
        private Document _document;

        public PdfGenerator()
        {
            var baseFont = BaseFont.CreateFont(PathToFonts.PathToUkrainianFont, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            _font = new Font(baseFont, 14, Font.NORMAL);
            _fontBold = new Font(baseFont, 12, Font.BOLD);
        }

        //----------------------------------------------------------------------------------------------

        public void GenerateReport(ReportModel model)
        {
            _document = new Document();
            _writer = PdfWriter.GetInstance(_document, new FileStream($@"{model.PathToSave}{model.FullNameEmployee}.{model.Type}.{Guid.NewGuid()}.pdf", FileMode.Create));
            _document.Open();

            new ReportGenerator(_font, _fontBold, Currency).Generate(model, _document);

            _document.Close();
            _writer.Close();
        }

        //----------------------------------------------------------------------------------------------

        public void GenerateSearch(SearchModel model)
        {
            _document = new Document();
            _writer = PdfWriter.GetInstance(_document, new FileStream($@"{model.PathToSave}{model.Type}.{Guid.NewGuid()}.pdf", FileMode.Create));
            _document.Open();

            // SearchGenerator

            _document.Close();
            _writer.Close();
        }
    }
}