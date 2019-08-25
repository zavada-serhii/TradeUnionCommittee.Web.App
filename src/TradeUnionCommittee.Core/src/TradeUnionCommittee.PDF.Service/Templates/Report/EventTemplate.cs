using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using TradeUnionCommittee.PDF.Service.Entities;
using TradeUnionCommittee.PDF.Service.Enums;

namespace TradeUnionCommittee.PDF.Service.Templates.Report
{
    internal class EventTemplate : BaseSettings
    {
        public void CreateBody(Document doc, TypeReport typeReport, IReadOnlyCollection<EventEmployeeEntity> model)
        {
            var table = new PdfPTable(5) { WidthPercentage = 100 };
            AddEmptyParagraph(doc, 1);

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
        }

        public void AddSum(Document doc, decimal sumAmount, decimal sumDiscount, decimal generalSum)
        {
            doc.Add(AddParagraph($"Сумма дотацій - {sumAmount} {Сurrency}", Element.ALIGN_RIGHT));
            doc.Add(AddParagraph($"Сумма знижок - {sumDiscount} {Сurrency}", Element.ALIGN_RIGHT));
            doc.Add(AddParagraph($"Загальна сумма - {generalSum} {Сurrency}", Element.ALIGN_RIGHT));
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