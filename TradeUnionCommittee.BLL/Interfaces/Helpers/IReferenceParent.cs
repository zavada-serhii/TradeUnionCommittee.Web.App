using System;
using System.Threading.Tasks;

namespace TradeUnionCommittee.BLL.Interfaces.Helpers
{
    public interface IReferenceParent : IDisposable
    {
        Task<string> GetHashIdEmployeeByFamily(string hashIdFamily);
        Task<string> GetHashIdEmployeeByChildren(string hashIdChildren);
        Task<string> GetHashIdEmployeeByGrandChildren(string hashIdGrandChildren);
    }
}