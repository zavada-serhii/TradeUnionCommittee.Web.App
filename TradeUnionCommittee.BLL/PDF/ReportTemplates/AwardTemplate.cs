using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace TradeUnionCommittee.BLL.PDF.ReportTemplates
{
    internal class AwardTemplate : BaseTemplate
    {
        public override void CreateTemplateReport(ReportModel model)
        {
            var fullName = model.AwardEmployees.First().IdEmployeeNavigation;

            //---------------------------------------------------------------

            var doc = CreateDocument(model.PathToSave);
            var table = AddPdfPTable(6);

            //---------------------------------------------------------------

            doc.Add(new Paragraph("Звіт по матеріальним заохоченням члена профспілки", FontBold) { Alignment = Element.ALIGN_CENTER });
            AddFullNameEmployee(doc, $"{fullName.FirstName} {fullName.SecondName} {fullName.Patronymic}");
            AddPeriod(doc, model.StartDate, model.EndDate);
            AddEmptyParagraph(doc, 3);
            table.WidthPercentage = 100;

            //---------------------------------------------------------------

            table.AddCell(new PdfPCell(new Phrase("Джерело", FontBold))
            {
                PaddingTop = 5,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Colspan = 2
            });

            table.AddCell(new PdfPCell(new Phrase("Розмір", FontBold))
            {
                PaddingTop = 5,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Colspan = 2
            });

            table.AddCell(new PdfPCell(new Phrase("Дата отримання", FontBold))
            {
                PaddingTop = 5,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Colspan = 2
            });

            //---------------------------------------------------------------

            foreach (var award in model.AwardEmployees)
            {
                table.AddCell(new PdfPCell(new Phrase($"{award.IdAwardNavigation.Name}", Font))
                {
                    PaddingTop = 5,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Colspan = 2
                });

                table.AddCell(new PdfPCell(new Phrase($"{award.Amount} {Сurrency}", Font))
                {
                    PaddingTop = 5,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Colspan = 2
                });

                table.AddCell(new PdfPCell(new Phrase($"{award.DateIssue:dd/MM/yyyy}", Font))
                {
                    PaddingTop = 5,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Colspan = 2
                });
            }

            //---------------------------------------------------------------

            doc.Add(table);

            //---------------------------------------------------------------
            
            AddSumFrom(doc, model.AwardEmployees.GroupBy(l => l.IdAwardNavigation.Name).Select(cl => new { cl.First().IdAwardNavigation.Name, Sum = cl.Sum(c => c.Amount) }).ToList());

            //---------------------------------------------------------------

            AddGeneralSum(doc, model.AwardEmployees.Sum(x => x.Amount));
            AddSignature(doc);

            //---------------------------------------------------------------

            SaveFile(doc);
        }
    }
}