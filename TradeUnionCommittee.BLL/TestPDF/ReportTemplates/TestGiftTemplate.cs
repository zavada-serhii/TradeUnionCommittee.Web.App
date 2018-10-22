using iTextSharp.text;
using System.Collections.Generic;
using System.Linq;
using TradeUnionCommittee.BLL.PDF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.TestPDF.ReportTemplates
{
    internal class TestGiftTemplate : BaseTemplate, IBaseReportTemplate<GiftEmployees>
    {
        public decimal CreateBody(Document doc, IEnumerable<GiftEmployees> model)
        {

            var table = AddPdfPTable(5);

            //---------------------------------------------------------------

            AddEmptyParagraph(doc, 3);
            table.WidthPercentage = 100;

            //---------------------------------------------------------------

            AddCell(table, FontBold, 1, "Назва заходу");
            AddCell(table, FontBold, 1, "Назва подарунку");
            AddCell(table, FontBold, 1, "Ціна");
            AddCell(table, FontBold, 1, "Розмір знижки");
            AddCell(table, FontBold, 1, "Дата отримання");

            foreach (var gift in model)
            {
                AddCell(table, Font, 1, $"{gift.NameEvent}");
                AddCell(table, Font, 1, $"{gift.NameGift}");
                AddCell(table, Font, 1, $"{gift.Price} {Сurrency}");
                AddCell(table, Font, 1, $"{gift.Discount} {Сurrency}");
                AddCell(table, Font, 2, $"{gift.DateGift:dd/MM/yyyy}");
            }

            doc.Add(table);

            //---------------------------------------------------------------

            var sumAmount = model.Sum(x => x.Price);
            var sumDiscount = model.Sum(x => x.Discount);
            var generalSum = sumAmount + sumDiscount;


            AddSubsidiesSum(doc, sumAmount, true);
            AddDiscountSum(doc, sumDiscount);
            AddGeneralSum(doc, generalSum);

            return generalSum;
        }
    }
}
