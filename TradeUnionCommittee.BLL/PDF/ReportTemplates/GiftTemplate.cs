using System.Linq;
using TradeUnionCommittee.BLL.PDF.Models;

namespace TradeUnionCommittee.BLL.PDF.ReportTemplates
{
    internal class GiftTemplate : BaseTemplate, IReportTemplate
    {
        public void CreateTemplateReport(ReportModel model)
        {
            var fullName = model.GiftEmployees.First().IdEmployeeNavigation;

            //---------------------------------------------------------------

            var doc = CreateDocument(model.PathToSave);
            var table = AddPdfPTable(5);

            //---------------------------------------------------------------
            
            AddNameReport(doc, "Звіт по подарункам члена профспілки");
            AddFullNameEmployee(doc, $"{fullName.FirstName} {fullName.SecondName} {fullName.Patronymic}");
            AddPeriod(doc, model.StartDate, model.EndDate);
            AddEmptyParagraph(doc, 3);
            table.WidthPercentage = 100;

            //---------------------------------------------------------------

            AddCell(table, FontBold, 1, "Назва заходу");
            AddCell(table, FontBold, 1, "Назва подарунку");
            AddCell(table, FontBold, 1, "Ціна");
            AddCell(table, FontBold, 1, "Розмір знижки");
            AddCell(table, FontBold, 1, "Дата отримання");

            foreach (var gift in model.GiftEmployees)
            {
                AddCell(table, Font, 1, $"{gift.NameEvent}");
                AddCell(table, Font, 1, $"{gift.NameGift}");
                AddCell(table, Font, 1, $"{gift.Price} {Сurrency}");
                AddCell(table, Font, 1, $"{gift.Discount} {Сurrency}");
                AddCell(table, Font, 2, $"{gift.DateGift:dd/MM/yyyy}");
            }

            doc.Add(table);

            //---------------------------------------------------------------

            var sumAmount = model.GiftEmployees.Sum(x => x.Price);
            var sumDiscount = model.GiftEmployees.Sum(x => x.Discount);

            AddSubsidiesSum(doc, sumAmount, true);
            AddDiscountSum(doc, sumDiscount);
            AddGeneralSum(doc, sumAmount + sumDiscount);
            AddSignature(doc);

            //---------------------------------------------------------------

            SaveFile(doc);
        }
    }
}