using System.Collections.Generic;
using iTextSharp.text;

namespace TradeUnionCommittee.BLL.TestPDF
{
    public interface IBaseReportTemplate<in T> where T : class, new()
    {
        decimal CreateBody(Document doc, IEnumerable<T> model);
    }
}
