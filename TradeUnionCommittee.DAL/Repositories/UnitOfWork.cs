using System;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;
using TradeUnionCommittee.DAL.Repositories.Directory;

namespace TradeUnionCommittee.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TradeUnionCommitteeEmployeesCoreContext _context;
        private PositionRepository _positionRepository;

        public UnitOfWork(string connectionString)
        {
            _context = new TradeUnionCommitteeEmployeesCoreContext(connectionString);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public IRepository<Position> PositionRepository => _positionRepository ?? (_positionRepository = new PositionRepository(_context));

        //------------------------------------------------------------------------------------------------------------------------------------------

        public void Save()
        {
            _context.SaveChanges();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        private bool _disposed;

        private void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}