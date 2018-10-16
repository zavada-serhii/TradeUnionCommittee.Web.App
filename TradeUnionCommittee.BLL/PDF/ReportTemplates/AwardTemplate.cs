using System.Linq;
using TradeUnionCommittee.BLL.PDF.Models;

namespace TradeUnionCommittee.BLL.PDF.ReportTemplates
{
    internal class AwardTemplate : BaseTemplate, IReportTemplate
    {
        public void CreateTemplateReport(ReportModel model)
        {
            var fullName = model.AwardEmployees.First().IdEmployeeNavigation;

            //---------------------------------------------------------------

            var doc = CreateDocument(model.PathToSave);
            var table = AddPdfPTable(6);

            //---------------------------------------------------------------

            AddNameReport(doc, "Звіт по матеріальним заохоченням члена профспілки");
            AddFullNameEmployee(doc, $"{fullName.FirstName} {fullName.SecondName} {fullName.Patronymic}");
            AddPeriod(doc, model.StartDate, model.EndDate);
            AddEmptyParagraph(doc, 3);
            table.WidthPercentage = 100;

            //---------------------------------------------------------------

            AddCell(table, FontBold, 2, "Джерело");
            AddCell(table, FontBold, 2, "Розмір");
            AddCell(table, FontBold, 2, "Дата отримання");

            foreach (var award in model.AwardEmployees)
            {
                AddCell(table, Font, 2, $"{award.IdAwardNavigation.Name}");
                AddCell(table, Font, 2, $"{award.Amount} {Сurrency}");
                AddCell(table, Font, 2, $"{award.DateIssue:dd/MM/yyyy}");
            }

            doc.Add(table);

            //---------------------------------------------------------------
            
            AddSumFrom(doc, model.AwardEmployees.GroupBy(l => l.IdAwardNavigation.Name).Select(cl => new { cl.First().IdAwardNavigation.Name, Sum = cl.Sum(c => c.Amount) }).ToList());
            AddGeneralSum(doc, model.AwardEmployees.Sum(x => x.Amount));
            AddSignature(doc);

            //---------------------------------------------------------------

            SaveFile(doc);
        }
    }
}