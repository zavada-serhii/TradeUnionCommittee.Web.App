using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Enums;
using TradeUnionCommittee.DAL.Extensions;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.DAL.Repositories.Search
{
    public class SearchRepository : ISearchRepository
    {
        private readonly TradeUnionCommitteeEmployeesCoreContext _dbContext;

        public SearchRepository(TradeUnionCommitteeEmployeesCoreContext db)
        {
            _dbContext = db;
        }

        public async Task<IEnumerable<long>> SearchByFullName(string fullName, AlgorithmSearchFullName algorithm)
        {
            string sqlQuery;

            switch (algorithm)
            {
                case AlgorithmSearchFullName.Gist:
                    sqlQuery = "SELECT e.\"Id\", public.\"TrigramFullName\"(e) <-> @fullName AS \"ResultIds\" FROM public.\"Employee\" AS e ORDER BY \"ResultIds\" ASC LIMIT 10;"; ;
                    break;
                case AlgorithmSearchFullName.Gin:
                    sqlQuery = "SELECT e.\"Id\", similarity(public.\"TrigramFullName\"(e), @fullName ) AS \"ResultIds\" FROM public.\"Employee\" AS e WHERE TRUE AND public.\"TrigramFullName\"(e) % @fullName ORDER BY \"ResultIds\" DESC LIMIT 10;";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(algorithm), algorithm, null);
            }

            var result = new List<long>();
            var parameter = new NpgsqlParameter("@fullName", fullName);
            using (var dr = await _dbContext.Database.ExecuteSqlQueryAsync(sqlQuery, default(CancellationToken), parameter))
            {
                var reader = dr.DbDataReader;
                result.AddRange(from DbDataRecord dbDataRecord in reader select (long) dbDataRecord["Id"]);
            }
            return result;
        }
    }
}