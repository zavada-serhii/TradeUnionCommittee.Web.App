using Microsoft.EntityFrameworkCore;
using TradeUnionCommittee.DAL.Audit.Extensions;

namespace TradeUnionCommittee.DAL.Audit.EF
{
    internal sealed class TradeUnionCommitteeAuditContext : DbContext
    {
        public TradeUnionCommitteeAuditContext(DbContextOptions<TradeUnionCommitteeAuditContext> options) : base(options)
        {
            if (Database.EnsureCreated())
            {
                InitializeAuditTable();
            }
        }

        private void InitializeAuditTable()
        {
            const string sql = "CREATE TYPE \"Operations\" AS ENUM(\'Select\', \'Insert\', \'Update\', \'Delete\'); ALTER TYPE \"Operations\" OWNER TO postgres; CREATE TYPE \"Tables\" AS ENUM ( \'Employee\', \'Children\', \'GrandChildren\', \'Family\', \'Award\', \'MaterialAid\', \'Hobby\', \'Event\', \'Cultural\', \'Activities\', \'Privileges\', \'SocialActivity\', \'Position\', \'Subdivisions\', \'AddressPublicHouse\', \'AwardEmployees\', \'MaterialAidEmployees\', \'HobbyEmployees\', \'FluorographyEmployees\', \'EventEmployees\', \'CulturalEmployees\', \'ActivityEmployees\', \'GiftEmployees\', \'PrivilegeEmployees\', \'SocialActivityEmployees\', \'PositionEmployees\', \'PublicHouseEmployees\', \'PrivateHouseEmployees\', \'ApartmentAccountingEmployees\', \'EventChildrens\', \'CulturalChildrens\', \'HobbyChildrens\', \'ActivityChildrens\', \'GiftChildrens\', \'EventGrandChildrens\', \'CulturalGrandChildrens\', \'HobbyGrandChildrens\', \'ActivityGrandChildrens\', \'GiftGrandChildrens\', \'EventFamily\', \'CulturalFamily\', \'ActivityFamily\'); ALTER TYPE \"Tables\" OWNER TO postgres; CREATE TABLE \"Journal\" ( \"Guid\" VARCHAR(36) NOT NULL PRIMARY KEY, \"Operation\" \"Operations\" NOT NULL, \"IpUser\" CIDR NOT NULL, \"DateTime\" TimeStamp NOT NULL, \"EmailUser\" VARCHAR(256) NOT NULL, \"Table\" \"Tables\" NOT NULL ); ALTER TABLE \"Journal\" OWNER TO postgres; CREATE OR REPLACE FUNCTION journal_insert_trigger() RETURNS trigger AS $BODY$ DECLARE table_master varchar(255) := \'journal\'; table_part varchar(255) := \'\'; BEGIN table_part := table_master || \'_y\' || date_part( \'year\', NEW.\"DateTime\" )::text || \'_m\' || date_part( \'month\', NEW.\"DateTime\" )::text; PERFORM 1 FROM pg_class WHERE relname = table_part LIMIT 1; IF NOT FOUND THEN EXECUTE \'CREATE TABLE \' || table_part || \'() INHERITS ( \' || \'\"\' || \'Journal\' || \'\"\' || \' ) WITH ( OIDS=FALSE ); ALTER TABLE \' || table_part || \' OWNER TO postgres;\'; EXECUTE \'CREATE INDEX \' || table_part || \'_journal_date_index ON \' || table_part || \' USING btree (\"Guid\", \"DateTime\", \"EmailUser\")\'; END IF; EXECUTE \'INSERT INTO \' || table_part || \' SELECT ( (\' || quote_literal(NEW) || \')::\' || \'\"Journal\"\' || \' ).*\'; RETURN NULL; END; $BODY$ LANGUAGE plpgsql VOLATILE COST 100; ALTER FUNCTION journal_insert_trigger() OWNER TO postgres; CREATE TRIGGER journal_insert_trigger BEFORE INSERT ON \"Journal\" FOR EACH ROW EXECUTE PROCEDURE journal_insert_trigger();";
            using (var dr = Database.ExecuteSqlQuery(sql))
            {
                dr.DbDataReader.Close();
            }
        }
    }
}