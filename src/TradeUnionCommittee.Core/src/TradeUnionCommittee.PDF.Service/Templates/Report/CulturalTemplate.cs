using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using TradeUnionCommittee.PDF.Service.Entities;

namespace TradeUnionCommittee.PDF.Service.Templates.Report
{
    internal class CulturalTemplate : BaseSettings
    {
        public void CreateBody(Document doc, IReadOnlyCollection<CulturalEmployeeEntity> model)
        {
            var table = new PdfPTable(4) { WidthPercentage = 100 };
            AddEmptyParagraph(doc, 1);

            //---------------------------------------------------------------

            AddCell(table, FontBold, 1, "Назва закладу");
            AddCell(table, FontBold, 1, "Розмір дотації");
            AddCell(table, FontBold, 1, "Розмір знижки");
            AddCell(table, FontBold, 1, "Дата візиту");

            foreach (var cultural in model)
            {
                AddCell(table, Font, 1, $"{cultural.Name}");
                AddCell(table, Font, 1, $"{cultural.Amount} {Сurrency}");
                AddCell(table, Font, 1, $"{cultural.Discount} {Сurrency}");
                AddCell(table, Font, 1, $"{cultural.Date:dd/MM/yyyy}");
            }

            doc.Add(table);
        }

        public void AddSum(Document doc, decimal sumAmount, decimal sumDiscount, decimal generalSum)
        {
            doc.Add(AddParagraph($"Сумма дотацій - {sumAmount} {Сurrency}", Element.ALIGN_RIGHT));
            doc.Add(AddParagraph($"Сумма знижок - {sumDiscount} {Сurrency}", Element.ALIGN_RIGHT));
            doc.Add(AddParagraph($"Загальна сумма - {generalSum} {Сurrency}", Element.ALIGN_RIGHT));
        }
    }
}