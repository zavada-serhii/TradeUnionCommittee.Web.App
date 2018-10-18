using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using TradeUnionCommittee.BLL.Enums;
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

        private decimal _materialAidEmployeesGeneralSum = 0;
        private decimal _awardEmployeesGeneralSum = 0;
        private decimal _culturalEmployeesGeneralSum = 0;
        private decimal _travelEventEmployeesGeneralSum = 0;
        private decimal _wellnessEventEmployeesGeneralSum = 0;
        private decimal _tourEventEmployeesGeneralSum = 0;
        private decimal _giftEmployeesGeneralSum = 0;



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

            AddNameReport(model, document);
            document.Add(new Paragraph(model.FullNameEmployee, _fontBold) { Alignment = Element.ALIGN_CENTER });
            document.Add(new Paragraph($"за період з {model.StartDate:dd/MM/yyyy}р по {model.EndDate:dd/MM/yyyy}р", _fontBold) { Alignment = Element.ALIGN_CENTER });

            //----------------------------------------------------------------------------------------------

            AddBodyReport(model, document);

            //----------------------------------------------------------------------------------------------

            for (var i = 0; i < 2; i++)
            {
                document.Add(new Paragraph(" ") { Alignment = Element.ALIGN_CENTER });
            }

            if (model.Type == ReportType.All)
            {
                var sum = _materialAidEmployeesGeneralSum + _awardEmployeesGeneralSum + _culturalEmployeesGeneralSum +
                          _travelEventEmployeesGeneralSum + _wellnessEventEmployeesGeneralSum + _tourEventEmployeesGeneralSum +
                          _giftEmployeesGeneralSum;

                document.Add(new Paragraph($"Cумма - {sum} {_сurrency}", _font) { Alignment = Element.ALIGN_RIGHT });

                for (var i = 0; i < 2; i++)
                {
                    document.Add(new Paragraph(" ") { Alignment = Element.ALIGN_CENTER });
                }
            }

            document.Add(new Paragraph($"Головний бухгалтер ППО ОНУ імені І.І.Мечникова {new string('_', 10)}  {new string('_', 18)}", _font) { Alignment = Element.ALIGN_RIGHT });
            document.Close();
            _writer.Close();
        }

        private void AddNameReport(TestBaseModel model, IElementListener doc)
        {
            switch (model.Type)
            {
                case ReportType.All:
                    doc.Add(new Paragraph("Звіт по всім дотаційним заходам члена профспілки", _fontBold) { Alignment = Element.ALIGN_CENTER });
                    break;
                case ReportType.MaterialAid:
                    doc.Add(new Paragraph("Звіт по матеріальним допомогам члена профспілки", _fontBold) { Alignment = Element.ALIGN_CENTER });
                    break;
                case ReportType.Award:
                    doc.Add(new Paragraph("Звіт по матеріальним заохоченням члена профспілки", _fontBold) { Alignment = Element.ALIGN_CENTER });
                    break;
                case ReportType.Travel:
                    doc.Add(new Paragraph("Звіт по поїздкам члена профспілки", _fontBold) { Alignment = Element.ALIGN_CENTER });
                    break;
                case ReportType.Wellness:
                    doc.Add(new Paragraph("Звіт по оздоровленням члена профспілки", _fontBold) { Alignment = Element.ALIGN_CENTER });
                    break;
                case ReportType.Tour:
                    doc.Add(new Paragraph("Звіт по путівкам члена профспілки", _fontBold) { Alignment = Element.ALIGN_CENTER });
                    break;
                case ReportType.Cultural:
                    doc.Add(new Paragraph("Звіт по культурно-просвітницьким закладам члена профспілки", _fontBold) { Alignment = Element.ALIGN_CENTER });
                    break;
                case ReportType.Gift:
                    doc.Add(new Paragraph("Звіт по подарункам члена профспілки", _fontBold) { Alignment = Element.ALIGN_CENTER });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void AddBodyReport(TestReportModel model, Document doc)
        {
            switch (model.Type)
            {
                case ReportType.All:

                case ReportType.MaterialAid:
                    _materialAidEmployeesGeneralSum = new TestMaterialAidTemplate().CreateBody(doc, model.MaterialAidEmployees);
                    break;
                case ReportType.Award:
                    _awardEmployeesGeneralSum = new TestAwardTemplate().CreateBody(doc, model.AwardEmployees);
                    break;
                case ReportType.Travel:
                    _travelEventEmployeesGeneralSum = new TestEventTemplate().CreateBody(doc, model.EventEmployees);
                    break;
                case ReportType.Wellness:
                    _wellnessEventEmployeesGeneralSum = new TestEventTemplate().CreateBody(doc, model.EventEmployees);
                    break;
                case ReportType.Tour:
                    _tourEventEmployeesGeneralSum = new TestEventTemplate().CreateBody(doc, model.EventEmployees);
                    break;
                case ReportType.Cultural:
                    _culturalEmployeesGeneralSum = new TestCulturalTemplate().CreateBody(doc, model.CulturalEmployees);
                    break;
                case ReportType.Gift:
                    _giftEmployeesGeneralSum = new TestGiftTemplate().CreateBody(doc, model.GiftEmployees);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}