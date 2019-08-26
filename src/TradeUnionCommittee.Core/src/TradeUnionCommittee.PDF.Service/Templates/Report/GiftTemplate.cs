using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.Linq;
using TradeUnionCommittee.PDF.Service.Entities;

namespace TradeUnionCommittee.PDF.Service.Templates.Report
{
    internal class GiftTemplate : BaseSettings
    {
        private readonly Document _document;
        private readonly IEnumerable<GiftEmployeeEntity> _model;

        public decimal GeneralSum { get; private set; }

        public GiftTemplate(Document document, IEnumerable<GiftEmployeeEntity> model)
        {
            _document = document;
            _model = model;
        }

        public void CreateBody()
        {
            _document.Add(AddBoldParagraph("Подарунки", Element.ALIGN_CENTER));
            AddEmptyParagraph(_document, 1);

            //---------------------------------------------------------------

            var table = new PdfPTable(5) { WidthPercentage = 100 };

            AddCell(table, FontBold, 1, "Назва заходу");
            AddCell(table, FontBold, 1, "Назва подарунку");
            AddCell(table, FontBold, 1, "Ціна");
            AddCell(table, FontBold, 1, "Розмір знижки");
            AddCell(table, FontBold, 1, "Дата отримання");

            foreach (var gift in _model)
            {
                AddCell(table, Font, 1, $"{gift.Name}");
                AddCell(table, Font, 1, $"{gift.NameGift}");
                AddCell(table, Font, 1, $"{gift.Amount} {Сurrency}");
                AddCell(table, Font, 1, $"{gift.Discount} {Сurrency}");
                AddCell(table, Font, 2, $"{gift.Date:dd/MM/yyyy}");
            }

            _document.Add(table);
        }

        public void AddSum()
        {
            var sumAmount = _model.Sum(x => x.Amount);
            var sumDiscount = _model.Sum(x => x.Discount);
            var generalSum = sumAmount + sumDiscount;

            _document.Add(AddParagraph($"Сумма - {sumAmount} {Сurrency}", Element.ALIGN_RIGHT));
            _document.Add(AddParagraph($"Сумма знижок - {sumDiscount} {Сurrency}", Element.ALIGN_RIGHT));
            _document.Add(AddParagraph($"Загальна сумма - {generalSum} {Сurrency}", Element.ALIGN_RIGHT));

            AddEmptyParagraph(_document, 2);

            GeneralSum = generalSum;
        }
    }
}