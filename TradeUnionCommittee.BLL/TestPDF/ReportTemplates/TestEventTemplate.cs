using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeUnionCommittee.BLL.PDF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.TestPDF.ReportTemplates
{
    internal class TestEventTemplate : BaseTemplate, IBaseTemplate<EventEmployees>
    {
        public decimal CreateBody(Document doc, IEnumerable<EventEmployees> model)
        {
            var table = AddPdfPTable(5);

            //---------------------------------------------------------------

            AddEmptyParagraph(doc, 3);
            table.WidthPercentage = 100;

            //---------------------------------------------------------------

            AddCell(table, FontBold, 1, $"Назва {GetEventName(model.First().IdEventNavigation.Type)}");
            AddCell(table, FontBold, 1, "Розмір дотації");
            AddCell(table, FontBold, 1, "Розмір знижки");
            AddCell(table, FontBold, 1, "Дата початку");
            AddCell(table, FontBold, 1, "Дата закінчення");

            foreach (var ev in model)
            {
                AddCell(table, Font, 1, $"{ev.IdEventNavigation.Name}");
                AddCell(table, Font, 1, $"{ev.Amount} {Сurrency}");
                AddCell(table, Font, 1, $"{ev.Discount} {Сurrency}");
                AddCell(table, Font, 1, $"{ev.StartDate:dd/MM/yyyy}");
                AddCell(table, Font, 1, $"{ev.EndDate:dd/MM/yyyy}");
            }

            doc.Add(table);

            //---------------------------------------------------------------


            var sumAmount = model.Sum(x => x.Amount);
            var sumDiscount = model.Sum(x => x.Discount);
            var generalSum = sumAmount + sumDiscount;

            AddSubsidiesSum(doc, sumAmount);
            AddDiscountSum(doc, sumDiscount);
            AddGeneralSum(doc, generalSum);

            return generalSum;
        }

        private string GetEventName(TypeEvent @event)
        {
            switch (@event)
            {
                case TypeEvent.Travel:
                    return "Поїздки";
                case TypeEvent.Wellness:
                    return "Оздоровлення";
                case TypeEvent.Tour:
                    return "Путівки";
                default:
                    throw new ArgumentOutOfRangeException(nameof(@event), @event, null);
            }
        }
    }
}