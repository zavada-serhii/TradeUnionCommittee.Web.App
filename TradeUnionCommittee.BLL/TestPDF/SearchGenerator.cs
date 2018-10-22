using iTextSharp.text;
using System;
using System.Linq;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.TestPDF.Models;

namespace TradeUnionCommittee.BLL.TestPDF
{
    public class SearchGenerator
    {
        private readonly Font _font;
        private readonly Font _fontBold;
        private readonly string _currency;

        public SearchGenerator(Font font, Font fontBold, string currency)
        {
            _font = font;
            _fontBold = fontBold;
            _currency = currency;
        }

        public void Generate(SearchModel model, Document document)
        {
            AddNameReport(model, document);
            document.Add(new Paragraph($"за період з {model.StartDate:dd/MM/yyyy}р по {model.EndDate:dd/MM/yyyy}р", _fontBold) { Alignment = Element.ALIGN_CENTER });

            //----------------------------------------------------------------------------------------------

            // Add Body

            //----------------------------------------------------------------------------------------------

            AddEmptyParagraph(document, 2);
        }

        private void AddNameReport(SearchModel model, IElementListener doc)
        {
            switch (model.Type)
            {
                case SearchType.Travel:
                    doc.Add(new Paragraph($"Пошук по поїздкам в {model.EventEmployees.First().IdEventNavigation.Name}", _fontBold) { Alignment = Element.ALIGN_CENTER });
                    break;
                case SearchType.Wellness:
                    doc.Add(new Paragraph($"Пошук по оздоровленням в {model.EventEmployees.First().IdEventNavigation.Name}", _fontBold) { Alignment = Element.ALIGN_CENTER });
                    break;
                case SearchType.Tour:
                    doc.Add(new Paragraph($"Пошук по путівкам в {model.EventEmployees.First().IdEventNavigation.Name}", _fontBold) { Alignment = Element.ALIGN_CENTER });
                    break;
                case SearchType.Cultural:
                    doc.Add(new Paragraph($"Пошук по культурно-просвітницьким закладам в {model.EventEmployees.First().IdEventNavigation.Name}", _fontBold) { Alignment = Element.ALIGN_CENTER });
                    break;
                case SearchType.Gift:
                    doc.Add(new Paragraph($"Пошук по подарункам на {model.GiftEmployees.First().NameEvent}", _fontBold) { Alignment = Element.ALIGN_CENTER });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void AddBodySearch(SearchModel model, Document doc)
        {
            switch (model.Type)
            {
                case SearchType.Travel:
                    break;
                case SearchType.Wellness:
                    break;
                case SearchType.Tour:
                    break;
                case SearchType.Cultural:
                    break;
                case SearchType.Gift:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void AddEmptyParagraph(IElementListener document, int count)
        {
            for (var i = 0; i < count; i++)
            {
                document.Add(new Paragraph(" ") { Alignment = Element.ALIGN_CENTER });
            }
        }
    }
}