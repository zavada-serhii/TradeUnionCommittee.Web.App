using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Linq;
using System.Text;
using TradeUnionCommittee.PDF.Service.Enums;
using TradeUnionCommittee.PDF.Service.Interfaces;
using TradeUnionCommittee.PDF.Service.Models;
using TradeUnionCommittee.PDF.Service.Templates.Report;

namespace TradeUnionCommittee.PDF.Service.Services
{
    public class ReportGeneratorService : BaseSettings, IReportGeneratorService
    {
        private readonly Document _document;

        public ReportGeneratorService()
        {
            _document = new Document();
        }

        public byte[] Generate(ReportModel model)
        {
            try
            {
                using (var stream = new MemoryStream())
                {
                    var writer = PdfWriter.GetInstance(_document, stream);
                    _document.Open();

                    AddTitle(model);
                    AddBody(model);
                    AddSignature();

                    _document.Close();
                    writer.Close();

                    return stream.ToArray();
                }
            }
            catch (Exception)
            {
                return new byte[0];
            }
        }

        private void AddTitle(ReportModel model)
        {
            switch (model.Type)
            {
                case TypeReport.All:
                    _document.Add(AddBoldParagraph("Звіт по всім дотаційним заходам члена профспілки", Element.ALIGN_CENTER));
                    break;
                case TypeReport.MaterialAid:
                    _document.Add(AddBoldParagraph("Звіт по матеріальним допомогам члена профспілки", Element.ALIGN_CENTER));
                    break;
                case TypeReport.Award:
                    _document.Add(AddBoldParagraph("Звіт по матеріальним заохоченням члена профспілки", Element.ALIGN_CENTER));
                    break;
                case TypeReport.Travel:
                    _document.Add(AddBoldParagraph("Звіт по поїздкам члена профспілки", Element.ALIGN_CENTER));
                    break;
                case TypeReport.Wellness:
                    _document.Add(AddBoldParagraph("Звіт по оздоровленням члена профспілки", Element.ALIGN_CENTER));
                    break;
                case TypeReport.Tour:
                    _document.Add(AddBoldParagraph("Звіт по путівкам члена профспілки", Element.ALIGN_CENTER));
                    break;
                case TypeReport.Cultural:
                    _document.Add(AddBoldParagraph("Звіт по культурно-просвітницьким закладам члена профспілки", Element.ALIGN_CENTER));
                    break;
                case TypeReport.Gift:
                    _document.Add(AddBoldParagraph("Звіт по подарункам члена профспілки", Element.ALIGN_CENTER));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _document.Add(AddBoldParagraph(model.FullNameEmployee, Element.ALIGN_CENTER));
            _document.Add(AddBoldParagraph($"за період з {model.StartDate:dd/MM/yyyy}р по {model.EndDate:dd/MM/yyyy}р", Element.ALIGN_CENTER));
            if (model.Type == TypeReport.All)
            {
                AddEmptyParagraph(_document, 2);
            }
        }

        private void AddBody(ReportModel model)
        {
            switch (model.Type)
            {
                case TypeReport.All:
                    FullReport(model);
                    break;
                case TypeReport.MaterialAid:
                    new MaterialAidTemplate().CreateBody(_document, model.MaterialAidEmployees.ToList());
                    break;
                case TypeReport.Award:
                    new AwardTemplate().CreateBody(_document, model.AwardEmployees.ToList());
                    break;
                case TypeReport.Cultural:
                    new CulturalTemplate().CreateBody(_document, model.CulturalEmployees.ToList());
                    break;
                case TypeReport.Travel:
                case TypeReport.Wellness:
                case TypeReport.Tour:
                    new EventTemplate().CreateBody(_document, model.Type, model.EventEmployees.ToList());
                    break;
                case TypeReport.Gift:
                    new GiftTemplate().CreateBody(_document, model.GiftEmployees.ToList());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void FullReport(DataModel model)
        {
            var generalSum = 0.0M;

            if (model.MaterialAidEmployees.Any())
            {
                _document.Add(AddBoldParagraph("Матеріальні допомоги", Element.ALIGN_CENTER));
                new MaterialAidTemplate().CreateBody(_document, model.MaterialAidEmployees.ToList());
                AddEmptyParagraph(_document, 2);

                generalSum += model.MaterialAidEmployees.Sum(x => x.Amount);
            }

            if (model.AwardEmployees.Any())
            {
                _document.Add(AddBoldParagraph("Матеріальні заохочення", Element.ALIGN_CENTER));
                new AwardTemplate().CreateBody(_document, model.AwardEmployees.ToList());
                AddEmptyParagraph(_document, 2);

                generalSum += model.AwardEmployees.Sum(x => x.Amount);
            }

            if (model.CulturalEmployees.Any())
            {
                _document.Add(AddBoldParagraph("Культурно-просвітницькі заклади", Element.ALIGN_CENTER));
                new CulturalTemplate().CreateBody(_document, model.CulturalEmployees.ToList());
                AddEmptyParagraph(_document, 2);

                generalSum += model.CulturalEmployees.Sum(x => x.Amount);
                generalSum += model.CulturalEmployees.Sum(x => x.Discount);
            }

            if (model.EventEmployees.Any(x => x.TypeEvent == TypeEvent.Travel))
            {
                _document.Add(AddBoldParagraph("Поїздки", Element.ALIGN_CENTER));
                var travel = model.EventEmployees.Where(x => x.TypeEvent == TypeEvent.Travel).ToList();
                new EventTemplate().CreateBody(_document, TypeReport.Travel, travel);
                AddEmptyParagraph(_document, 2);

                generalSum += travel.Sum(x => x.Amount);
                generalSum += travel.Sum(x => x.Discount);
            }

            if (model.EventEmployees.Any(x => x.TypeEvent == TypeEvent.Wellness))
            {
                _document.Add(AddBoldParagraph("Оздоровлення", Element.ALIGN_CENTER));
                var wellness = model.EventEmployees.Where(x => x.TypeEvent == TypeEvent.Wellness).ToList();
                new EventTemplate().CreateBody(_document, TypeReport.Wellness, wellness);
                AddEmptyParagraph(_document, 2);

                generalSum += wellness.Sum(x => x.Amount);
                generalSum += wellness.Sum(x => x.Discount);
            }

            if (model.EventEmployees.Any(x => x.TypeEvent == TypeEvent.Tour))
            {
                _document.Add(AddBoldParagraph("Путівки", Element.ALIGN_CENTER));
                var tour = model.EventEmployees.Where(x => x.TypeEvent == TypeEvent.Tour).ToList();
                new EventTemplate().CreateBody(_document, TypeReport.Tour, tour);
                AddEmptyParagraph(_document, 2);

                generalSum += tour.Sum(x => x.Amount);
                generalSum += tour.Sum(x => x.Discount);
            }

            if (model.GiftEmployees.Any())
            {
                _document.Add(AddBoldParagraph("Подарунки", Element.ALIGN_CENTER));
                new GiftTemplate().CreateBody(_document, model.GiftEmployees.ToList());
                AddEmptyParagraph(_document, 2);

                generalSum += model.GiftEmployees.Sum(x => x.Amount);
                generalSum += model.GiftEmployees.Sum(x => x.Discount);
            }

            AddEmptyParagraph(_document, 2);
            _document.Add(AddParagraph($"Cумма - {generalSum} {Сurrency}", Element.ALIGN_RIGHT));
            AddEmptyParagraph(_document, 2);
        }

        private void AddSignature()
        {
            var builder = new StringBuilder();

            builder.Append("Головний бухгалтер ППО ОНУ імені І.І.Мечникова ");
            builder.Append(new string('_', 10));
            builder.Append(' ').Append(' ');
            builder.Append(new string('_', 18));

            _document.Add(AddParagraph(builder.ToString(), Element.ALIGN_RIGHT));
        }
    }
}
