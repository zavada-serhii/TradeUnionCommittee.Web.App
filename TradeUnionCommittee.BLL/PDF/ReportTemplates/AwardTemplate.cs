using System.Collections.Generic;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.PDF.ReportTemplates
{
    internal class AwardTemplate : BaseSettings
    {
        public decimal CreateBody(Document doc, IEnumerable<AwardEmployees> model)
        {
            var table = new PdfPTable(6);

            //---------------------------------------------------------------

            AddEmptyParagraph(doc, 3);
            table.WidthPercentage = 100;

            //---------------------------------------------------------------

            AddCell(table, FontBold, 2, "Джерело");
            AddCell(table, FontBold, 2, "Розмір");
            AddCell(table, FontBold, 2, "Дата отримання");

            foreach (var award in model)
            {
                AddCell(table, Font, 2, $"{award.IdAwardNavigation.Name}");
                AddCell(table, Font, 2, $"{award.Amount} {Сurrency}");
                AddCell(table, Font, 2, $"{award.DateIssue:dd/MM/yyyy}");
            }

            doc.Add(table);

            //---------------------------------------------------------------

            var generalSum = model.Sum(x => x.Amount);

            foreach (var item in model.GroupBy(l => l.IdAwardNavigation.Name).Select(cl => new { cl.First().IdAwardNavigation.Name, Sum = cl.Sum(c => c.Amount) }).ToList())
            {
                doc.Add(new Paragraph($"Cумма від {item.Name} - {item.Sum} {Сurrency}", Font) { Alignment = Element.ALIGN_RIGHT });
            }

            doc.Add(new Paragraph($"Загальна сумма - {generalSum} {Сurrency}", Font) { Alignment = Element.ALIGN_RIGHT });

            //---------------------------------------------------------------

            return generalSum;
        }
    }
}
