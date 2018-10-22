using System.Collections.Generic;
using System.Linq;
using iTextSharp.text;
using TradeUnionCommittee.BLL.PDF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.TestPDF.ReportTemplates
{
    internal class TestMaterialAidTemplate : BaseTemplate, IBaseReportTemplate<MaterialAidEmployees>
    {
        public decimal CreateBody(Document doc, IEnumerable<MaterialAidEmployees> model)
        {
            var table = AddPdfPTable(6);

            //---------------------------------------------------------------
            
            AddEmptyParagraph(doc, 3);
            table.WidthPercentage = 100;

            //---------------------------------------------------------------

            AddCell(table, FontBold, 2, "Джерело");
            AddCell(table, FontBold, 2, "Розмір");
            AddCell(table, FontBold, 2, "Дата отримання");

            foreach (var materialInterestse in model)
            {
                AddCell(table, Font, 2, $"{materialInterestse.IdMaterialAidNavigation.Name}");
                AddCell(table, Font, 2, $"{materialInterestse.Amount} {Сurrency}");
                AddCell(table, Font, 2, $"{materialInterestse.DateIssue:dd/MM/yyyy}");
            }

            doc.Add(table);

            //---------------------------------------------------------------

            var generalSum = model.Sum(x => x.Amount);

            AddSumFrom(doc, model.GroupBy(l => l.IdMaterialAidNavigation.Name).Select(cl => new { cl.First().IdMaterialAidNavigation.Name, Sum = cl.Sum(c => c.Amount) }).ToList());
            AddGeneralSum(doc, generalSum);

            //---------------------------------------------------------------

            return generalSum;
        }
    }
}