using System;
using System.IO;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using TradeUnionCommittee.BLL.PDF;
using TradeUnionCommittee.BLL.TestPDF.Models;
using TradeUnionCommittee.BLL.TestPDF.ReportTemplates;

namespace TradeUnionCommittee.BLL.TestPDF
{
    internal class GeneratePdf
    {
        private readonly BaseFont _baseFont;
        private readonly Font _font;
        private readonly Font _fontBold;
        private string _сurrency = "грн";
        private PdfWriter _writer;

        private decimal _materialAidEmployeesGeneralSum = 0M;
        private decimal _awardEmployeesGeneralSum = 0M;
        

        public GeneratePdf()
        {
            _baseFont = BaseFont.CreateFont(PathToFonts.PathToUkrainianFont, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            _font = new Font(_baseFont, 14, Font.NORMAL);
            _fontBold = new Font(_baseFont, 12, Font.BOLD);
        }

        //----------------------------------------------------------------------------------------------

        public void Generate(TestReportModel model)
        {
            var document = new Document();
            _writer = PdfWriter.GetInstance(document, new FileStream($@"{model.PathToSave}{Guid.NewGuid()}.pdf", FileMode.Create));
            document.Open();

            //TODO: AddNameReport

            document.Add(new Paragraph(model.FullNameEmployee, _fontBold) { Alignment = Element.ALIGN_CENTER });
            document.Add(new Paragraph($"за період з {model.StartDate:dd/MM/yyyy}р по {model.EndDate:dd/MM/yyyy}р", _fontBold) { Alignment = Element.ALIGN_CENTER });
            for (var i = 0; i < 3; i++)
            {
                document.Add(new Paragraph(" ") { Alignment = Element.ALIGN_CENTER });
            }

            //----------------------------------------------------------------------------------------------

            if (model.MaterialAidEmployees.Any())
            {
                _materialAidEmployeesGeneralSum = new TestMaterialAidTemplate().CreateBody(document, model.MaterialAidEmployees);
            }

            if (model.AwardEmployees.Any())
            {
                _awardEmployeesGeneralSum = new TestAwardTemplate().CreateBody(document, model.AwardEmployees);
            }

            //----------------------------------------------------------------------------------------------

            for (var i = 0; i < 2; i++)
            {
                document.Add(new Paragraph(" ") { Alignment = Element.ALIGN_CENTER });
            }
            document.Add(new Paragraph($"Головний бухгалтер ППО ОНУ імені І.І.Мечникова {new string('_', 10)}  {new string('_', 18)}", _font) { Alignment = Element.ALIGN_RIGHT });
            document.Close();
            _writer.Close();
        }
    }
}