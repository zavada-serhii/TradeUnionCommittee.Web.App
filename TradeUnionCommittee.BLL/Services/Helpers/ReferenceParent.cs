using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.Interfaces.Helpers;
using TradeUnionCommittee.DAL.EF;

namespace TradeUnionCommittee.BLL.Services.Helpers
{
    public class ReferenceParent : IReferenceParent
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public ReferenceParent(TradeUnionCommitteeContext context, IHashIdConfiguration hashIdUtilities)
        {
            _context = context;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<string> GetHashIdEmployeeByFamily(string hashIdFamily)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdFamily);
            var result = await _context.Family.FindAsync(id);
            return result != null ? _hashIdUtilities.EncryptLong(result.IdEmployee) : null;
        }

        public async Task<string> GetHashIdEmployeeByChildren(string hashIdChildren)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdChildren);
            var result = await _context.Children.FindAsync(id);
            return result != null ? _hashIdUtilities.EncryptLong(result.IdEmployee) : null;
        }

        public async Task<string> GetHashIdEmployeeByGrandChildren(string hashIdGrandChildren)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdGrandChildren);
            var result = await _context.GrandChildren.FindAsync(id);
            return result != null ? _hashIdUtilities.EncryptLong(result.IdEmployee) : null;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}