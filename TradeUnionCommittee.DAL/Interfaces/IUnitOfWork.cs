using System;
using System.Threading.Tasks;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Children> ChildrenRepository { get; }
        IRepository<GrandChildren> GrandChildrenRepository { get; }

        //------------------------------------------------------------------------------------------------------------------------------------------
        
        IRepository<EventChildrens> EventChildrensRepository { get; }
        IRepository<CulturalChildrens> CulturalChildrensRepository { get; }
        IRepository<HobbyChildrens> HobbyChildrensRepository { get; }
        IRepository<ActivityChildrens> ActivityChildrensRepository { get; }
        IRepository<GiftChildrens> GiftChildrensRepository { get; }

        IRepository<EventGrandChildrens> EventGrandChildrensRepository { get; }
        IRepository<CulturalGrandChildrens> CulturalGrandChildrensRepository { get; }
        IRepository<HobbyGrandChildrens> HobbyGrandChildrensRepository { get; }
        IRepository<ActivityGrandChildrens> ActivityGrandChildrensRepository { get; }
        IRepository<GiftGrandChildrens> GiftGrandChildrensRepository { get; }

        //------------------------------------------------------------------------------------------------------------------------------------------

        Task<ActualResult> SaveAsync();
    }
}