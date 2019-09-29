using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUnionCommittee.DAL.Audit.EF;
using TradeUnionCommittee.DAL.Audit.Entities;
using TradeUnionCommittee.DAL.Audit.Enums;
using TradeUnionCommittee.DAL.Audit.Extensions;

namespace TradeUnionCommittee.DAL.Audit.Repository
{
    public interface ISystemAuditRepository : IDisposable
    {
        Task AuditAsync(Journal journal);
        Task<IEnumerable<Journal>> FilterAsync(string email, DateTime startDate, DateTime endDate);
    }

    internal class SystemAuditRepository : ISystemAuditRepository
    {
        private readonly TradeUnionCommitteeAuditContext _dbContext;

        public SystemAuditRepository(TradeUnionCommitteeAuditContext db)
        {
            _dbContext = db;
        }

        public async Task AuditAsync(Journal journal)
        {
            const string sqlQuery = "INSERT INTO \"Journal\" (\"Guid\",\"Operation\",\"DateTime\",\"EmailUser\",\"Table\",\"IpUser\") VALUES (@1, CAST(@2 AS \"Operations\"), @3, @4, CAST(@5 AS \"Tables\"), CAST(@6 AS CIDR));";

            var guid = new NpgsqlParameter("@1", Guid.NewGuid().ToString());
            var operation = new NpgsqlParameter("@2", journal.Operation.ToString());
            var dateTime = new NpgsqlParameter("@3", DateTime.Now);
            var email = new NpgsqlParameter("@4", journal.EmailUser);
            var table = new NpgsqlParameter("@5", journal.Table.ToString());
            var ip = new NpgsqlParameter("@6", journal.IpUser);

            await using var dr = _dbContext.Database.ExecuteSqlQuery(sqlQuery, guid, operation, dateTime, email, table, ip);
            dr.Close();
        }

        public async Task<IEnumerable<Journal>> FilterAsync(string email, DateTime startDate, DateTime endDate)
        {
            var result = new List<Journal>();
            var existingPartitionInDb = await GetExistingPartitionInDbAsync();
            var sequenceDate = startDate.Date.GetListPartitionings(endDate.Date);
            var namesPartitions = sequenceDate.Intersect(existingPartitionInDb).ToList();

            if (namesPartitions.Any())
            {
                var par1 = new NpgsqlParameter("@1", email);
                var par2 = new NpgsqlParameter("@2", startDate);
                var par3 = new NpgsqlParameter("@3", endDate);

                using (var dr = _dbContext.Database.ExecuteSqlQuery(SqlGenerator(namesPartitions), par1, par2, par3))
                {
                    if (dr.HasRows)
                    {
                        result.AddRange(from DbDataRecord dbDataRecord in dr select new Journal
                        {
                            Guid = dbDataRecord["Guid"].ToString(),
                            Operation = (Operations)Enum.Parse(typeof(Operations), dbDataRecord["Operation"].ToString()),
                            DateTime = (DateTime)dbDataRecord["DateTime"],
                            EmailUser = dbDataRecord["EmailUser"].ToString(),
                            Table = (Tables)Enum.Parse(typeof(Tables), dbDataRecord["Table"].ToString())
                        });
                    }
                    dr.Close();
                }
            }
            return result;
        }

        private async Task<IEnumerable<string>> GetExistingPartitionInDbAsync()
        {
            const string sqlQuery = "SELECT tablename FROM pg_tables WHERE schemaname = \'public\' AND tablename != \'Journal\';";
            var result = new List<string>();
            await using (var dr = _dbContext.Database.ExecuteSqlQuery(sqlQuery))
            {
                result.AddRange(from DbDataRecord dbDataRecord in dr select dbDataRecord["tablename"].ToString());
                dr.Close();
            }
            return result;
        }

        private string SqlGenerator(IReadOnlyList<string> list)
        {
            if (list.Count > 1)
            {
                var builder = new StringBuilder();
                for (var i = 0; i < list.Count; i++)
                {
                    if (i != list.Count - 1)
                    {
                        builder.Append("SELECT \"Guid\", CAST(\"Operation\" AS VARCHAR),\"DateTime\",\"EmailUser\",CAST(\"Table\" AS VARCHAR) FROM ");
                        builder.Append(list[i]);
                        builder.Append(" WHERE \"EmailUser\" = @1 AND \"DateTime\" BETWEEN @2 AND @3 UNION ALL ");
                    }
                    else
                    {
                        builder.Append("SELECT \"Guid\", CAST(\"Operation\" AS VARCHAR),\"DateTime\",\"EmailUser\",CAST(\"Table\" AS VARCHAR) FROM ");
                        builder.Append(list[i]);
                        builder.Append(" WHERE \"EmailUser\" = @1 AND \"DateTime\" BETWEEN @2 AND @3");
                    }
                }
                return builder.ToString();
            }
            return $"SELECT \"Guid\", CAST(\"Operation\" AS VARCHAR),\"DateTime\",\"EmailUser\",CAST(\"Table\" AS VARCHAR) FROM {list[0]} WHERE \"EmailUser\" = @1 AND \"DateTime\" BETWEEN @2 AND @3";
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}