using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.Linq;
using TradeUnionCommittee.PDF.Service.Entities;

namespace TradeUnionCommittee.PDF.Service.Templates.Report
{
    internal class MaterialAidTemplate : BaseSettings
    {
        public void CreateBody(Document doc, IReadOnlyCollection<MaterialIncentivesEmployeeEntity> model)
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
                doc.Add(AddParagraph($"Cумма від {item.Name} - {item.Sum} {Сurrency}", Element.ALIGN_RIGHT));
            }

            doc.Add(AddParagraph($"Загальна сумма - {generalSum} {Сurrency}", Element.ALIGN_RIGHT));
        }
    }
}