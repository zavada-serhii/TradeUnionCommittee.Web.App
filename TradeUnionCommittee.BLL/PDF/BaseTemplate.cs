using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace TradeUnionCommittee.BLL.PDF
{
    internal abstract class BaseTemplate
    {
        public abstract void CreateTemplateReport(ReportModel model);

        //------------------------------------------------------------------------------------------------------------------------------------------

        protected readonly BaseFont BaseFont;
        protected readonly Font Font;
        protected readonly Font FontBold;
        protected string Сurrency = "грн";
        private PdfWriter _writer;

        protected BaseTemplate()
        {
            BaseFont = BaseFont.CreateFont(PathToFonts.PathToUkrainianFont, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            Font = new Font(BaseFont, 14, Font.NORMAL);
            FontBold = new Font(BaseFont, 12, Font.BOLD);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        protected Document CreateDocument(string pathToSave)
        {
            var document = new Document();
            _writer = PdfWriter.GetInstance(document, new FileStream($"{pathToSave}{Guid.NewGuid()}.pdf", FileMode.Create));
            document.Open();
            return document;
        }

        protected PdfPTable AddPdfPTable(int column)
        {
            return new PdfPTable(column);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        protected void AddFullNameEmployee(Document document, string fullNameEmployee)
        {
            document.Add(new Paragraph($"{fullNameEmployee}", FontBold) { Alignment = Element.ALIGN_CENTER });
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        protected void AddPeriod(Document document, DateTime sDate, DateTime eDate)
        {
            document.Add(new Paragraph($"за період з {sDate:dd/MM/yyyy}р по {eDate:dd/MM/yyyy}р", FontBold) { Alignment = Element.ALIGN_CENTER });
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        protected void AddEmptyParagraph(Document document, int count)
        {
            for (var i = 0; i < count; i++)
            {
                document.Add(new Paragraph(" ") { Alignment = Element.ALIGN_CENTER });
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------
        
        protected void AddSumFrom<T>(Document document, IEnumerable<T> list)
        {
            foreach (var item in list)
            {
                var typedItem = Cast(item, new { Name = string.Empty, Sum = new decimal() });
                document.Add(new Paragraph($"Cумма від {typedItem.Name} - {typedItem.Sum} {Сurrency}", Font) { Alignment = Element.ALIGN_RIGHT });
            }
        }

        private T Cast<T>(object obj, T type)
        {
            return (T)obj;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        protected void AddGeneralSum(Document document, decimal sum)
        {
            document.Add(new Paragraph($"Загальна сумма - {sum} грн", Font) { Alignment = Element.ALIGN_RIGHT });
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        protected void AddSignature(Document document)
        {
            AddEmptyParagraph(document, 2);
            document.Add(new Paragraph($"Головний бухгалтер ППО ОНУ імені І.І.Мечникова {new string('_', 10)}  {new string('_', 18)}", Font) { Alignment = Element.ALIGN_RIGHT });
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        protected void SaveFile(Document document)
        {
            document.Close();
            _writer.Close();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------
    }
}