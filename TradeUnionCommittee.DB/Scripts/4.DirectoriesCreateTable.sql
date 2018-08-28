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

CREATE TABLE "TypeEvent" (
	"Id" 		BIGSERIAL	NOT NULL	PRIMARY KEY,
	"Name"		VARCHAR		NOT NULL	UNIQUE
);
ALTER TABLE "TypeEvent"
OWNER TO postgres;

INSERT INTO  "TypeEvent" ("Name") VALUES
('Travel'),
('Wellness'),
('Tour');

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "Event" (
	"Id" 		BIGSERIAL	NOT NULL	PRIMARY KEY,
	"Name"		VARCHAR		NOT NULL,
	"TypeId" 	BIGINT 		NOT NULL 	REFERENCES "TypeEvent"("Id"),
	UNIQUE("Name", "TypeId")
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
	"DeptName" 	VARCHAR 	NOT NULL 	UNIQUE,
	"Abbreviation" VARCHAR  NOT NULL 	UNIQUE
);
ALTER TABLE "Subdivisions"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "TypeHouse" (
	"Id" 		BIGSERIAL	NOT NULL		PRIMARY KEY,
	"Name"		VARCHAR		NOT NULL 		UNIQUE
);
ALTER TABLE "TypeHouse"
OWNER TO postgres;

INSERT INTO  "TypeHouse" ("Name") VALUES 
('Dormitory'),
('Departmental');

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "AddressPublicHouse"(
	"Id" 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	"City" 			VARCHAR 	NOT NULL,
	"Street" 		VARCHAR 	NOT NULL,
	"NumberHouse" 		VARCHAR 	NOT NULL,
	"NumberDormitory" 	VARCHAR 	NULL,
	"Type" 			BIGINT 		NOT NULL 	REFERENCES "TypeHouse"("Id"),
	UNIQUE("City","Street","NumberHouse","Type")
);
ALTER TABLE "AddressPublicHouse"
OWNER TO postgres;