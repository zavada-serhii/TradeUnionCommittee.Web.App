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
        private readonly TradeUnionCommitteeContext _dbContext;

        public SearchRepository(TradeUnionCommitteeContext db)
        {
            _dbContext = db;
        }

        public async Task<IEnumerable<ResultFullNameSearch>> SearchByFullName(string fullName, TrigramSearch type)
        {
            string sqlQuery;

            switch (type)
            {
                case TrigramSearch.Gist:
                    sqlQuery = @"SELECT e.""Id"", public.""TrigramFullName""(e) <-> @fullName AS ""Gist"",
                                        e.""FirstName"" || ' ' || e.""SecondName"" || ' ' || e.""Patronymic"" AS ""FullName"",
	                                    e.""FirstName"" || ' ' || LEFT(e.""SecondName"",1) || '.' || CASE WHEN e.""Patronymic"" IS NULL THEN '' ELSE LEFT(e.""Patronymic"",1) || '.' END AS ""SurnameAndInitials"",
	                                    e.""BirthDate"", e.""MobilePhone"", e.""CityPhone"",
                                        COALESCE(ss.""Name"",s.""Name"") AS ""MainSubdivision"",
                                        COALESCE(ss.""Abbreviation"",s.""Abbreviation"") AS ""MainSubdivisionAbbreviation"",
                                        CASE WHEN ss.""Name"" IS NULL THEN NULL ELSE s.""Name"" END AS ""SubordinateSubdivision"",
                                        CASE WHEN ss.""Abbreviation"" IS NULL THEN NULL ELSE s.""Abbreviation"" END AS ""SubordinateSubdivisionAbbreviation""
                                 FROM ""Employee"" AS e 
                                 INNER JOIN ""PositionEmployees"" AS pe ON e.""Id"" = pe.""IdEmployee""
                                 INNER JOIN ""Subdivisions"" AS s ON pe.""IdSubdivision"" = s.""Id""
                                 LEFT JOIN ""Subdivisions"" AS ss ON ss.""Id"" = s.""IdSubordinate""
                                 ORDER BY ""Gist"" ASC LIMIT 10;";
                    break;
                case TrigramSearch.Gin:
                    sqlQuery = @"SELECT e.""Id"", SIMILARITY(public.""TrigramFullName""(e), @fullName ) AS ""Gin"",
                                        e.""FirstName"" || ' ' || e.""SecondName"" || ' ' || e.""Patronymic"" AS ""FullName"",
	                                    e.""FirstName"" || ' ' || LEFT(e.""SecondName"",1) || '.' || CASE WHEN e.""Patronymic"" IS NULL THEN '' ELSE LEFT(e.""Patronymic"",1) || '.' END AS ""SurnameAndInitials"",
	                                    e.""BirthDate"", e.""MobilePhone"", e.""CityPhone"",
                                        COALESCE(ss.""Name"",s.""Name"") AS ""MainSubdivision"",
                                        COALESCE(ss.""Abbreviation"",s.""Abbreviation"") AS ""MainSubdivisionAbbreviation"",
                                        CASE WHEN ss.""Name"" IS NULL THEN NULL ELSE s.""Name"" END AS ""SubordinateSubdivision"",
                                        CASE WHEN ss.""Abbreviation"" IS NULL THEN NULL ELSE s.""Abbreviation"" END AS ""SubordinateSubdivisionAbbreviation""
                                 FROM ""Employee"" AS e 
                                 INNER JOIN ""PositionEmployees"" AS pe ON e.""Id"" = pe.""IdEmployee""
                                 INNER JOIN ""Subdivisions"" AS s ON pe.""IdSubdivision"" = s.""Id""
                                 LEFT JOIN ""Subdivisions"" AS ss ON ss.""Id"" = s.""IdSubordinate""
                                 WHERE TRUE AND public.""TrigramFullName""(e) % @fullName
                                 ORDER BY ""Gin"" DESC LIMIT 10;";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            var result = new List<ResultFullNameSearch>();
            var parameter = new NpgsqlParameter("@fullName", fullName);
            using (var dr = await _dbContext.Database.ExecuteSqlQueryAsync(sqlQuery, default(CancellationToken), parameter))
            {
                var reader = dr.DbDataReader;
                result.AddRange(from DbDataRecord dbDataRecord in reader select new ResultFullNameSearch
                {
                    Id = (long)dbDataRecord["Id"],
                    FullName = dbDataRecord["FullName"].ToString(),
                    SurnameAndInitials = dbDataRecord["SurnameAndInitials"].ToString(),
                    BirthDate = (DateTime)dbDataRecord["BirthDate"],
                    MobilePhone = dbDataRecord["MobilePhone"].ToString(),
                    CityPhone = dbDataRecord["CityPhone"].ToString(),
                    MainSubdivision = dbDataRecord["MainSubdivision"].ToString(),
                    MainSubdivisionAbbreviation = dbDataRecord["MainSubdivisionAbbreviation"].ToString(),
                    SubordinateSubdivision = dbDataRecord["SubordinateSubdivision"].ToString(),
                    SubordinateSubdivisionAbbreviation = dbDataRecord["SubordinateSubdivisionAbbreviation"].ToString(),
                });
            }
            return result;
        }
    }

    public class ResultFullNameSearch
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string SurnameAndInitials { get; set; }
        public DateTime BirthDate { get; set; }
        public string MobilePhone { get; set; }
        public string CityPhone { get; set; }
        public string MainSubdivision { get; set; }
        public string MainSubdivisionAbbreviation { get; set; }
        public string SubordinateSubdivision { get; set; }
        public string SubordinateSubdivisionAbbreviation { get; set; }
    }
}