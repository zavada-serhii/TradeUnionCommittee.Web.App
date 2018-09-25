using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TradeUnionCommittee.DAL.EF;
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

        public async Task<IEnumerable<long>> SearchByFullName(string fullName)
        {
            var result = new List<long>();
            const string sqlQuery = "SELECT e.\"Id\", public.\"TrigramFullName\"(e) <-> @fullName AS \"ResultIds\" FROM public.\"Employee\" AS e ORDER BY \"ResultIds\" ASC LIMIT 10;";
            var parameter = new NpgsqlParameter("@fullName", fullName);
            using (var dr = await _dbContext.Database.ExecuteSqlQueryAsync(sqlQuery, default(CancellationToken), parameter))
            {
                var reader = dr.DbDataReader;
                result.AddRange(from DbDataRecord dbDataRecord in reader select (long) dbDataRecord["Id"]);
            }
            return result;
        }
    }

    public static class DatabaseFacadeExtensions
    {
        public static RelationalDataReader ExecuteSqlQuery(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            var concurrencyDetector = databaseFacade.GetService<IConcurrencyDetector>();
            using (concurrencyDetector.EnterCriticalSection())
            {
                var rawSqlCommand = databaseFacade.GetService<IRawSqlCommandBuilder>().Build(sql, parameters);
                return rawSqlCommand.RelationalCommand.ExecuteReader(databaseFacade.GetService<IRelationalConnection>(), rawSqlCommand.ParameterValues);
            }
        }

        public static async Task<RelationalDataReader> ExecuteSqlQueryAsync(this DatabaseFacade databaseFacade, string sql,
            CancellationToken cancellationToken = default(CancellationToken), params object[] parameters)
        {
            var concurrencyDetector = databaseFacade.GetService<IConcurrencyDetector>();
            using (concurrencyDetector.EnterCriticalSection())
            {
                var rawSqlCommand = databaseFacade.GetService<IRawSqlCommandBuilder>().Build(sql, parameters);
                return await rawSqlCommand.RelationalCommand.ExecuteReaderAsync(databaseFacade.GetService<IRelationalConnection>(), rawSqlCommand.ParameterValues, cancellationToken);
            }
        }
    }
}