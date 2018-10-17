using System.Collections.Generic;
using System.Linq;
using iTextSharp.text;
using TradeUnionCommittee.BLL.PDF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.TestPDF.ReportTemplates
{
    internal class TestAwardTemplate : BaseTemplate, IBaseTemplate<AwardEmployees>
    {
        public decimal CreateBody(Document doc, IEnumerable<AwardEmployees> model)
        {
            var table = AddPdfPTable(6);

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

            AddSumFrom(doc, model.GroupBy(l => l.IdAwardNavigation.Name).Select(cl => new { cl.First().IdAwardNavigation.Name, Sum = cl.Sum(c => c.Amount) }).ToList());
            AddGeneralSum(doc, generalSum);

            //---------------------------------------------------------------

            return generalSum;
        }
    }
}
