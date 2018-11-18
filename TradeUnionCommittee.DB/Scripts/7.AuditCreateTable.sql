
CREATE SCHEMA audit AUTHORIZATION postgres;

-----------------------------------------------------------------

CREATE TYPE audit."Operations" AS ENUM ('Select', 'Insert', 'Update', 'Delete');
ALTER TYPE audit."Operations" OWNER TO postgres;

CREATE TYPE audit."Tables" AS ENUM 
(
    'Employee',
    'Children',
    'GrandChildren',
    'Family',
    'Award',
    'MaterialAid',
    'Hobby',
    'Event',
    'Cultural',
    'Activities',
    'Privileges',
    'SocialActivity',
    'Position',
    'Subdivisions',
    'AddressPublicHouse',
    'AwardEmployees',
    'MaterialAidEmployees',
    'HobbyEmployees',
    'FluorographyEmployees',
    'EventEmployees',
    'CulturalEmployees',
    'ActivityEmployees',
    'GiftEmployees',
    'PrivilegeEmployees',
    'SocialActivityEmployees',
    'PositionEmployees',
    'PublicHouseEmployees',
    'PrivateHouseEmployees',
    'ApartmentAccountingEmployees',
    'EventChildrens',
    'CulturalChildrens',
    'HobbyChildrens',
    'ActivityChildrens',
    'GiftChildrens',
    'EventGrandChildrens',
    'CulturalGrandChildrens',
    'HobbyGrandChildrens',
    'ActivityGrandChildrens',
    'GiftGrandChildrens',
    'EventFamily',
    'CulturalFamily',
    'ActivityFamily'
);
ALTER TYPE audit."Tables" OWNER TO postgres;

-----------------------------------------------------------------

CREATE TABLE audit."Journal" 
(
 "Guid"               VARCHAR(36)             NOT NULL  PRIMARY KEY,
 "Operation"          audit."Operations"      NOT NULL,
 "DateTime"           TimeStamp               NOT NULL,
 "EmailUser"          VARCHAR(256)            NOT NULL,
 "Table"              audit."Tables"          NOT NULL
);
ALTER TABLE audit."Journal" 
OWNER TO postgres;

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE OR REPLACE FUNCTION audit.journal_insert_trigger()
    RETURNS trigger AS
$BODY$
DECLARE
    table_master    varchar(255)        := 'journal';
    table_part      varchar(255)        := '';
BEGIN
    -- Give the name of the partition --------------------------------------------------
    table_part := table_master || '_y' || date_part( 'year', NEW."DateTime" )::text || '_m' || date_part( 'month', NEW."DateTime" )::text;

    -- We check the partition for existence --------------------------------

    PERFORM 1 FROM pg_class WHERE relname = table_part LIMIT 1;

    -- If not, then create --------------------------------------------
    IF NOT FOUND
    THEN
    -- Create a partition, inheriting the master table --------------------------
        EXECUTE 'CREATE TABLE ' || 'audit.' || table_part || '()
                INHERITS ( ' || 'audit.' || '"' || 'Journal' || '"' || ' )
                WITH ( OIDS=FALSE );
                ALTER TABLE ' || 'audit.' || table_part || ' OWNER TO postgres;';

    -- Create indexes for the current partition. -------------------------------
        EXECUTE  'CREATE INDEX ' || table_part || '_journal_date_index ON ' || 'audit.' || table_part || ' USING btree ("Guid", "DateTime", "EmailUser")';  
    END IF;

    -- Insert the data into the partition --------------------------------------------
        EXECUTE 'INSERT INTO ' || 'audit.' || table_part || ' SELECT ( (' || quote_literal(NEW) || ')::' || 'audit."Journal"' || ' ).*';

    RETURN NULL;
END;
$BODY$
LANGUAGE plpgsql VOLATILE
COST 100;
ALTER FUNCTION audit.journal_insert_trigger()
OWNER TO postgres; 

CREATE TRIGGER journal_insert_trigger
BEFORE INSERT
ON audit."Journal"
FOR EACH ROW
EXECUTE PROCEDURE audit.journal_insert_trigger();