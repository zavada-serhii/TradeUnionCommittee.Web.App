using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Extensions;
using TradeUnionCommittee.DAL.Interfaces;
using TradeUnionCommittee.DAL.Repositories.General;
using TradeUnionCommittee.DAL.Repositories.Lists;

namespace TradeUnionCommittee.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TradeUnionCommitteeContext _context;

        public UnitOfWork(TradeUnionCommitteeContext context)
        {
            _context = context;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------
        
        private GrandChildrenRepository _grandChildrenRepository;

        //------------------------------------------------------------------------------------------------------------------------------------------

        private EventGrandChildrensRepository _eventGrandChildrensRepository;
        private CulturalGrandChildrensRepository _culturalGrandChildrensRepository;
        private HobbyGrandChildrensRepository _hobbyGrandChildrensRepository;
        private ActivityGrandChildrensRepository _activityGrandChildrensRepository;
        private GiftGrandChildrensRepository _giftGrandChildrensRepository;

        //------------------------------------------------------------------------------------------------------------------------------------------
        
        public IRepository<GrandChildren> GrandChildrenRepository => _grandChildrenRepository ?? (_grandChildrenRepository = new GrandChildrenRepository(_context));

        //------------------------------------------------------------------------------------------------------------------------------------------
        
        public IRepository<EventGrandChildrens> EventGrandChildrensRepository => _eventGrandChildrensRepository ?? (_eventGrandChildrensRepository = new EventGrandChildrensRepository(_context));
        public IRepository<CulturalGrandChildrens> CulturalGrandChildrensRepository => _culturalGrandChildrensRepository ?? (_culturalGrandChildrensRepository = new CulturalGrandChildrensRepository(_context));
        public IRepository<HobbyGrandChildrens> HobbyGrandChildrensRepository => _hobbyGrandChildrensRepository ?? (_hobbyGrandChildrensRepository = new HobbyGrandChildrensRepository(_context));
        public IRepository<ActivityGrandChildrens> ActivityGrandChildrensRepository => _activityGrandChildrensRepository ?? (_activityGrandChildrensRepository = new ActivityGrandChildrensRepository(_context));
        public IRepository<GiftGrandChildrens> GiftGrandChildrensRepository => _giftGrandChildrensRepository ?? (_giftGrandChildrensRepository = new GiftGrandChildrensRepository(_context));
       
        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult> SaveAsync()
        {
            try
            {
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entity = ex.Entries.Single().GetDatabaseValues();
                if (entity == null)
                {
                    _context.UndoChanges();
                    return new ActualResult(Errors.TupleDeleted);
                }
                var data = ex.Entries.Single();
                data.OriginalValues.SetValues(data.GetDatabaseValues());
                return new ActualResult(Errors.TupleUpdated);
            }
            catch (Exception e)
            {
                _context.UndoChanges();
                return new ActualResult(e.Message);
            }
            return new ActualResult();
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