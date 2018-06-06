using System;
using TradeUnionCommittee.Common.Entities;

namespace TradeUnionCommittee.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Position> PositionRepository { get; }
    }
}
