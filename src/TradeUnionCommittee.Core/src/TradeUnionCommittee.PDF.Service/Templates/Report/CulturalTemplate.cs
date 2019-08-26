using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.Linq;
using TradeUnionCommittee.PDF.Service.Entities;

namespace TradeUnionCommittee.PDF.Service.Templates.Report
{
    internal class CulturalTemplate : BaseSettings
    {
        private readonly Document _document;
        private readonly IEnumerable<CulturalEmployeeEntity> _model;

        public decimal GeneralSum { get; private set; }

        public CulturalTemplate(Document doc, IEnumerable<CulturalEmployeeEntity> model)
        {
            _document = doc;
            _model = model;
        }

        public void CreateBody()
        {
            _document.Add(AddBoldParagraph("Культурно-просвітницькі заклади", Element.ALIGN_CENTER));
            AddEmptyParagraph(_document, 1);

            //---------------------------------------------------------------

            var table = new PdfPTable(4) { WidthPercentage = 100 };

            AddCell(table, FontBold, 1, "Назва закладу");
            AddCell(table, FontBold, 1, "Розмір дотації");
            AddCell(table, FontBold, 1, "Розмір знижки");
            AddCell(table, FontBold, 1, "Дата візиту");

            foreach (var cultural in _model)
            {
                AddCell(table, Font, 1, $"{cultural.Name}");
                AddCell(table, Font, 1, $"{cultural.Amount} {Сurrency}");
                AddCell(table, Font, 1, $"{cultural.Discount} {Сurrency}");
                AddCell(table, Font, 1, $"{cultural.Date:dd/MM/yyyy}");
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
    }
}