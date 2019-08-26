using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.Linq;
using TradeUnionCommittee.PDF.Service.Entities;

namespace TradeUnionCommittee.PDF.Service.Templates.Report
{
    internal class AwardTemplate : BaseSettings
    {
        private readonly Document _document;
        private readonly IEnumerable<MaterialIncentivesEmployeeEntity> _model;

        public decimal GeneralSum { get; private set; }

        public AwardTemplate(Document document, IEnumerable<MaterialIncentivesEmployeeEntity> model)
        {
            _document = document;
            _model = model;
        }

        public void CreateBody()
        {
            _document.Add(AddBoldParagraph("Матеріальні заохочення", Element.ALIGN_CENTER));
            AddEmptyParagraph(_document, 1);

            //---------------------------------------------------------------

            var table = new PdfPTable(6) { WidthPercentage = 100 };

            AddCell(table, FontBold, 2, "Джерело");
            AddCell(table, FontBold, 2, "Розмір");
            AddCell(table, FontBold, 2, "Дата отримання");

            foreach (var award in _model)
            {
                AddCell(table, Font, 2, $"{award.Name}");
                AddCell(table, Font, 2, $"{award.Amount} {Сurrency}");
                AddCell(table, Font, 2, $"{award.Date:dd/MM/yyyy}");
            }

            _document.Add(table);

            //---------------------------------------------------------------

            
        }

        public void AddSum()
        {
            var sumAmount = _model.Sum(x => x.Amount);

            foreach (var item in _model.GroupBy(l => l.Name).Select(cl => new { cl.First().Name, Sum = cl.Sum(c => c.Amount) }).ToList())
            {
                _document.Add(AddParagraph($"Cумма від {item.Name} - {item.Sum} {Сurrency}", Element.ALIGN_RIGHT));
            }

            _document.Add(AddParagraph($"Загальна сумма - {sumAmount} {Сurrency}", Element.ALIGN_RIGHT));

            AddEmptyParagraph(_document, 2);

            GeneralSum = sumAmount;
        }
    }
}