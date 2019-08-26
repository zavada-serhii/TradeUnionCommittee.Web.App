using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.Linq;
using TradeUnionCommittee.PDF.Service.Entities;
using TradeUnionCommittee.PDF.Service.Helpers;

namespace TradeUnionCommittee.PDF.Service.Templates.Report
{
    public class CulturalTemplate
    {
        private readonly PdfHelper _pdfHelper;
        private readonly Document _document;
        private readonly IEnumerable<CulturalEmployeeEntity> _model;

        public decimal GeneralSum { get; private set; }

        public CulturalTemplate(PdfHelper pdfHelper, Document document, IEnumerable<CulturalEmployeeEntity> model)
        {
            _pdfHelper = pdfHelper;
            _document = document;
            _model = model;
        }

        public void CreateBody()
        {
            var table = new PdfPTable(4) { WidthPercentage = 100 };

            _pdfHelper.AddTitleTemplate(table, 4, "Культурно-просвітницькі заклади");
            _pdfHelper.AddBoldCell(table, 1, "Назва закладу");
            _pdfHelper.AddBoldCell(table, 1, "Розмір дотації");
            _pdfHelper.AddBoldCell(table, 1, "Розмір знижки");
            _pdfHelper.AddBoldCell(table, 1, "Дата візиту");

            foreach (var cultural in _model)
            {
                _pdfHelper.AddCell(table, 1, $"{cultural.Name}");
                _pdfHelper.AddCell(table, 1, $"{cultural.Amount} {_pdfHelper.Сurrency}");
                _pdfHelper.AddCell(table, 1, $"{cultural.Discount} {_pdfHelper.Сurrency}");
                _pdfHelper.AddCell(table, 1, $"{cultural.Date:dd/MM/yyyy}");
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
    }
}