using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.Linq;
using TradeUnionCommittee.PDF.Service.Entities;
using TradeUnionCommittee.PDF.Service.Helpers;

namespace TradeUnionCommittee.PDF.Service.Templates.Report
{
    public class AwardTemplate
    {
        private readonly PdfHelper _pdfHelper;
        private readonly Document _document;
        private readonly IEnumerable<MaterialIncentivesEmployeeEntity> _model;

        public decimal GeneralSum { get; private set; }

        public AwardTemplate(PdfHelper pdfHelper, Document document, IEnumerable<MaterialIncentivesEmployeeEntity> model)
        {
            _pdfHelper = pdfHelper;
            _document = document;
            _model = model;
        }

        public void CreateBody()
        {
            _document.Add(_pdfHelper.AddBoldParagraph("Матеріальні заохочення", Element.ALIGN_CENTER));
            _pdfHelper.AddEmptyParagraph(_document, 1);

            //---------------------------------------------------------------

            var table = new PdfPTable(6) { WidthPercentage = 100 };

            _pdfHelper.AddBoldCell(table, 2, "Джерело");
            _pdfHelper.AddBoldCell(table, 2, "Розмір");
            _pdfHelper.AddBoldCell(table, 2, "Дата отримання");

            foreach (var award in _model)
            {
                _pdfHelper.AddCell(table, 2, $"{award.Name}");
                _pdfHelper.AddCell(table, 2, $"{award.Amount} {_pdfHelper.Сurrency}");
                _pdfHelper.AddCell(table, 2, $"{award.Date:dd/MM/yyyy}");
            }

            _document.Add(table);
        }

        public void AddSum()
        {
            var sumAmount = _model.Sum(x => x.Amount);

            foreach (var item in _model.GroupBy(l => l.Name).Select(cl => new { cl.First().Name, Sum = cl.Sum(c => c.Amount) }).ToList())
            {
                _document.Add(_pdfHelper.AddParagraph($"Cумма від {item.Name} - {item.Sum} {_pdfHelper.Сurrency}", Element.ALIGN_RIGHT));
            }

            _document.Add(_pdfHelper.AddParagraph($"Загальна сумма - {sumAmount} {_pdfHelper.Сurrency}", Element.ALIGN_RIGHT));

            _pdfHelper.AddEmptyParagraph(_document, 2);

            GeneralSum = sumAmount;
        }
    }
}