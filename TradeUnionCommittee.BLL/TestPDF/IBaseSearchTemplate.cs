using iTextSharp.text;
using System.Collections.Generic;

namespace TradeUnionCommittee.BLL.TestPDF
{
    public interface IBaseSearchTemplate<in T> where T : class, new()
    {
        void CreateBody(Document doc, IEnumerable<T> model);
    }
}