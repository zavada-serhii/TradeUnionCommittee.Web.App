using System;
using System.Linq;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.BLL.Interfaces.Helpers;
using TradeUnionCommittee.DAL.EF;

namespace TradeUnionCommittee.BLL.Services.Helpers
{
    internal class ReferenceParent : IReferenceParent
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly HashIdConfiguration _hashIdUtilities;

        public ReferenceParent(TradeUnionCommitteeContext context, HashIdConfiguration hashIdUtilities)
        {
            _context = context;
            _hashIdUtilities = hashIdUtilities;
        }

        public ActualResult<string> GetHashIdEmployee(string hashId, ReferenceParentType type)
        {
            try
            {
                long? result;
                var id = _hashIdUtilities.DecryptLong(hashId);
                switch (type)
                {
                    case ReferenceParentType.Family:
                        result = _context.Family.FirstOrDefault(x => x.Id == id)?.IdEmployee;
                        break;
                    case ReferenceParentType.Children:
                        result = _context.Children.FirstOrDefault(x => x.Id == id)?.IdEmployee;
                        break;
                    case ReferenceParentType.GrandChildren:
                        result = _context.GrandChildren.FirstOrDefault(x => x.Id == id)?.IdEmployee;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }

                if (result != null)
                {
                    var idEmployee = _hashIdUtilities.EncryptLong(result.Value);
                    return new ActualResult<string> { Result = idEmployee };
                }
                return new ActualResult<string>(Errors.EmployeeDeleted);
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}