CREATE TABLE "Award"(
	"Id" 		BIGSERIAL	NOT NULL	PRIMARY KEY,
	"Name"		VARCHAR		NOT NULL	UNIQUE
);
ALTER TABLE "Award"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "MaterialAid"(
	"Id" 		BIGSERIAL	NOT NULL	PRIMARY KEY,
	"Name"		VARCHAR		NOT NULL	UNIQUE
);
ALTER TABLE "MaterialAid"
OWNER TO postgres;	

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "Hobby"(
	"Id"		BIGSERIAL	NOT NULL	PRIMARY KEY,
	"Name"		VARCHAR		NOT NULL	UNIQUE
);
ALTER TABLE "Hobby"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TYPE "TypeEvent" AS ENUM ('Travel', 'Wellness', 'Tour');
ALTER TYPE "TypeEvent"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "Event" (
	"Id" 		BIGSERIAL	NOT NULL	PRIMARY KEY,
	"Name"		VARCHAR		NOT NULL,
	"Type" 		"TypeEvent" NOT NULL,
	UNIQUE("Name", "Type")
);
ALTER TABLE "Event"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "Cultural"(
	"Id"		BIGSERIAL	NOT NULL	PRIMARY KEY,
	"Name"		VARCHAR		NOT NULL	UNIQUE
);
ALTER TABLE "Cultural"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "Activities"(
	"Id" 		BIGSERIAL	NOT NULL	PRIMARY KEY,
	"Name"		VARCHAR		NOT NULL	UNIQUE
);
ALTER TABLE  "Activities"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "Privileges"(
	"Id" 		BIGSERIAL	NOT NULL	PRIMARY KEY,
	"Name"		VARCHAR		NOT NULL	UNIQUE
);
ALTER TABLE  "Privileges"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "SocialActivity"(
	"Id"		BIGSERIAL	NOT NULL	PRIMARY KEY,
	"Name"		VARCHAR		NOT NULL	UNIQUE
);
ALTER TABLE "SocialActivity"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "Position"(
	"Id" 		BIGSERIAL	NOT NULL	PRIMARY KEY,
	"Name"		VARCHAR		NOT NULL	UNIQUE
);
ALTER TABLE "Position"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "Subdivisions"(
	"Id" 		BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	"IdSubordinate" BIGINT 				REFERENCES "Subdivisions"("Id"),
	"Name" 	VARCHAR 	NOT NULL 	UNIQUE,
	"Abbreviation" VARCHAR  NOT NULL 	UNIQUE
);
ALTER TABLE "Subdivisions"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TYPE "TypeHouse" AS ENUM ('Dormitory', 'Departmental');
ALTER TYPE "TypeHouse"
OWNER TO postgres;
-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "AddressPublicHouse"(
	"Id" 				BIGSERIAL 		NOT NULL 	PRIMARY KEY,
	"City" 				VARCHAR 		NOT NULL,
	"Street" 			VARCHAR 		NOT NULL,
	"NumberHouse" 		VARCHAR 		NOT NULL,
	"NumberDormitory" 	VARCHAR 		NULL,
	"Type" 				"TypeHouse" 	NOT NULL,
	UNIQUE("City","Street","NumberHouse","Type")
);
ALTER TABLE "AddressPublicHouse"
OWNER TO postgres;