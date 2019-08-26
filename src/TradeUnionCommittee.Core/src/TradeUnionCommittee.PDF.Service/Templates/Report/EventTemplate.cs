using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeUnionCommittee.PDF.Service.Entities;
using TradeUnionCommittee.PDF.Service.Enums;
using TradeUnionCommittee.PDF.Service.Helpers;

namespace TradeUnionCommittee.PDF.Service.Templates.Report
{
    public class EventTemplate
    {
        private readonly PdfHelper _pdfHelper;
        private readonly Document _document;
        private readonly IEnumerable<EventEmployeeEntity> _model;

        public decimal GeneralSum { get; private set; }

        public EventTemplate(PdfHelper pdfHelper, Document document, IEnumerable<EventEmployeeEntity> model)
        {
            _pdfHelper = pdfHelper;
            _document = document;
            _model = model;
        }

        public void CreateBody()
        {
            var table = new PdfPTable(5) { WidthPercentage = 100 };
            var eventName = GetEventName(_model.First().TypeEvent);

            _pdfHelper.AddTitleTemplate(table, 5, eventName);
            _pdfHelper.AddBoldCell(table, 1, $"Назва {eventName}");
            _pdfHelper.AddBoldCell(table, 1, "Розмір дотації");
            _pdfHelper.AddBoldCell(table, 1, "Розмір знижки");
            _pdfHelper.AddBoldCell(table, 1, "Дата початку");
            _pdfHelper.AddBoldCell(table, 1, "Дата закінчення");

            foreach (var ev in _model)
            {
                _pdfHelper.AddCell(table, 1, $"{ev.Name}");
                _pdfHelper.AddCell(table, 1, $"{ev.Amount} {_pdfHelper.Сurrency}");
                _pdfHelper.AddCell(table, 1, $"{ev.Discount} {_pdfHelper.Сurrency}");
                _pdfHelper.AddCell(table, 1, $"{ev.StartDate:dd/MM/yyyy}");
                _pdfHelper.AddCell(table, 1, $"{ev.EndDate:dd/MM/yyyy}");
            }

            _document.Add(table);
        }

        public void AddSum()
        {
            var sumAmount = _model.Sum(x => x.Amount);
            var sumDiscount = _model.Sum(x => x.Discount);
            var generalSum = sumAmount + sumDiscount;

            _document.Add(_pdfHelper.AddParagraph($"Сумма дотацій - {sumAmount} {_pdfHelper.Сurrency}", Element.ALIGN_RIGHT));
            _document.Add(_pdfHelper.AddParagraph($"Сумма знижок - {sumDiscount} {_pdfHelper.Сurrency}", Element.ALIGN_RIGHT));
            _document.Add(_pdfHelper.AddParagraph($"Загальна сумма - {generalSum} {_pdfHelper.Сurrency}", Element.ALIGN_RIGHT));

            _pdfHelper.AddEmptyParagraph(_document, 2);

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