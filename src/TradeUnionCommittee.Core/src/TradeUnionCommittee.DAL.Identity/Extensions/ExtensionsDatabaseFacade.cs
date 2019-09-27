using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data;
using System.Data.Common;

namespace TradeUnionCommittee.DAL.Identity.Extensions
{
    public static class ExtensionsDatabaseFacade
    {
        public static DbDataReader ExecuteSqlQuery(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            using (var command = databaseFacade.GetDbConnection().CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = CommandType.Text;
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }

                databaseFacade.OpenConnection();

                return command.ExecuteReader();
            }
        }
    }
}