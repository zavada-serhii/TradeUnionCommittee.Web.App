using System;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Position> PositionRepository { get; }
    }
}
