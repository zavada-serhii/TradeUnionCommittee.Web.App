using TradeUnionCommittee.BLL.DTO;

namespace TradeUnionCommittee.BLL.Interfaces.Lists
{
    public interface IFamilyService : IList<FamilyDTO>, IService<FamilyDTO>
    {
    }

    public interface IChildrenService : IList<ChildrenDTO>, IService<ChildrenDTO>
    {
    }

    public interface IGrandChildrenService : IList<GrandChildrenDTO>, IService<GrandChildrenDTO>
    {
    }
}