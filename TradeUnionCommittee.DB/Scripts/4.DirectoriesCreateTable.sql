CREATE TABLE "Award"(
	"Id" 		BIGSERIAL	NOT NULL	PRIMARY KEY,
	"Name"		VARCHAR		NOT NULL	UNIQUE
);
ALTER TABLE "Award"
OWNER TO AdminTradeUnionCommitteeEmployees;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "MaterialAid"(
	"Id" 		BIGSERIAL	NOT NULL	PRIMARY KEY,
	"Name"		VARCHAR		NOT NULL	UNIQUE
);
ALTER TABLE "MaterialAid"
OWNER TO AdminTradeUnionCommitteeEmployees;	

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "Hobby"(
	"Id"		BIGSERIAL	NOT NULL	PRIMARY KEY,
	"Name"		VARCHAR		NOT NULL	UNIQUE
);
ALTER TABLE "Hobby"
OWNER TO AdminTradeUnionCommitteeEmployees;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "TypeEvent" (
	"Id" 		BIGSERIAL	NOT NULL	PRIMARY KEY,
	"Name"		VARCHAR		NOT NULL	UNIQUE
);
ALTER TABLE "TypeEvent"
OWNER TO AdminTradeUnionCommitteeEmployees;

INSERT INTO  "TypeEvent" ("Name") VALUES
('Travel'),
('Wellness'),
('Tour');

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "Event" (
	"Id" 		BIGSERIAL	NOT NULL	PRIMARY KEY,
	"Name"		VARCHAR		NOT NULL	UNIQUE,
	"TypeId" 	BIGINT 		NOT NULL 	UNIQUE REFERENCES "TypeEvent"("Id")
);
ALTER TABLE "Event"
OWNER TO AdminTradeUnionCommitteeEmployees;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "Cultural"(
	"Id"		BIGSERIAL	NOT NULL	PRIMARY KEY,
	"Name"		VARCHAR		NOT NULL	UNIQUE
);
ALTER TABLE "Cultural"
OWNER TO AdminTradeUnionCommitteeEmployees;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "Activities"(
	"Id" 		BIGSERIAL	NOT NULL	PRIMARY KEY,
	"Name"		VARCHAR		NOT NULL	UNIQUE
);
ALTER TABLE  "Activities"
OWNER TO AdminTradeUnionCommitteeEmployees;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "Privileges"(
	"Id" 		BIGSERIAL	NOT NULL	PRIMARY KEY,
	"Name"		VARCHAR		NOT NULL	UNIQUE
);
ALTER TABLE  "Privileges"
OWNER TO AdminTradeUnionCommitteeEmployees;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "SocialActivity"(
	"Id"		BIGSERIAL	NOT NULL	PRIMARY KEY,
	"Name"		VARCHAR		NOT NULL	UNIQUE
);
ALTER TABLE "SocialActivity"
OWNER TO AdminTradeUnionCommitteeEmployees;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "Position"(
	"Id" 		BIGSERIAL	NOT NULL	PRIMARY KEY,
	"Name"		VARCHAR		NOT NULL	UNIQUE
);
ALTER TABLE "Position"
OWNER TO AdminTradeUnionCommitteeEmployees;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "Subdivisions"(
	"Id" 		BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	"IdSubordinate" BIGINT 				REFERENCES "Subdivisions"("Id"),
	"DeptName" 	VARCHAR 	NOT NULL 	UNIQUE
);
ALTER TABLE "Subdivisions"
OWNER TO AdminTradeUnionCommitteeEmployees;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "TypeHouse" (
	"Id" 		BIGSERIAL	NOT NULL		PRIMARY KEY,
	"Name"		VARCHAR		NOT NULL 		UNIQUE
);
ALTER TABLE "TypeHouse"
OWNER TO AdminTradeUnionCommitteeEmployees;

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
OWNER TO AdminTradeUnionCommitteeEmployees;