using System.Collections.Generic;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.PDF.ReportTemplates
{
    internal class CulturalTemplate : BaseSettings, IBaseReportTemplate<CulturalEmployees>
    {
        public decimal CreateBody(Document doc, IEnumerable<CulturalEmployees> model)
        {
            var table = new PdfPTable(4);

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

            doc.Add(new Paragraph($"Сумма дотацій - {sumAmount} {Сurrency}", Font) { Alignment = Element.ALIGN_RIGHT });
            doc.Add(new Paragraph($"Сумма знижок - {sumDiscount} {Сurrency}", Font) { Alignment = Element.ALIGN_RIGHT });
            doc.Add(new Paragraph($"Загальна сумма - {generalSum} {Сurrency}", Font) { Alignment = Element.ALIGN_RIGHT });

            return generalSum;
        }
    }
}