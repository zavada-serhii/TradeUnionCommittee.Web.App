using System;
using System.Linq;
using TradeUnionCommittee.BLL.PDF.Models;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.PDF.ReportTemplates
{
    internal class EventTemplate : BaseTemplate, IReportTemplate
    {
        public void CreateTemplateReport(ReportModel model)
        {

            var fullName = model.EventEmployees.First().IdEmployeeNavigation;
            var eventName = GetEventName(model.EventEmployees.First().IdEventNavigation.Type);

            //---------------------------------------------------------------

            var doc = CreateDocument(model.PathToSave);
            var table = AddPdfPTable(5);

            //---------------------------------------------------------------
            
            AddNameReport(doc, $"Звіт по {eventName} члена профспілки");
            AddFullNameEmployee(doc, $"{fullName.FirstName} {fullName.SecondName} {fullName.Patronymic}");
            AddPeriod(doc, model.StartDate, model.EndDate);
            AddEmptyParagraph(doc, 3);
            table.WidthPercentage = 100;

            //---------------------------------------------------------------

            AddCell(table, FontBold, 1, $"Назва {GetEventName(eventName)}");
            AddCell(table, FontBold, 1, "Розмір дотації");
            AddCell(table, FontBold, 1, "Розмір знижки");
            AddCell(table, FontBold, 1, "Дата початку");
            AddCell(table, FontBold, 1, "Дата закінчення");

            foreach (var ev in model.EventEmployees)
            {
                AddCell(table, Font, 1, $"{ev.IdEventNavigation.Name}");
                AddCell(table, Font, 1, $"{ev.Amount} {Сurrency}");
                AddCell(table, Font, 1, $"{ev.Discount} {Сurrency}");
                AddCell(table, Font, 1, $"{ev.StartDate:dd/MM/yyyy}");
                AddCell(table, Font, 1, $"{ev.EndDate:dd/MM/yyyy}");
            }

            doc.Add(table);

            //---------------------------------------------------------------

            var sumAmount = model.EventEmployees.Sum(x => x.Amount);
            var sumDiscount = model.EventEmployees.Sum(x => x.Discount);

            AddSubsidiesSum(doc, sumAmount);
            AddDiscountSum(doc, sumDiscount);
            AddGeneralSum(doc, sumAmount + sumDiscount);
            AddSignature(doc);

            //---------------------------------------------------------------

            SaveFile(doc);
        }

        private string GetEventName(TypeEvent @event)
        {
            switch (@event)
            {
                case TypeEvent.Travel:
                    return "поїздкам";
                case TypeEvent.Wellness:
                    return "оздоровленням";
                case TypeEvent.Tour:
                    return "путівкам";
                default:
                    throw new ArgumentOutOfRangeException(nameof(@event), @event, null);
            }
        }

        private string GetEventName(string @event)
        {
            switch (@event)
            {
                case "поїздкам":
                    return "Поїздки";
                case "оздоровленням":
                    return "Оздоровлення";
                case "путівкам":
                    return "Путівки";
                default:
                    throw new ArgumentOutOfRangeException(nameof(@event), @event, null);
            }
        }
    }
}