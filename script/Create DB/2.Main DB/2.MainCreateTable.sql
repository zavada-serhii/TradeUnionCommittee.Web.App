CREATE TABLE "Employee"(
        "Id" 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
        "FirstName" 		VARCHAR 	NOT NULL,
        "SecondName" 		VARCHAR 	NOT NULL,
        "Patronymic" 		VARCHAR         NULL,
        "Sex" 			VARCHAR         NOT NULL        CHECK ("Sex" ~ '^Male$'::TEXT OR "Sex" ~ '^Female$'::TEXT),
        "BirthDate" 		DATE 		NOT NULL,
        "IdentificationCode" 	VARCHAR         NULL            CHECK ("IdentificationCode" ~ '^\d{10}$'::TEXT) 			UNIQUE,
        "MechnikovCard" 	VARCHAR         NULL            CHECK ("MechnikovCard" ~ '^\d{1}\-\d{6}\-\d{6}$'::TEXT)			UNIQUE,
        "MobilePhone" 		VARCHAR         NULL            CHECK ("MobilePhone" ~ '^\+38\(\d{3}\)\d{3}\-\d{2}\-\d{2}$'::TEXT),
        "CityPhone" 		VARCHAR         NULL            CHECK ("CityPhone" ~ '^\d{3}\-\d{2}\-\d{1}$'::TEXT OR "CityPhone" ~ '^\d{3}\-\d{2}\-\d{2}$'::TEXT),
        "BasicProfession" 	VARCHAR 	NOT NULL,
        "StartYearWork" 	INT 		NOT NULL 	CHECK("StartYearWork" >= CAST(EXTRACT(YEAR FROM "BirthDate") AS INT)),
        "EndYearWork" 		INT             NULL            CHECK("EndYearWork" >= "StartYearWork"),
        "StartDateTradeUnion" 	DATE 		NOT NULL 	CHECK(CAST(EXTRACT(YEAR FROM  "StartDateTradeUnion") AS INT) >= "StartYearWork"),
        "EndDateTradeUnion" 	DATE            NULL            CHECK("EndDateTradeUnion" > "StartDateTradeUnion"),
        "LevelEducation" 	VARCHAR		NOT NULL,
	"NameInstitution" 	VARCHAR		NOT NULL,
	"YearReceiving" 	INT             NULL,
        "ScientificDegree"	VARCHAR		NULL,
	"ScientificTitle" 	VARCHAR		NULL,
        "Note" 			TEXT            NULL,
        "DateAdded" 		DATE 		NOT NULL        DEFAULT CURRENT_DATE
);
ALTER TABLE "Employee"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "Children"(
	"Id" 			BIGSERIAL 	NOT NULL	PRIMARY KEY,
	"IdEmployee" 		BIGINT 		NOT NULL 	REFERENCES "Employee"("Id") ON UPDATE CASCADE ON DELETE CASCADE,
	"FirstName" 		VARCHAR 	NOT NULL,
        "SecondName" 		VARCHAR 	NOT NULL,
        "Patronymic" 		VARCHAR         NULL,
	"BirthDate"		DATE 		NOT NULL
);
ALTER TABLE "Children"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "GrandChildren"(
	"Id" 			BIGSERIAL 	NOT NULL	PRIMARY KEY,
	"IdEmployee" 		BIGINT 		NOT NULL 	REFERENCES "Employee"("Id") ON UPDATE CASCADE ON DELETE CASCADE,
	"FirstName" 		VARCHAR 	NOT NULL,
        "SecondName" 		VARCHAR 	NOT NULL,
        "Patronymic" 		VARCHAR         NULL,
	"BirthDate"		DATE 		NOT NULL
);
ALTER TABLE "GrandChildren"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "Family"(
	"Id" 			BIGSERIAL 	NOT NULL	PRIMARY KEY,
	"IdEmployee" 		BIGINT 		NOT NULL 	REFERENCES "Employee"("Id") ON UPDATE CASCADE ON DELETE CASCADE,
	"FirstName" 		VARCHAR 	NOT NULL,
        "SecondName" 		VARCHAR 	NOT NULL,
        "Patronymic" 		VARCHAR         NULL,
	"BirthDate"		DATE 		NULL
);
ALTER TABLE "Family"
OWNER TO postgres;