using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.Linq;
using TradeUnionCommittee.PDF.Service.Entities;

namespace TradeUnionCommittee.PDF.Service.ReportTemplates
{
    internal class MaterialAidTemplate : BaseSettings
    {
        public decimal CreateBody(Document doc, IEnumerable<MaterialIncentivesEmployeeEntity> model)
        {
            var table = new PdfPTable(6);

            //---------------------------------------------------------------

            AddEmptyParagraph(doc, 3);
            table.WidthPercentage = 100;

            //---------------------------------------------------------------

            AddCell(table, FontBold, 2, "Джерело");
            AddCell(table, FontBold, 2, "Розмір");
            AddCell(table, FontBold, 2, "Дата отримання");

            foreach (var materialInterestse in model)
            {
                AddCell(table, Font, 2, $"{materialInterestse.Name}");
                AddCell(table, Font, 2, $"{materialInterestse.Amount} {Сurrency}");
                AddCell(table, Font, 2, $"{materialInterestse.Date:dd/MM/yyyy}");
            }

            doc.Add(table);

            //---------------------------------------------------------------

            var generalSum = model.Sum(x => x.Amount);

            foreach (var item in model.GroupBy(l => l.Name).Select(cl => new { cl.First().Name, Sum = cl.Sum(c => c.Amount) }).ToList())
            {
                doc.Add(new Paragraph($"Cумма від {item.Name} - {item.Sum} {Сurrency}", Font) { Alignment = Element.ALIGN_RIGHT });
            }

            doc.Add(new Paragraph($"Загальна сумма - {generalSum} {Сurrency}", Font) { Alignment = Element.ALIGN_RIGHT });

            //---------------------------------------------------------------

            return generalSum;
        }
    }
}