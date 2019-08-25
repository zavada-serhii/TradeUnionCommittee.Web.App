using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Linq;
using System.Text;
using TradeUnionCommittee.PDF.Service.Enums;
using TradeUnionCommittee.PDF.Service.Interfaces;
using TradeUnionCommittee.PDF.Service.Models;
using TradeUnionCommittee.PDF.Service.ReportTemplates;

namespace TradeUnionCommittee.PDF.Service.Services
{
    public class ReportGeneratorService : BaseSettings, IReportGeneratorService
    {
        public byte[] Generate(ReportModel model)
        {
            try
            {
                using (var stream = new MemoryStream())
                {
                    var document = new Document();
                    var writer = PdfWriter.GetInstance(document, stream);

                    document.Open();

                    AddTitle(model, document);

                    AddBodyReport(model, document);

                    AddSignature(document);

                    document.Close();
                    writer.Close();

                    return stream.ToArray();
                }
            }
            catch (Exception)
            {
                return new byte[0];
            }
        }

        private void AddTitle(ReportModel model, IElementListener doc)
        {
            switch (model.Type)
            {
                case TypeReport.All:
                    doc.Add(GetBoldParagraph("Звіт по всім дотаційним заходам члена профспілки", Element.ALIGN_CENTER));
                    break;
                case TypeReport.MaterialAid:
                    doc.Add(GetBoldParagraph("Звіт по матеріальним допомогам члена профспілки", Element.ALIGN_CENTER));
                    break;
                case TypeReport.Award:
                    doc.Add(GetBoldParagraph("Звіт по матеріальним заохоченням члена профспілки", Element.ALIGN_CENTER));
                    break;
                case TypeReport.Travel:
                    doc.Add(GetBoldParagraph("Звіт по поїздкам члена профспілки", Element.ALIGN_CENTER));
                    break;
                case TypeReport.Wellness:
                    doc.Add(GetBoldParagraph("Звіт по оздоровленням члена профспілки", Element.ALIGN_CENTER));
                    break;
                case TypeReport.Tour:
                    doc.Add(GetBoldParagraph("Звіт по путівкам члена профспілки", Element.ALIGN_CENTER));
                    break;
                case TypeReport.Cultural:
                    doc.Add(GetBoldParagraph("Звіт по культурно-просвітницьким закладам члена профспілки", Element.ALIGN_CENTER));
                    break;
                case TypeReport.Gift:
                    doc.Add(GetBoldParagraph("Звіт по подарункам члена профспілки", Element.ALIGN_CENTER));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            doc.Add(GetBoldParagraph(model.FullNameEmployee, Element.ALIGN_CENTER));
            AddPeriod(model, doc);
        }

        private void AddPeriod(ReportModel model, IElementListener doc)
        {
            doc.Add(GetBoldParagraph($"за період з {model.StartDate:dd/MM/yyyy}р по {model.EndDate:dd/MM/yyyy}р", Element.ALIGN_CENTER));
            if (model.Type == TypeReport.All)
            {
                AddEmptyParagraph(doc, 2);
            }
        }

        private void AddBodyReport(ReportModel model, Document doc)
        {
            switch (model.Type)
            {
                case TypeReport.All:
                    FullReport(model, doc);
                    break;
                case TypeReport.MaterialAid:
                    new MaterialAidTemplate().CreateBody(doc, model.MaterialAidEmployees);
                    break;
                case TypeReport.Award:
                    new AwardTemplate().CreateBody(doc, model.AwardEmployees);
                    break;
                case TypeReport.Cultural:
                    new CulturalTemplate().CreateBody(doc, model.CulturalEmployees);
                    break;
                case TypeReport.Travel:
                case TypeReport.Wellness:
                case TypeReport.Tour:
                    new EventTemplate().CreateBody(doc, model.Type, model.EventEmployees);
                    break;
                case TypeReport.Gift:
                    new GiftTemplate().CreateBody(doc, model.GiftEmployees);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void FullReport(DataModel model, Document doc)
        {
            var generalSum = 0.0M;

            if (model.MaterialAidEmployees.Any())
            {
                doc.Add(GetBoldParagraph("Матеріальні допомоги", Element.ALIGN_CENTER));
                new MaterialAidTemplate().CreateBody(doc, model.MaterialAidEmployees);
                AddEmptyParagraph(doc, 2);

                generalSum += model.MaterialAidEmployees.Sum(x => x.Amount);
            }

            if (model.AwardEmployees.Any())
            {
                doc.Add(GetBoldParagraph("Матеріальні заохочення", Element.ALIGN_CENTER));
                new AwardTemplate().CreateBody(doc, model.AwardEmployees);
                AddEmptyParagraph(doc, 2);

                generalSum += model.AwardEmployees.Sum(x => x.Amount);
            }

            if (model.CulturalEmployees.Any())
            {
                doc.Add(GetBoldParagraph("Культурно-просвітницькі заклади", Element.ALIGN_CENTER));
                new CulturalTemplate().CreateBody(doc, model.CulturalEmployees);
                AddEmptyParagraph(doc, 2);

                generalSum += model.CulturalEmployees.Sum(x => x.Amount);
                generalSum += model.CulturalEmployees.Sum(x => x.Discount);
            }

            if (model.EventEmployees.Any(x => x.TypeEvent == TypeEvent.Travel))
            {
                doc.Add(GetBoldParagraph("Поїздки", Element.ALIGN_CENTER));
                var travel = model.EventEmployees.Where(x => x.TypeEvent == TypeEvent.Travel).ToList();
                new EventTemplate().CreateBody(doc, TypeReport.Travel, travel);
                AddEmptyParagraph(doc, 2);

                generalSum += travel.Sum(x => x.Amount);
                generalSum += travel.Sum(x => x.Discount);
            }

            if (model.EventEmployees.Any(x => x.TypeEvent == TypeEvent.Wellness))
            {
                doc.Add(GetBoldParagraph("Оздоровлення", Element.ALIGN_CENTER));
                var wellness = model.EventEmployees.Where(x => x.TypeEvent == TypeEvent.Wellness).ToList();
                new EventTemplate().CreateBody(doc, TypeReport.Wellness, wellness);
                AddEmptyParagraph(doc, 2);

                generalSum += wellness.Sum(x => x.Amount);
                generalSum += wellness.Sum(x => x.Discount);
            }

            if (model.EventEmployees.Any(x => x.TypeEvent == TypeEvent.Tour))
            {
                doc.Add(GetBoldParagraph("Путівки", Element.ALIGN_CENTER));
                var tour = model.EventEmployees.Where(x => x.TypeEvent == TypeEvent.Tour).ToList();
                new EventTemplate().CreateBody(doc, TypeReport.Tour, tour);
                AddEmptyParagraph(doc, 2);

                generalSum += tour.Sum(x => x.Amount);
                generalSum += tour.Sum(x => x.Discount);
            }

            if (model.GiftEmployees.Any())
            {
                doc.Add(GetBoldParagraph("Подарунки", Element.ALIGN_CENTER));
                new GiftTemplate().CreateBody(doc, model.GiftEmployees);
                AddEmptyParagraph(doc, 2);

                generalSum += model.GiftEmployees.Sum(x => x.Amount);
                generalSum += model.GiftEmployees.Sum(x => x.Discount);
            }

            AddEmptyParagraph(doc, 2);
            doc.Add(GetDefaultParagraph($"Cумма - {generalSum} {Сurrency}", Element.ALIGN_RIGHT));
            AddEmptyParagraph(doc, 2);
        }

        private void AddSignature(IElementListener doc)
        {
            var builder = new StringBuilder();

            builder.Append("Головний бухгалтер ППО ОНУ імені І.І.Мечникова ");
            builder.Append(new string('_', 10));
            builder.Append(' ').Append(' ');
            builder.Append(new string('_', 18));

            doc.Add(GetDefaultParagraph(builder.ToString(), Element.ALIGN_RIGHT));
        }
    }
}
