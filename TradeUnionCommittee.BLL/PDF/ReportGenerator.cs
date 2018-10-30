using System;
using System.Linq;
using iTextSharp.text;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.PDF.Models;
using TradeUnionCommittee.BLL.PDF.ReportTemplates;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.PDF
{
    public class ReportGenerator : BaseSettings
    {
        private decimal _materialAidEmployeesGeneralSum;
        private decimal _awardEmployeesGeneralSum;
        private decimal _culturalEmployeesGeneralSum;
        private decimal _travelEventEmployeesGeneralSum;
        private decimal _wellnessEventEmployeesGeneralSum;
        private decimal _tourEventEmployeesGeneralSum;
        private decimal _giftEmployeesGeneralSum;

        public void Generate(ReportModel model, Document document)
        {
            AddNameReport(model, document);
            document.Add(new Paragraph(model.FullNameEmployee, FontBold) { Alignment = Element.ALIGN_CENTER });
            AddPeriod(model, document);

            //----------------------------------------------------------------------------------------------

            AddBodyReport(model, document);

            //----------------------------------------------------------------------------------------------

            AddEmptyParagraph(document, 2);
            AddAllGeneralSum(model.Type, document);

            document.Add(new Paragraph($"Головний бухгалтер ППО ОНУ імені І.І.Мечникова {new string('_', 10)}  {new string('_', 18)}", Font) { Alignment = Element.ALIGN_RIGHT });
        }

        private void AddNameReport(ReportModel model, IElementListener doc)
        {
            switch (model.Type)
            {
                case ReportType.All:
                    doc.Add(new Paragraph("Звіт по всім дотаційним заходам члена профспілки", FontBold) { Alignment = Element.ALIGN_CENTER });
                    break;
                case ReportType.MaterialAid:
                    doc.Add(new Paragraph("Звіт по матеріальним допомогам члена профспілки", FontBold) { Alignment = Element.ALIGN_CENTER });
                    break;
                case ReportType.Award:
                    doc.Add(new Paragraph("Звіт по матеріальним заохоченням члена профспілки", FontBold) { Alignment = Element.ALIGN_CENTER });
                    break;
                case ReportType.Travel:
                    doc.Add(new Paragraph("Звіт по поїздкам члена профспілки", FontBold) { Alignment = Element.ALIGN_CENTER });
                    break;
                case ReportType.Wellness:
                    doc.Add(new Paragraph("Звіт по оздоровленням члена профспілки", FontBold) { Alignment = Element.ALIGN_CENTER });
                    break;
                case ReportType.Tour:
                    doc.Add(new Paragraph("Звіт по путівкам члена профспілки", FontBold) { Alignment = Element.ALIGN_CENTER });
                    break;
                case ReportType.Cultural:
                    doc.Add(new Paragraph("Звіт по культурно-просвітницьким закладам члена профспілки", FontBold) { Alignment = Element.ALIGN_CENTER });
                    break;
                case ReportType.Gift:
                    doc.Add(new Paragraph("Звіт по подарункам члена профспілки", FontBold) { Alignment = Element.ALIGN_CENTER });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void AddPeriod(ReportModel model, IElementListener doc)
        {
            doc.Add(new Paragraph($"за період з {model.StartDate:dd/MM/yyyy}р по {model.EndDate:dd/MM/yyyy}р", FontBold) { Alignment = Element.ALIGN_CENTER });
            if (model.Type == ReportType.All)
            {
                AddEmptyParagraph(doc, 2);
            }
        }

        private void AddBodyReport(ReportModel model, Document doc)
        {
            switch (model.Type)
            {
                case ReportType.All:
                    FullReport(model, doc);
                    break;
                case ReportType.MaterialAid:
                    _materialAidEmployeesGeneralSum = new MaterialAidTemplate().CreateBody(doc, model.MaterialAidEmployees);
                    break;
                case ReportType.Award:
                    _awardEmployeesGeneralSum = new AwardTemplate().CreateBody(doc, model.AwardEmployees);
                    break;
                case ReportType.Travel:
                    _travelEventEmployeesGeneralSum = new EventTemplate().CreateBody(doc, model.EventEmployees);
                    break;
                case ReportType.Wellness:
                    _wellnessEventEmployeesGeneralSum = new EventTemplate().CreateBody(doc, model.EventEmployees);
                    break;
                case ReportType.Tour:
                    _tourEventEmployeesGeneralSum = new EventTemplate().CreateBody(doc, model.EventEmployees);
                    break;
                case ReportType.Cultural:
                    _culturalEmployeesGeneralSum = new CulturalTemplate().CreateBody(doc, model.CulturalEmployees);
                    break;
                case ReportType.Gift:
                    _giftEmployeesGeneralSum = new GiftTemplate().CreateBody(doc, model.GiftEmployees);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void FullReport(DataModel model, Document doc)
        {
            if (model.MaterialAidEmployees.Any())
            {
                doc.Add(new Paragraph("Матеріальні допомоги", FontBold) { Alignment = Element.ALIGN_CENTER });
                _materialAidEmployeesGeneralSum = new MaterialAidTemplate().CreateBody(doc, model.MaterialAidEmployees);
                AddEmptyParagraph(doc, 2);
            }

            if (model.AwardEmployees.Any())
            {
                doc.Add(new Paragraph("Матеріальні заохочення", FontBold) { Alignment = Element.ALIGN_CENTER });
                _awardEmployeesGeneralSum = new AwardTemplate().CreateBody(doc, model.AwardEmployees);
                AddEmptyParagraph(doc, 2);
            }

            if (model.EventEmployees.Any(x => x.IdEventNavigation.Type == TypeEvent.Travel))
            {
                doc.Add(new Paragraph("Поїздки", FontBold) { Alignment = Element.ALIGN_CENTER });
                _travelEventEmployeesGeneralSum = new EventTemplate().CreateBody(doc, model.EventEmployees.Where(x => x.IdEventNavigation.Type == TypeEvent.Travel));
                AddEmptyParagraph(doc, 2);
            }

            if (model.EventEmployees.Any(x => x.IdEventNavigation.Type == TypeEvent.Wellness))
            {
                doc.Add(new Paragraph("Оздоровлення", FontBold) { Alignment = Element.ALIGN_CENTER });
                _wellnessEventEmployeesGeneralSum = new EventTemplate().CreateBody(doc, model.EventEmployees.Where(x => x.IdEventNavigation.Type == TypeEvent.Wellness));
                AddEmptyParagraph(doc, 2);
            }

            if (model.EventEmployees.Any(x => x.IdEventNavigation.Type == TypeEvent.Tour))
            {
                doc.Add(new Paragraph("Путівки", FontBold) { Alignment = Element.ALIGN_CENTER });
                _tourEventEmployeesGeneralSum = new EventTemplate().CreateBody(doc, model.EventEmployees.Where(x => x.IdEventNavigation.Type == TypeEvent.Tour));
                AddEmptyParagraph(doc, 2);
            }

            if (model.CulturalEmployees.Any())
            {
                doc.Add(new Paragraph("Культурно-просвітницькі заклади", FontBold) { Alignment = Element.ALIGN_CENTER });
                _culturalEmployeesGeneralSum = new CulturalTemplate().CreateBody(doc, model.CulturalEmployees);
                AddEmptyParagraph(doc, 2);
            }

            if (model.GiftEmployees.Any())
            {
                doc.Add(new Paragraph("Подарунки", FontBold) { Alignment = Element.ALIGN_CENTER });
                _giftEmployeesGeneralSum = new GiftTemplate().CreateBody(doc, model.GiftEmployees);
                AddEmptyParagraph(doc, 2);
            }
        }

        private void AddAllGeneralSum(ReportType predicate, IElementListener doc)
        {
            if (predicate == ReportType.All)
            {
                var sum = _materialAidEmployeesGeneralSum + _awardEmployeesGeneralSum + _culturalEmployeesGeneralSum +
                          _travelEventEmployeesGeneralSum + _wellnessEventEmployeesGeneralSum + _tourEventEmployeesGeneralSum +
                          _giftEmployeesGeneralSum;

                doc.Add(new Paragraph($"Cумма - {sum} {Сurrency}", Font) { Alignment = Element.ALIGN_RIGHT });

                AddEmptyParagraph(doc, 2);
            }
        }
    }
}
