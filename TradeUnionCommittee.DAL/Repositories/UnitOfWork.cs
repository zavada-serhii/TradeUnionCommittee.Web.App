using System;
using TradeUnionCommittee.Common.EF;
using TradeUnionCommittee.Common.Entities;
using TradeUnionCommittee.DAL.Interfaces;
using TradeUnionCommittee.DAL.Repositories.Directory;

namespace TradeUnionCommittee.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed;
        private readonly TradeUnionCommitteeEmployeesCoreContext _context;
        private PositionRepository _positionRepository;

        public UnitOfWork()
        {
            _context = new TradeUnionCommitteeEmployeesCoreContext();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public IRepository<Position> PositionRepository => _positionRepository ?? (_positionRepository = new PositionRepository(_context));

        //------------------------------------------------------------------------------------------------------------------------------------------

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}