CREATE SCHEMA directories
  AUTHORIZATION admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE directories.Award(
	Id 		BIGSERIAL	NOT NULL	PRIMARY KEY,
	Name		VARCHAR		NOT NULL	UNIQUE
);
ALTER TABLE directories.Award
OWNER TO admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE directories.MaterialAid(
	Id 		BIGSERIAL	NOT NULL	PRIMARY KEY,
	Name		VARCHAR		NOT NULL	UNIQUE
);
ALTER TABLE directories.MaterialAid
OWNER TO admin;	

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE directories.Hobby(
	Id		BIGSERIAL	NOT NULL	PRIMARY KEY,
	Name		VARCHAR		NOT NULL	UNIQUE
);
ALTER TABLE directories.Hobby
OWNER TO admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE directories.TypeEvent (
	Id 		BIGSERIAL	NOT NULL	PRIMARY KEY,
	Name		VARCHAR		NOT NULL	UNIQUE
);
ALTER TABLE directories.TypeEvent
OWNER TO admin;

INSERT INTO  directories.TypeEvent (Name) VALUES
('Travel'),
('Wellness'),
('Tour');

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE directories.Event (
	Id 		BIGSERIAL	NOT NULL	PRIMARY KEY,
	Name		VARCHAR		NOT NULL	UNIQUE,
	TypeId 		BIGINT 		NOT NULL 	UNIQUE REFERENCES directories.TypeEvent(Id)
);
ALTER TABLE directories.Event
OWNER TO admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE directories.Cultural(
	Id		BIGSERIAL	NOT NULL	PRIMARY KEY,
	Name		VARCHAR		NOT NULL	UNIQUE
);
ALTER TABLE  directories.Cultural
OWNER TO admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE directories.Activities(
	Id 		BIGSERIAL	NOT NULL	PRIMARY KEY,
	Name		VARCHAR		NOT NULL	UNIQUE
);
ALTER TABLE  directories.Activities
OWNER TO admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE directories.Privileges(
	Id 		BIGSERIAL	NOT NULL	PRIMARY KEY,
	Name		VARCHAR		NOT NULL	UNIQUE
);
ALTER TABLE  directories.Privileges
OWNER TO admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE directories.SocialActivity(
	Id		BIGSERIAL	NOT NULL	PRIMARY KEY,
	Name		VARCHAR		NOT NULL	UNIQUE
);
ALTER TABLE directories.SocialActivity
OWNER TO admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE directories.Position(
	Id 		BIGSERIAL	NOT NULL	PRIMARY KEY,
	Name		VARCHAR		NOT NULL	UNIQUE
);
ALTER TABLE directories.Position
OWNER TO admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE directories.Subdivisions(
	Id 		BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	IdSubordinate 	BIGINT 				REFERENCES directories.Subdivisions(Id),
	DeptName 	VARCHAR 	NOT NULL 	UNIQUE
);
ALTER TABLE directories.Subdivisions
OWNER TO admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE directories.TypeHouse (
	Id 		BIGSERIAL	NOT NULL		PRIMARY KEY,
	Name		VARCHAR		NOT NULL 		UNIQUE
);
ALTER TABLE directories.TypeHouse
OWNER TO admin;

INSERT INTO  directories.TypeHouse (Name) VALUES 
('Dormitory'),
('Departmental');

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE directories.AddressPublicHouse(
	ID 		BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	City 		VARCHAR 	NOT NULL,
	Street 		VARCHAR 	NOT NULL,
	NumberHouse 	VARCHAR 	NOT NULL,
	NumberDormitory VARCHAR 	NULL,
	Type 		BIGINT 		NOT NULL 	REFERENCES directories.TypeHouse(Id),
	UNIQUE(City,Street,NumberHouse,Type)
);
ALTER TABLE directories.AddressPublicHouse
OWNER TO admin;