using iTextSharp.text;
using System.Collections.Generic;
using System.Linq;
using TradeUnionCommittee.BLL.PDF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.TestPDF.ReportTemplates
{
    internal class TestCulturalTemplate : BaseTemplate, IBaseReportTemplate<CulturalEmployees>
    {
        public decimal CreateBody(Document doc, IEnumerable<CulturalEmployees> model)
        {
            var table = AddPdfPTable(4);

            //---------------------------------------------------------------

            AddEmptyParagraph(doc, 3);
            table.WidthPercentage = 100;

            //---------------------------------------------------------------

            AddCell(table, FontBold, 1, "Назва закладу");
            AddCell(table, FontBold, 1, "Розмір дотації");
            AddCell(table, FontBold, 1, "Розмір знижки");
            AddCell(table, FontBold, 1, "Дата візиту");

            foreach (var cultural in model)
            {
                AddCell(table, Font, 1, $"{cultural.IdCulturalNavigation.Name}");
                AddCell(table, Font, 1, $"{cultural.Amount} {Сurrency}");
                AddCell(table, Font, 1, $"{cultural.Discount} {Сurrency}");
                AddCell(table, Font, 1, $"{cultural.DateVisit:dd/MM/yyyy}");
            }

            doc.Add(table);

            //---------------------------------------------------------------

            var sumAmount = model.Sum(x => x.Amount);
            var sumDiscount = model.Sum(x => x.Discount);
            var generalSum = sumAmount + sumDiscount;

            AddSubsidiesSum(doc, sumAmount);
            AddDiscountSum(doc, sumDiscount);
            AddGeneralSum(doc, generalSum);

            return generalSum;
        }
    }
}