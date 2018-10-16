using System.Linq;
using TradeUnionCommittee.BLL.PDF.Models;

namespace TradeUnionCommittee.BLL.PDF.ReportTemplates
{
    internal class CulturalTemplate : BaseTemplate, IReportTemplate
    {
        public void CreateTemplateReport(ReportModel model)
        {
            var fullName = model.CulturalEmployees.First().IdEmployeeNavigation;

            //---------------------------------------------------------------

            var doc = CreateDocument(model.PathToSave);
            var table = AddPdfPTable(4);

            //---------------------------------------------------------------

            AddNameReport(doc, "Звіт по культурно-просвітницьким закладам члена профспілки");
            AddFullNameEmployee(doc, $"{fullName.FirstName} {fullName.SecondName} {fullName.Patronymic}");
            AddPeriod(doc, model.StartDate, model.EndDate);
            AddEmptyParagraph(doc, 3);
            table.WidthPercentage = 100;

            //---------------------------------------------------------------

            AddCell(table, FontBold, 1, "Назва закладу");
            AddCell(table, FontBold, 1, "Розмір дотації");
            AddCell(table, FontBold, 1, "Розмір знижки");
            AddCell(table, FontBold, 1, "Дата візиту");

            foreach (var cultural in model.CulturalEmployees)
            {
                AddCell(table, Font, 1, $"{cultural.IdCulturalNavigation.Name}");
                AddCell(table, Font, 1, $"{cultural.Amount} {Сurrency}");
                AddCell(table, Font, 1, $"{cultural.Discount} {Сurrency}");
                AddCell(table, Font, 1, $"{cultural.DateVisit:dd/MM/yyyy}");
            }

            doc.Add(table);

            //---------------------------------------------------------------

            var sumAmount = model.CulturalEmployees.Sum(x => x.Amount);
            var sumDiscount = model.CulturalEmployees.Sum(x => x.Discount);

            AddSubsidiesSum(doc, sumAmount);
            AddDiscountSum(doc, sumDiscount);
            AddGeneralSum(doc, sumAmount + sumDiscount);
            AddSignature(doc);

            //---------------------------------------------------------------

            SaveFile(doc);
        }
    }
}