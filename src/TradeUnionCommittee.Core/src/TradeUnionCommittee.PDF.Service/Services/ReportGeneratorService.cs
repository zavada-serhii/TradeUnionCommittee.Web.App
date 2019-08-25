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
            AddEmptyParagraph(_document, 2);
        }

        private void AddBody(ReportModel model)
        {
            var sum = 0.0M;

            if (model.MaterialAidEmployees.Any())
            {
                var sumAmount = model.MaterialAidEmployees.Sum(x => x.Amount);

                _document.Add(AddBoldParagraph("Матеріальні допомоги", Element.ALIGN_CENTER));

                var template = new MaterialAidTemplate();
                template.CreateBody(_document, model.MaterialAidEmployees.ToList());
                template.AddSum(_document, sumAmount);

                AddEmptyParagraph(_document, 2);

                sum += sumAmount;
            }

            if (model.AwardEmployees.Any())
            {
                var sumAmount = model.AwardEmployees.Sum(x => x.Amount);

                _document.Add(AddBoldParagraph("Матеріальні заохочення", Element.ALIGN_CENTER));

                var template = new AwardTemplate();
                template.CreateBody(_document, model.AwardEmployees.ToList());
                template.AddSum(_document, sumAmount);

                AddEmptyParagraph(_document, 2);

                sum += sumAmount;
            }

            if (model.CulturalEmployees.Any())
            {
                var sumAmount = model.CulturalEmployees.Sum(x => x.Amount);
                var sumDiscount = model.CulturalEmployees.Sum(x => x.Discount);
                var generalSum = sumAmount + sumDiscount;

                _document.Add(AddBoldParagraph("Культурно-просвітницькі заклади", Element.ALIGN_CENTER));

                var template = new CulturalTemplate();
                template.CreateBody(_document, model.CulturalEmployees.ToList());
                template.AddSum(_document, sumAmount, sumDiscount, generalSum);

                AddEmptyParagraph(_document, 2);

                sum += generalSum;
            }

            if (model.EventEmployees.Any(x => x.TypeEvent == TypeEvent.Travel))
            {
                var travel = model.EventEmployees.Where(x => x.TypeEvent == TypeEvent.Travel).ToList();
                var sumAmount = travel.Sum(x => x.Amount);
                var sumDiscount = travel.Sum(x => x.Discount);
                var generalSum = sumAmount + sumDiscount;

                _document.Add(AddBoldParagraph("Поїздки", Element.ALIGN_CENTER));

                var template = new EventTemplate();
                template.CreateBody(_document, TypeReport.Travel, travel);
                template.AddSum(_document, sumAmount, sumDiscount, generalSum);

                AddEmptyParagraph(_document, 2);

                sum += generalSum;
            }

            if (model.EventEmployees.Any(x => x.TypeEvent == TypeEvent.Wellness))
            {
                var wellness = model.EventEmployees.Where(x => x.TypeEvent == TypeEvent.Wellness).ToList();
                var sumAmount = wellness.Sum(x => x.Amount);
                var sumDiscount = wellness.Sum(x => x.Discount);
                var generalSum = sumAmount + sumDiscount;

                _document.Add(AddBoldParagraph("Оздоровлення", Element.ALIGN_CENTER));

                var template = new EventTemplate();
                template.CreateBody(_document, TypeReport.Wellness, wellness);
                template.AddSum(_document, sumAmount, sumDiscount, generalSum);

                AddEmptyParagraph(_document, 2);

                sum += generalSum;
            }

            if (model.EventEmployees.Any(x => x.TypeEvent == TypeEvent.Tour))
            {
                var tour = model.EventEmployees.Where(x => x.TypeEvent == TypeEvent.Tour).ToList();
                var sumAmount = tour.Sum(x => x.Amount);
                var sumDiscount = tour.Sum(x => x.Discount);
                var generalSum = sumAmount + sumDiscount;

                _document.Add(AddBoldParagraph("Путівки", Element.ALIGN_CENTER));

                var template = new EventTemplate();
                template.CreateBody(_document, TypeReport.Tour, tour);
                template.AddSum(_document, sumAmount, sumDiscount, generalSum);

                AddEmptyParagraph(_document, 2);

                sum += generalSum;
            }

            if (model.GiftEmployees.Any())
            {
                var sumAmount = model.GiftEmployees.Sum(x => x.Amount);
                var sumDiscount = model.GiftEmployees.Sum(x => x.Discount);
                var generalSum = sumAmount + sumDiscount;

                _document.Add(AddBoldParagraph("Подарунки", Element.ALIGN_CENTER));

                var template = new GiftTemplate();
                template.CreateBody(_document, model.GiftEmployees.ToList());
                template.AddSum(_document, sumAmount, sumDiscount, generalSum);

                AddEmptyParagraph(_document, 2);

                sum += generalSum;
            }

            if (model.Type == TypeReport.All)
            {
                AddEmptyParagraph(_document, 2);
                _document.Add(AddParagraph($"Cумма - {sum} {Сurrency}", Element.ALIGN_RIGHT));
            }

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