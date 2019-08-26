using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.Linq;
using TradeUnionCommittee.PDF.Service.Entities;
using TradeUnionCommittee.PDF.Service.Helpers;

namespace TradeUnionCommittee.PDF.Service.Templates.Report
{
    public class GiftTemplate
    {
        private readonly PdfHelper _pdfHelper;
        private readonly Document _document;
        private readonly IEnumerable<GiftEmployeeEntity> _model;

        public decimal GeneralSum { get; private set; }

        public GiftTemplate(PdfHelper pdfHelper, Document document, IEnumerable<GiftEmployeeEntity> model)
        {
            _pdfHelper = pdfHelper;
            _document = document;
            _model = model;
        }

        public void CreateBody()
        {
            _document.Add(_pdfHelper.AddBoldParagraph("Подарунки", Element.ALIGN_CENTER));
            _pdfHelper.AddEmptyParagraph(_document, 1);

            //---------------------------------------------------------------

            var table = new PdfPTable(5) { WidthPercentage = 100 };

            _pdfHelper.AddBoldCell(table, 1, "Назва заходу");
            _pdfHelper.AddBoldCell(table, 1, "Назва подарунку");
            _pdfHelper.AddBoldCell(table, 1, "Ціна");
            _pdfHelper.AddBoldCell(table, 1, "Розмір знижки");
            _pdfHelper.AddBoldCell(table, 1, "Дата отримання");

            foreach (var gift in _model)
            {
                _pdfHelper.AddCell(table, 1, $"{gift.Name}");
                _pdfHelper.AddCell(table, 1, $"{gift.NameGift}");
                _pdfHelper.AddCell(table, 1, $"{gift.Amount} {_pdfHelper.Сurrency}");
                _pdfHelper.AddCell(table, 1, $"{gift.Discount} {_pdfHelper.Сurrency}");
                _pdfHelper.AddCell(table, 2, $"{gift.Date:dd/MM/yyyy}");
            }

            _document.Add(table);
        }

        public void AddSum()
        {
            var sumAmount = _model.Sum(x => x.Amount);
            var sumDiscount = _model.Sum(x => x.Discount);
            var generalSum = sumAmount + sumDiscount;

            _document.Add(_pdfHelper.AddParagraph($"Сумма - {sumAmount} {_pdfHelper.Сurrency}", Element.ALIGN_RIGHT));
            _document.Add(_pdfHelper.AddParagraph($"Сумма знижок - {sumDiscount} {_pdfHelper.Сurrency}", Element.ALIGN_RIGHT));
            _document.Add(_pdfHelper.AddParagraph($"Загальна сумма - {generalSum} {_pdfHelper.Сurrency}", Element.ALIGN_RIGHT));

            _pdfHelper.AddEmptyParagraph(_document, 2);

            GeneralSum = generalSum;
        }
    }
}