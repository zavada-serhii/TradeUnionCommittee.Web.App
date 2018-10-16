using System.Linq;
using TradeUnionCommittee.BLL.PDF.Models;

namespace TradeUnionCommittee.BLL.PDF.ReportTemplates
{
    internal class MaterialAidTemplate : BaseTemplate, IReportTemplate
    {
        public void CreateTemplateReport(ReportModel model)
        {
            var fullName = model.MaterialAidEmployees.First().IdEmployeeNavigation;

            //---------------------------------------------------------------

            var doc = CreateDocument(model.PathToSave);
            var table = AddPdfPTable(6);

            //---------------------------------------------------------------

            AddNameReport(doc, "Звіт по матеріальним допомогам члена профспілки");
            AddFullNameEmployee(doc, $"{fullName.FirstName} {fullName.SecondName} {fullName.Patronymic}");
            AddPeriod(doc, model.StartDate, model.EndDate);
            AddEmptyParagraph(doc, 3);
            table.WidthPercentage = 100;

            //---------------------------------------------------------------

            AddCell(table, FontBold, 2, "Джерело");
            AddCell(table, FontBold, 2, "Розмір");
            AddCell(table, FontBold, 2, "Дата отримання");

            foreach (var materialInterestse in model.MaterialAidEmployees)
            {
                AddCell(table, Font, 2, $"{materialInterestse.IdMaterialAidNavigation.Name}");
                AddCell(table, Font, 2, $"{materialInterestse.Amount} {Сurrency}");
                AddCell(table, Font, 2, $"{materialInterestse.DateIssue:dd/MM/yyyy}");
            }

            doc.Add(table);

            //---------------------------------------------------------------

            AddSumFrom(doc, model.MaterialAidEmployees.GroupBy(l => l.IdMaterialAidNavigation.Name).Select(cl => new { cl.First().IdMaterialAidNavigation.Name, Sum = cl.Sum(c => c.Amount) }).ToList());
            AddGeneralSum(doc, model.MaterialAidEmployees.Sum(x => x.Amount));
            AddSignature(doc);

            //---------------------------------------------------------------

            SaveFile(doc);
        }
    }
}