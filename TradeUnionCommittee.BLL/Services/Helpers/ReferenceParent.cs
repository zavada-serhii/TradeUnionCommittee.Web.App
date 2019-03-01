using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.Interfaces.Helpers;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Helpers
{
    public class ReferenceParent : IReferenceParent
    {
        private readonly IUnitOfWork _database;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public ReferenceParent(IUnitOfWork database, IHashIdConfiguration hashIdUtilities)
        {
            _database = database;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<string> GetHashIdEmployeeByFamily(string hashIdFamily)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdFamily, Enums.Services.Family);
            var result = await _database.FamilyRepository.GetById(id);
            if (result.IsValid && result.Result != null)
            {
                return _hashIdUtilities.EncryptLong(result.Result.IdEmployee, Enums.Services.Employee);
            }
            return null;
        }

        public async Task<string> GetHashIdEmployeeByChildren(string hashIdChildren)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdChildren, Enums.Services.Children);
            var result = await _database.ChildrenRepository.GetById(id);
            if (result.IsValid && result.Result != null)
            {
                return _hashIdUtilities.EncryptLong(result.Result.IdEmployee, Enums.Services.Employee);
            }
            return null;
        }

        public async Task<string> GetHashIdEmployeeByGrandChildren(string hashIdGrandChildren)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdGrandChildren, Enums.Services.GrandChildren);
            var result = await _database.GrandChildrenRepository.GetById(id);
            if (result.IsValid && result.Result != null)
            {
                return _hashIdUtilities.EncryptLong(result.Result.IdEmployee, Enums.Services.Employee);
            }
            return null;
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}