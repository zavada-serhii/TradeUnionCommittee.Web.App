﻿using iTextSharp.text;
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
            var generalSum = 0.0M;

            if (model.MaterialAidEmployees.Any())
            {
                var template = new MaterialAidTemplate(_document, model.MaterialAidEmployees);
                template.CreateBody();
                template.AddSum();

                generalSum += template.GeneralSum;
            }

            if (model.AwardEmployees.Any())
            {
                var template = new AwardTemplate(_document, model.AwardEmployees);
                template.CreateBody();
                template.AddSum();

                generalSum += template.GeneralSum;
            }

            if (model.CulturalEmployees.Any())
            {
                var template = new CulturalTemplate(_document, model.CulturalEmployees);
                template.CreateBody();
                template.AddSum();

                generalSum += template.GeneralSum;
            }

            if (model.EventEmployees.Any(x => x.TypeEvent == TypeEvent.Travel))
            {
                var template = new EventTemplate(_document, model.EventEmployees.Where(x => x.TypeEvent == TypeEvent.Travel));
                template.CreateBody();
                template.AddSum();

                generalSum += template.GeneralSum;
            }

            if (model.EventEmployees.Any(x => x.TypeEvent == TypeEvent.Wellness))
            {
                var template = new EventTemplate(_document, model.EventEmployees.Where(x => x.TypeEvent == TypeEvent.Wellness));
                template.CreateBody();
                template.AddSum();

                generalSum += template.GeneralSum;
            }

            if (model.EventEmployees.Any(x => x.TypeEvent == TypeEvent.Tour))
            {
                var template = new EventTemplate(_document, model.EventEmployees.Where(x => x.TypeEvent == TypeEvent.Tour));
                template.CreateBody();
                template.AddSum();

                generalSum += template.GeneralSum;
            }

            if (model.GiftEmployees.Any())
            {
                var template = new GiftTemplate(_document, model.GiftEmployees);
                template.CreateBody();
                template.AddSum();

                generalSum += template.GeneralSum;
            }

            if (model.Type == TypeReport.All)
            {
                AddEmptyParagraph(_document, 2);
                _document.Add(AddParagraph($"Cумма - {generalSum} {Сurrency}", Element.ALIGN_RIGHT));
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