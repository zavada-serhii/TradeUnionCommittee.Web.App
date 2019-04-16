using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Net.Sockets;
using TradeUnionCommittee.BLL.Enums;

namespace TradeUnionCommittee.BLL.Helpers
{
    internal class DescriptionExceptionHelper
    {
        public static Errors GetDescriptionError(Exception exception)
        {
            if (exception.InnerException != null)
            {
                if (exception.InnerException is NpgsqlException npgsqlException && npgsqlException.InnerException is SocketException socketException)
                {
                    if (socketException.SocketErrorCode == SocketError.ConnectionReset)
                    {
                        return Errors.ConnectionLost;
                    }
                }

                if (exception.InnerException is PostgresException postgresException)
                {
                    switch (postgresException.SqlState)
                    {
                        case "23503":
                            return Errors.DataUsed;
                        case "23505":
                            return Errors.DuplicateData;
                        default:
                            return Errors.DataBaseError;
                    }
                }
            }

            if (exception is DbUpdateConcurrencyException)
            {
                return Errors.TupleDeletedOrUpdated;
            }

            if (exception is SocketException socket)
            {
                if (socket.SocketErrorCode == SocketError.ConnectionRefused)
                {
                    return Errors.ConnectionLost;
                }
            }

            return Errors.DataBaseError;
        }
    }
}