using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeUnionCommittee.PDF.Service.Entities;
using TradeUnionCommittee.PDF.Service.Enums;

namespace TradeUnionCommittee.PDF.Service.Templates.Report
{
    internal class EventTemplate : BaseSettings
    {
        private readonly Document _document;
        private readonly IEnumerable<EventEmployeeEntity> _model;

        public decimal GeneralSum { get; private set; }

        public EventTemplate(Document document, IEnumerable<EventEmployeeEntity> model)
        {
            _document = document;
            _model = model;
        }

        public void CreateBody()
        {
            var eventName = GetEventName(_model.First().TypeEvent);

            //---------------------------------------------------------------

            _document.Add(AddBoldParagraph(eventName, Element.ALIGN_CENTER));
            AddEmptyParagraph(_document, 1);

            //---------------------------------------------------------------

            var table = new PdfPTable(5) { WidthPercentage = 100 };

            AddCell(table, FontBold, 1, $"Назва {eventName}");
            AddCell(table, FontBold, 1, "Розмір дотації");
            AddCell(table, FontBold, 1, "Розмір знижки");
            AddCell(table, FontBold, 1, "Дата початку");
            AddCell(table, FontBold, 1, "Дата закінчення");

            foreach (var ev in _model)
            {
                AddCell(table, Font, 1, $"{ev.Name}");
                AddCell(table, Font, 1, $"{ev.Amount} {Сurrency}");
                AddCell(table, Font, 1, $"{ev.Discount} {Сurrency}");
                AddCell(table, Font, 1, $"{ev.StartDate:dd/MM/yyyy}");
                AddCell(table, Font, 1, $"{ev.EndDate:dd/MM/yyyy}");
            }

            _document.Add(table);
        }

        public void AddSum()
        {
            var sumAmount = _model.Sum(x => x.Amount);
            var sumDiscount = _model.Sum(x => x.Discount);
            var generalSum = sumAmount + sumDiscount;

            _document.Add(AddParagraph($"Сумма дотацій - {sumAmount} {Сurrency}", Element.ALIGN_RIGHT));
            _document.Add(AddParagraph($"Сумма знижок - {sumDiscount} {Сurrency}", Element.ALIGN_RIGHT));
            _document.Add(AddParagraph($"Загальна сумма - {generalSum} {Сurrency}", Element.ALIGN_RIGHT));

            AddEmptyParagraph(_document, 2);

            GeneralSum = generalSum;
        }

        private string GetEventName(TypeEvent typeEvent)
        {
            switch (typeEvent)
            {
                case TypeEvent.Travel:
                    return "Поїздки";
                case TypeEvent.Wellness:
                    return "Оздоровлення";
                case TypeEvent.Tour:
                    return "Путівки";
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeEvent), typeEvent, null);
            }
        }
    }
}