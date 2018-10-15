using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace TradeUnionCommittee.BLL.PDF.ReportTemplates
{
    internal class MaterialAidTemplate : BaseTemplate
    {
        public override void CreateTemplateReport(ReportModel model)
        {
            var fullName = model.MaterialAidEmployees.First().IdEmployeeNavigation;

            //---------------------------------------------------------------

            var doc = CreateDocument(model.PathToSave);
            var table = AddPdfPTable(6);

            //---------------------------------------------------------------

            doc.Add(new Paragraph("Звіт по матеріальним допомогам члена профспілки", FontBold) { Alignment = Element.ALIGN_CENTER });
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

            foreach (var materialInterestse in model.MaterialAidEmployees)
            {
                table.AddCell(new PdfPCell(new Phrase($"{materialInterestse.IdMaterialAidNavigation.Name}", Font))
                {
                    PaddingTop = 5,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Colspan = 2
                });

                table.AddCell(new PdfPCell(new Phrase($"{materialInterestse.Amount} {Сurrency}", Font))
                {
                    PaddingTop = 5,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Colspan = 2
                });

                table.AddCell(new PdfPCell(new Phrase($"{materialInterestse.DateIssue:dd/MM/yyyy}", Font))
                {
                    PaddingTop = 5,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Colspan = 2
                });
            }

            //---------------------------------------------------------------

            doc.Add(table);

            //---------------------------------------------------------------

            AddSumFrom(doc, model.MaterialAidEmployees.GroupBy(l => l.IdMaterialAidNavigation.Name).Select(cl => new { cl.First().IdMaterialAidNavigation.Name, Sum = cl.Sum(c => c.Amount) }).ToList());
            
            //---------------------------------------------------------------

            AddGeneralSum(doc, model.MaterialAidEmployees.Sum(x => x.Amount));
            AddSignature(doc);

            //---------------------------------------------------------------

            SaveFile(doc);
        }
    }
}