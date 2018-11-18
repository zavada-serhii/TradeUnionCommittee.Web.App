using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Extensions;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.DAL.Repositories.SystemAudit
{
    public class SystemAuditRepository : ISystemAuditRepository
    {
        private readonly TradeUnionCommitteeEmployeesCoreContext _dbContext;

        public SystemAuditRepository(TradeUnionCommitteeEmployeesCoreContext db)
        {
            _dbContext = db;
        }

        public async Task AuditAsync(Journal journal)
        {
            const string sqlQuery = "INSERT INTO audit.\"Journal\" (\"Guid\",\"Operation\",\"DateTime\",\"EmailUser\",\"Table\") VALUES (@1, CAST(@2 AS audit.\"Operations\"), @3, @4, CAST(@5 AS audit.\"Tables\"));";

            var guid = new NpgsqlParameter("@1", Guid.NewGuid().ToString());
            var operation = new NpgsqlParameter("@2", journal.Operation.ToString());
            var dateTime = new NpgsqlParameter("@3", DateTime.Now.AddMonths(2));
            var email = new NpgsqlParameter("@4", journal.EmailUser);
            var table = new NpgsqlParameter("@5", journal.Table.ToString());

            using (var dr = await _dbContext.Database.ExecuteSqlQueryAsync(sqlQuery, default(CancellationToken), guid, operation, dateTime, email, table))
            {
                dr.DbDataReader.Close();
            }
        }

        public async Task<IEnumerable<string>> GetExistingPartitionInDbAsync()
        {
            const string sqlQuery = "SELECT tablename FROM pg_tables WHERE schemaname = \'audit\' AND tablename != \'Journal\';";

            var result = new List<string>();

            using (var dr = await _dbContext.Database.ExecuteSqlQueryAsync(sqlQuery))
            {
                var reader = dr.DbDataReader;
                result.AddRange(from DbDataRecord dbDataRecord in reader select (string)dbDataRecord["tablename"]);
                dr.DbDataReader.Close();
            }
            return result;
        }

        public async Task<IEnumerable<Journal>> FilterAsync(IEnumerable<string> namesPartitions, string email, DateTime startDate, DateTime endDate)
        {
            var par1 = new NpgsqlParameter("@1", email);
            var par2 = new NpgsqlParameter("@2", startDate);
            var par3 = new NpgsqlParameter("@3", endDate);

            var result = new List<Journal>();

            using (var dr = await _dbContext.Database.ExecuteSqlQueryAsync(SqlGenerator(namesPartitions.ToList()), default(CancellationToken), par1, par2, par3))
            {
                var reader = dr.DbDataReader;
                result.AddRange(from DbDataRecord dbDataRecord in reader select new Journal
                {
                   Guid = (string)dbDataRecord["Guid"],
                   Operation = (Operations)dbDataRecord["Operation"],
                   DateTime = (DateTime)dbDataRecord["DateTime"],
                   EmailUser = (string)dbDataRecord["EmailUser"],
                   Table = (Tables)dbDataRecord["Table"]
                });
                dr.DbDataReader.Close();
            }
            return result;
        }

        private string SqlGenerator(IReadOnlyList<string> list)
        {
            if (list.Count > 1)
            {
                var result = string.Empty;

                for (var i = 0; i < list.Count; i++)
                {
                    if (i != list.Count - 1)
                    {
                        result += $"SELECT * FROM audit.{list[i]} WHERE \"EmailUser\" = @1 AND \"DateTime\" BETWEEN @2 AND @3 UNION ALL ";
                    }
                    else
                    {
                        result += $"SELECT * FROM audit.{list[i]} WHERE \"EmailUser\" = @1 AND \"DateTime\" BETWEEN @2 AND @3";
                    }
                }
                return result;
            }
            return $"SELECT * FROM audit.{list[0]} WHERE \"EmailUser\" = @1 AND \"DateTime\" BETWEEN @2 AND @3";
        }
    }
}