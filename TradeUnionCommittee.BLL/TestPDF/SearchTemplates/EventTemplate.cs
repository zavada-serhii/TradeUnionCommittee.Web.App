using iTextSharp.text;
using System.Collections.Generic;
using TradeUnionCommittee.BLL.PDF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.TestPDF.SearchTemplates
{
    internal class EventTemplate : BaseTemplate, IBaseSearchTemplate<EventEmployees>
    {
        public void CreateBody(Document doc, IEnumerable<EventEmployees> model)
        {
            var table = AddPdfPTable(4);

            //---------------------------------------------------------------

            AddEmptyParagraph(doc, 3);
            table.WidthPercentage = 100;

            //---------------------------------------------------------------

            AddCell(table, FontBold, 1, "ПІБ");
            AddCell(table, FontBold, 1, "Структурний підрозділ");
            AddCell(table, FontBold, 1, "Кафедра або інше");
            AddCell(table, FontBold, 1, "Посада");

            foreach (var ev in model)
            {
                AddCell(table, Font, 1, $"{ev.IdEmployeeNavigation.FirstName} {ev.IdEmployeeNavigation.SecondName} {ev.IdEmployeeNavigation.Patronymic}");
                AddCell(table, Font, 1, $"{ev.IdEmployeeNavigation.PositionEmployees.IdSubdivisionNavigation.Name}");
                AddCell(table, Font, 1, $"{ev.IdEmployeeNavigation.PositionEmployees.IdSubdivisionNavigation.Name}");
                AddCell(table, Font, 1, $"{ev.IdEmployeeNavigation.PositionEmployees.IdPositionNavigation.Name}");
            }

            doc.Add(table);
        }
    }
}
