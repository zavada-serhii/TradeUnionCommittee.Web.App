using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using TradeUnionCommittee.PDF.Service.Entities;

namespace TradeUnionCommittee.PDF.Service.Templates.Report
{
    internal class GiftTemplate : BaseSettings
    {
        public void CreateBody(Document doc, IReadOnlyCollection<GiftEmployeeEntity> model)
        {
            var table = new PdfPTable(5) { WidthPercentage = 100 };
            AddEmptyParagraph(doc, 1);

            //---------------------------------------------------------------

            AddCell(table, FontBold, 1, "Назва заходу");
            AddCell(table, FontBold, 1, "Назва подарунку");
            AddCell(table, FontBold, 1, "Ціна");
            AddCell(table, FontBold, 1, "Розмір знижки");
            AddCell(table, FontBold, 1, "Дата отримання");

            foreach (var gift in model)
            {
                AddCell(table, Font, 1, $"{gift.Name}");
                AddCell(table, Font, 1, $"{gift.NameGift}");
                AddCell(table, Font, 1, $"{gift.Amount} {Сurrency}");
                AddCell(table, Font, 1, $"{gift.Discount} {Сurrency}");
                AddCell(table, Font, 2, $"{gift.Date:dd/MM/yyyy}");
            }

            doc.Add(table);
        }

        public void AddSum(Document doc, decimal sumAmount, decimal sumDiscount, decimal generalSum)
        {
            doc.Add(AddParagraph($"Сумма - {sumAmount} {Сurrency}", Element.ALIGN_RIGHT));
            doc.Add(AddParagraph($"Сумма знижок - {sumDiscount} {Сurrency}", Element.ALIGN_RIGHT));
            doc.Add(AddParagraph($"Загальна сумма - {generalSum} {Сurrency}", Element.ALIGN_RIGHT));
        }
    }
}