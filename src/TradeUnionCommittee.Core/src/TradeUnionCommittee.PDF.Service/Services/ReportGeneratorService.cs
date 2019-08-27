using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Linq;
using System.Text;
using TradeUnionCommittee.PDF.Service.Enums;
using TradeUnionCommittee.PDF.Service.Helpers;
using TradeUnionCommittee.PDF.Service.Interfaces;
using TradeUnionCommittee.PDF.Service.Models;
using TradeUnionCommittee.PDF.Service.Templates;
using TradeUnionCommittee.PDF.Service.Templates.Report;

namespace TradeUnionCommittee.PDF.Service.Services
{
    public class ReportGeneratorService : IReportGeneratorService
    {
        private readonly PdfHelper _pdfHelper;

        public ReportGeneratorService()
        {
            _pdfHelper = new PdfHelper();
        }

        //----------------------------------------------------------------------------------------------------

        public (string FileName, byte[] Data) Generate(ReportModel model)
        {
            using (var stream = new MemoryStream())
            {
                var fileName = Guid.NewGuid().ToString();

                FillPdf(stream, model, fileName);
                SignPdf(stream, model, fileName);

                return (fileName, stream.ToArray());
            }
        }

        //----------------------------------------------------------------------------------------------------

        private void FillPdf(Stream stream, ReportModel model, string fileName)
        {
            var document = new Document();
            var writer = PdfWriter.GetInstance(document, stream);
            document.Open();

            AddTitle(document, model, fileName);
            AddBody(document, model);
            AddPlaceForSignatureAccountant(document);

            document.Close();
            writer.Close();
        }

        private void AddTitle(Document document, ReportModel model, string fileName)
        {
            StringBuilder title = new StringBuilder();

            switch (model.TypeReport)
            {
                case TypeReport.All:
                    title.Append("Звіт по всім дотаційним заходам члена профспілки");
                    break;
                case TypeReport.MaterialAid:
                    title.Append("Звіт по матеріальним допомогам члена профспілки");
                    break;
                case TypeReport.Award:
                    title.Append("Звіт по матеріальним заохоченням члена профспілки");
                    break;
                case TypeReport.Travel:
                    title.Append("Звіт по поїздкам члена профспілки");
                    break;
                case TypeReport.Wellness:
                    title.Append("Звіт по оздоровленням члена профспілки");
                    break;
                case TypeReport.Tour:
                    title.Append("Звіт по путівкам члена профспілки");
                    break;
                case TypeReport.Cultural:
                    title.Append("Звіт по культурно-просвітницьким закладам члена профспілки");
                    break;
                case TypeReport.Gift:
                    title.Append("Звіт по подарункам члена профспілки");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            title.Append(Environment.NewLine)
                .Append(model.FullNameEmployee)
                .Append(Environment.NewLine)
                .Append($"за період з {model.StartDate:dd/MM/yyyy}р по {model.EndDate:dd/MM/yyyy}р")
                .Append(Environment.NewLine)
                .Append($"eZign: {model.HashIdEmployee}")
                .Append(Environment.NewLine)
                .Append($"Reference: {fileName}");

            new TitleTemplate(_pdfHelper, document).AddTitle(title.ToString());
        }

        private void AddBody(Document document, ReportModel model)
        {
            var generalSum = 0.0M;

            if (model.MaterialAidEmployees.Any())
            {
                var template = new MaterialAidTemplate(_pdfHelper, document, model.MaterialAidEmployees);
                template.CreateBody();
                template.AddSum();

                generalSum += template.GeneralSum;
            }

            if (model.AwardEmployees.Any())
            {
                var template = new AwardTemplate(_pdfHelper, document, model.AwardEmployees);
                template.CreateBody();
                template.AddSum();

                generalSum += template.GeneralSum;
            }

            if (model.CulturalEmployees.Any())
            {
                var template = new CulturalTemplate(_pdfHelper, document, model.CulturalEmployees);
                template.CreateBody();
                template.AddSum();

                generalSum += template.GeneralSum;
            }

            if (model.EventEmployees.Any(x => x.TypeEvent == TypeEvent.Travel))
            {
                var template = new EventTemplate(_pdfHelper, document, model.EventEmployees.Where(x => x.TypeEvent == TypeEvent.Travel));
                template.CreateBody();
                template.AddSum();

                generalSum += template.GeneralSum;
            }

            if (model.EventEmployees.Any(x => x.TypeEvent == TypeEvent.Wellness))
            {
                var template = new EventTemplate(_pdfHelper, document, model.EventEmployees.Where(x => x.TypeEvent == TypeEvent.Wellness));
                template.CreateBody();
                template.AddSum();

                generalSum += template.GeneralSum;
            }

            if (model.EventEmployees.Any(x => x.TypeEvent == TypeEvent.Tour))
            {
                var template = new EventTemplate(_pdfHelper, document, model.EventEmployees.Where(x => x.TypeEvent == TypeEvent.Tour));
                template.CreateBody();
                template.AddSum();

                generalSum += template.GeneralSum;
            }

            if (model.GiftEmployees.Any())
            {
                var template = new GiftTemplate(_pdfHelper,document, model.GiftEmployees);
                template.CreateBody();
                template.AddSum();

                generalSum += template.GeneralSum;
            }

            if (model.TypeReport == TypeReport.All)
            {
                _pdfHelper.AddEmptyParagraph(document, 2);
                document.Add(_pdfHelper.AddParagraph($"Cумма - {generalSum} {_pdfHelper.Сurrency}", Element.ALIGN_RIGHT));
            }

            _pdfHelper.AddEmptyParagraph(document, 2);
        }

        private void AddPlaceForSignatureAccountant(IElementListener document)
        {
            var builder = new StringBuilder();

            builder.Append("Головний бухгалтер ППО ОНУ імені І.І.Мечникова ");
            builder.Append(new string('_', 10));
            builder.Append(' ').Append(' ');
            builder.Append(new string('_', 18));

            document.Add(_pdfHelper.AddParagraph(builder.ToString(), Element.ALIGN_RIGHT));
        }

        //----------------------------------------------------------------------------------------------------

        private void SignPdf(MemoryStream stream, ReportModel model, string fileName)
        {
            var reader = new PdfReader(stream.ToArray());
            PdfStamper stamper = new PdfStamper(reader, stream);
            var signature = $"eZign: {model.HashIdEmployee}        Reference: {fileName}";

            int numberOfPages = reader.NumberOfPages;
            for (int i = 2; i <= numberOfPages; i++)
            {
                ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_CENTER, new Phrase($"{i} / {numberOfPages}", _pdfHelper.Font), 297f, 15f, 0);
                ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_LEFT, new Phrase(signature, _pdfHelper.SignFont), 18f, 825f, 0);
            }

            stamper.Close();
            reader.Close();
        }
    }
}