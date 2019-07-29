using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeUnionCommittee.PDF.Service.Entities;
using TradeUnionCommittee.PDF.Service.Enums;

namespace TradeUnionCommittee.PDF.Service.ReportTemplates
{
    internal class EventTemplate : BaseSettings
    {
        public decimal CreateBody(Document doc, TypeReport typeReport, IEnumerable<EventEmployeeEntity> model)
        {
            var table = new PdfPTable(5);

            //---------------------------------------------------------------

            AddEmptyParagraph(doc, 3);
            table.WidthPercentage = 100;

            //---------------------------------------------------------------

            AddCell(table, FontBold, 1, $"Назва {GetEventName(typeReport)}");
            AddCell(table, FontBold, 1, "Розмір дотації");
            AddCell(table, FontBold, 1, "Розмір знижки");
            AddCell(table, FontBold, 1, "Дата початку");
            AddCell(table, FontBold, 1, "Дата закінчення");

            foreach (var ev in model)
            {
                AddCell(table, Font, 1, $"{ev.Name}");
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

            doc.Add(new Paragraph($"Сумма дотацій - {sumAmount} {Сurrency}", Font) { Alignment = Element.ALIGN_RIGHT });
            doc.Add(new Paragraph($"Сумма знижок - {sumDiscount} {Сurrency}", Font) { Alignment = Element.ALIGN_RIGHT });
            doc.Add(new Paragraph($"Загальна сумма - {generalSum} {Сurrency}", Font) { Alignment = Element.ALIGN_RIGHT });

            return generalSum;
        }

        private string GetEventName(TypeReport typeEvent)
        {
            switch (typeEvent)
            {
                case TypeReport.Travel:
                    return "Поїздки";
                case TypeReport.Wellness:
                    return "Оздоровлення";
                case TypeReport.Tour:
                    return "Путівки";
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeEvent), typeEvent, null);
            }
        }
    }
}