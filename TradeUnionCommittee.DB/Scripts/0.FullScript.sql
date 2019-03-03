CREATE TABLE "AspNetRoles"
(
    "Id"                            text COLLATE pg_catalog."default" NOT NULL,
    "Name"                          character varying(256) COLLATE pg_catalog."default",
    "NormalizedName"                character varying(256) COLLATE pg_catalog."default",
    "ConcurrencyStamp"              text COLLATE pg_catalog."default",
    CONSTRAINT "PK_AspNetRoles" PRIMARY KEY ("Id")
);
ALTER TABLE "AspNetRoles"
OWNER to postgres;

CREATE TABLE "AspNetRoleClaims"
(
    "Id"                            SERIAL,
    "RoleId"                        text COLLATE pg_catalog."default" NOT NULL,
    "ClaimType"                     text COLLATE pg_catalog."default",
    "ClaimValue"                    text COLLATE pg_catalog."default",
    CONSTRAINT "PK_AspNetRoleClaims" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_AspNetRoleClaims_AspNetRoles_RoleId" FOREIGN KEY ("RoleId")
        REFERENCES "AspNetRoles" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
);
ALTER TABLE "AspNetRoleClaims"
OWNER to postgres;


CREATE TABLE "AspNetUsers"
(
    "Id"                            text COLLATE pg_catalog."default" NOT NULL,
    "UserName"                      character varying(256) COLLATE pg_catalog."default",
    "NormalizedUserName"            character varying(256) COLLATE pg_catalog."default",
    "Email"                         character varying(256) COLLATE pg_catalog."default",
    "NormalizedEmail"               character varying(256) COLLATE pg_catalog."default",
    "EmailConfirmed"                boolean NOT NULL,
    "PasswordHash"                  text COLLATE pg_catalog."default",
    "SecurityStamp"                 text COLLATE pg_catalog."default",
    "ConcurrencyStamp"              text COLLATE pg_catalog."default",
    "PhoneNumber"                   text COLLATE pg_catalog."default",
    "PhoneNumberConfirmed"          boolean NOT NULL,
    "TwoFactorEnabled"              boolean NOT NULL,
    "LockoutEnd"                    timestamp with time zone,
    "LockoutEnabled"                boolean NOT NULL,
    "AccessFailedCount"             integer NOT NULL,
    CONSTRAINT "PK_AspNetUsers" PRIMARY KEY ("Id")
);
ALTER TABLE "AspNetUsers"
OWNER to postgres;

CREATE TABLE "AspNetUserClaims"
(
    "Id"                            SERIAL,
    "UserId"                        text COLLATE pg_catalog."default" NOT NULL,
    "ClaimType"                     text COLLATE pg_catalog."default",
    "ClaimValue"                    text COLLATE pg_catalog."default",
    CONSTRAINT "PK_AspNetUserClaims" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_AspNetUserClaims_AspNetUsers_UserId" FOREIGN KEY ("UserId")
        REFERENCES "AspNetUsers" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
);
ALTER TABLE "AspNetUserClaims"
OWNER to postgres;

CREATE TABLE "AspNetUserLogins"
(
    "LoginProvider"                 text COLLATE pg_catalog."default" NOT NULL,
    "ProviderKey"                   text COLLATE pg_catalog."default" NOT NULL,
    "ProviderDisplayName"           text COLLATE pg_catalog."default",
    "UserId"                        text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "PK_AspNetUserLogins" PRIMARY KEY ("LoginProvider", "ProviderKey"),
    CONSTRAINT "FK_AspNetUserLogins_AspNetUsers_UserId" FOREIGN KEY ("UserId")
        REFERENCES "AspNetUsers" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
);
ALTER TABLE "AspNetUserLogins"
OWNER to postgres;

CREATE TABLE "AspNetUserRoles"
(
    "UserId"                        text COLLATE pg_catalog."default" NOT NULL,
    "RoleId"                        text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "PK_AspNetUserRoles" PRIMARY KEY ("UserId", "RoleId"),
    CONSTRAINT "FK_AspNetUserRoles_AspNetRoles_RoleId" FOREIGN KEY ("RoleId")
        REFERENCES "AspNetRoles" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE,
    CONSTRAINT "FK_AspNetUserRoles_AspNetUsers_UserId" FOREIGN KEY ("UserId")
        REFERENCES "AspNetUsers" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
);
ALTER TABLE "AspNetUserRoles"
OWNER to postgres;

CREATE TABLE "AspNetUserTokens"
(
    "UserId"                        text COLLATE pg_catalog."default" NOT NULL,
    "LoginProvider"                 text COLLATE pg_catalog."default" NOT NULL,
    "Name"                          text COLLATE pg_catalog."default" NOT NULL,
    "Value"                         text COLLATE pg_catalog."default",
    CONSTRAINT "PK_AspNetUserTokens" PRIMARY KEY ("UserId", "LoginProvider", "Name"),
    CONSTRAINT "FK_AspNetUserTokens_AspNetUsers_UserId" FOREIGN KEY ("UserId")
        REFERENCES "AspNetUsers" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
);
ALTER TABLE "AspNetUserTokens"
OWNER to postgres;

-----------------------------------------------------------

CREATE INDEX "EmailIndex"
    ON "AspNetUsers" USING btree
    ("NormalizedEmail" COLLATE pg_catalog."default")
    TABLESPACE pg_default;

CREATE UNIQUE INDEX "UserNameIndex"
    ON "AspNetUsers" USING btree
    ("NormalizedUserName" COLLATE pg_catalog."default")
    TABLESPACE pg_default;

CREATE INDEX "IX_AspNetUserRoles_RoleId"
    ON "AspNetUserRoles" USING btree
    ("RoleId" COLLATE pg_catalog."default")
    TABLESPACE pg_default;

CREATE INDEX "IX_AspNetUserLogins_UserId"
    ON "AspNetUserLogins" USING btree
    ("UserId" COLLATE pg_catalog."default")
    TABLESPACE pg_default;

CREATE INDEX "IX_AspNetUserClaims_UserId"
    ON "AspNetUserClaims" USING btree
    ("UserId" COLLATE pg_catalog."default")
    TABLESPACE pg_default;

CREATE UNIQUE INDEX "RoleNameIndex"
    ON "AspNetRoles" USING btree
    ("NormalizedName" COLLATE pg_catalog."default")
    TABLESPACE pg_default;

CREATE INDEX "IX_AspNetRoleClaims_RoleId"
    ON "AspNetRoleClaims" USING btree
    ("RoleId" COLLATE pg_catalog."default")
    TABLESPACE pg_default;

-----------------------------------------------------------

INSERT INTO "AspNetUsers"("Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnd", "LockoutEnabled", "AccessFailedCount") VALUES
('80c3b9e5-48f0-43dc-9226-dc05f13cd471','stewie.griffin@test.com','STEWIE.GRIFFIN@TEST.COM','stewie.griffin@test.com','STEWIE.GRIFFIN@TEST.COM',FALSE,'AQAAAAEAACcQAAAAEIpy4VbmDRxJOayhY6V2VSnGY+TtihLeiAxuHHcfpw++bIz5Qh1Zt/J3fQIU7MmojA==','7W3OKJNGBY43TZIF4B2U5QY5GRDG2WAX','03e08ee0-4d03-42d0-81a7-9f80d4f5151a',NULL,FALSE,FALSE,NULL,TRUE,0),
('bce59a46-2f93-4ab2-9e71-a023f7a851db','accountant@test.com','ACCOUNTANT@TEST.COM','accountant@test.com','ACCOUNTANT@TEST.COM',FALSE,'AQAAAAEAACcQAAAAEJmfjCDspGDCNMU0UKXmZ4aOqJ/kZr37HrVv1MWBDlHjSSfPBEqYBFrFTJYMFcUf2w==','O7GGVB4T3E2N4QRC6C2PC6GOML6WQ4VT','b320746f-de84-4ca9-ad12-be70aa943a20',NULL,FALSE,FALSE,NULL,TRUE,0),
('a2fd029e-f6d0-49aa-b3ca-c49d645bf41f','deputy@test.com','DEPUTY@TEST.COM','deputy@test.com','DEPUTY@TEST.COM',FALSE,'AQAAAAEAACcQAAAAEEHFo6yQWrGPFQwPUOFDtZ0V7C+F/oajrxJHWeeiWFZUVeP6AQKZ03+D66p0/wtQqQ==','N7SFWMK4W2XGIK4DFK3XAQMCW6FOQCXL','68f3773b-d59a-45fa-8763-519f60816d3d',NULL,FALSE,FALSE,NULL,TRUE,0);

INSERT INTO "AspNetRoles"("Id", "Name", "NormalizedName", "ConcurrencyStamp") VALUES
('c2c50d82-63d5-4764-a1b0-2919a826a4e6','Admin','ADMIN','7f5ebe86-e5d5-4c70-9f4e-467ff3c4d2d3'),
('0c277213-cf17-4be6-966f-316233db2dc5','Accountant','ACCOUNTANT','f61ce026-bd54-4882-85c8-fe6ec3cc8227'),
('121b555e-1037-4d80-90cc-1b5101d1af6d','Deputy','DEPUTY','80e5d186-5075-4cc4-ab95-5b70dbef7f2e');

INSERT INTO "AspNetUserRoles"("UserId", "RoleId") VALUES
('80c3b9e5-48f0-43dc-9226-dc05f13cd471','c2c50d82-63d5-4764-a1b0-2919a826a4e6'),
('bce59a46-2f93-4ab2-9e71-a023f7a851db','0c277213-cf17-4be6-966f-316233db2dc5'),
('a2fd029e-f6d0-49aa-b3ca-c49d645bf41f','121b555e-1037-4d80-90cc-1b5101d1af6d');

--Credentials
--stewie.griffin@test.com  P@ssw0rd_admin
--accountant@test.com      P@ssw0rd_accountant
--deputy@test.com          P@ssw0rd_deputy


CREATE TABLE "Employee"(
        "Id" 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
        "FirstName" 		VARCHAR 	NOT NULL,
        "SecondName" 		VARCHAR 	NOT NULL,
        "Patronymic" 		VARCHAR         NULL,
        "Sex" 			VARCHAR         NOT NULL        CHECK ("Sex" ~ '^Male$'::TEXT OR "Sex" ~ '^Female$'::TEXT),
        "BirthDate" 		DATE 		NOT NULL,
        "IdentificationСode" 	VARCHAR         NULL            CHECK ("IdentificationСode" ~ '^\d{10}$'::TEXT) 			UNIQUE,
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

CREATE TABLE "AwardEmployees"(
	"Id" 			BIGSERIAL 	NOT NULL	PRIMARY KEY,
	"IdEmployee"		BIGINT 		NOT NULL	REFERENCES "Employee"("Id") ON UPDATE CASCADE ON DELETE CASCADE,
	"IdAward"		BIGINT 		NOT NULL	REFERENCES "Award"("Id"),
	"Amount" 		MONEY		NOT NULL,
	"DateIssue"		DATE		NOT NULL,
	UNIQUE("IdEmployee","IdAward","DateIssue")
);
ALTER TABLE "AwardEmployees"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "MaterialAidEmployees"(
	"Id" 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	"IdEmployee"		BIGINT 		NOT NULL	REFERENCES "Employee"("Id") ON UPDATE CASCADE ON DELETE CASCADE,
	"IdMaterialAid"		BIGINT 		NOT NULL	REFERENCES "MaterialAid"("Id"),
	"Amount" 		MONEY		NOT NULL,
	"DateIssue"		DATE 		NOT NULL,
	UNIQUE("IdEmployee","IdMaterialAid","DateIssue")
);
ALTER TABLE "MaterialAidEmployees"
OWNER TO postgres;	

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "HobbyEmployees"(
	"Id" 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	"IdEmployee"		BIGINT 		NOT NULL 	REFERENCES "Employee"("Id") ON UPDATE CASCADE ON DELETE CASCADE,
	"IdHobby" 		BIGINT 		NOT NULL	REFERENCES "Hobby"("Id"),
	UNIQUE("IdEmployee", "IdHobby")
);
ALTER TABLE "HobbyEmployees"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "FluorographyEmployees"(
	"Id" 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	"IdEmployee"		BIGINT 		NOT NULL 	REFERENCES "Employee"("Id") ON UPDATE CASCADE ON DELETE CASCADE,
	"PlacePassing" 		VARCHAR 	NOT NULL,
	"Result" 		VARCHAR 	NOT NULL, 
	"DatePassage"		DATE 		NOT NULL,
	UNIQUE ("IdEmployee","Result","DatePassage")
);
ALTER TABLE "FluorographyEmployees"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "EventEmployees"(
	"Id" 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	"IdEmployee" 		BIGINT 		NOT NULL	REFERENCES "Employee"("Id") ON UPDATE CASCADE ON DELETE CASCADE,
	"IdEvent"		BIGINT 		NOT NULL	REFERENCES "Event"("Id"),		
 	"Amount"		MONEY		NOT NULL,
 	"Discount" 		MONEY 		NOT NULL,
	"StartDate"		DATE 		NOT NULL,
	"EndDate"		DATE 		NOT NULL	CHECK("EndDate" >= "StartDate"),
	UNIQUE("IdEmployee","IdEvent","StartDate")
);
ALTER TABLE "EventEmployees"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "CulturalEmployees"(
	"Id" 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	"IdEmployee" 		BIGINT 		NOT NULL	REFERENCES "Employee"("Id") ON UPDATE CASCADE ON DELETE CASCADE,
	"IdCultural"		BIGINT 		NOT NULL	REFERENCES "Cultural"("Id"),
	"Amount"		MONEY		NOT NULL,
	"Discount" 		MONEY 		NOT NULL,
	"DateVisit"		DATE 		NOT NULL,
	UNIQUE("IdEmployee","IdCultural","DateVisit")
);
ALTER TABLE "CulturalEmployees"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "ActivityEmployees"(
	"Id" 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	"IdEmployee" 		BIGINT 		NOT NULL	REFERENCES "Employee"("Id") ON UPDATE CASCADE ON DELETE CASCADE,
	"IdActivities"		BIGINT 		NOT NULL	REFERENCES "Activities"("Id"),
	"DateEvent"		DATE 		NOT NULL,
	UNIQUE("IdEmployee","IdActivities","DateEvent")
);
ALTER TABLE "ActivityEmployees"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "GiftEmployees"(
	"Id" 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	"IdEmployee"		BIGINT 		NOT NULL	REFERENCES "Employee"("Id") ON UPDATE CASCADE ON DELETE CASCADE,
	"NameEvent" 		VARCHAR 	NOT NULL,
	"NameGift" 		VARCHAR 	NOT NULL,
	"Price" 		MONEY 		NOT NULL,
	"Discount" 		MONEY 		NOT NULL,
	"DateGift" 		DATE 		NOT NULL,
 	UNIQUE("IdEmployee","NameEvent","NameGift","DateGift")
);
ALTER TABLE "GiftEmployees"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "PrivilegeEmployees"(
	"Id" 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	"IdEmployee"	 	BIGINT 		NOT NULL	REFERENCES "Employee"("Id") ON UPDATE CASCADE ON DELETE CASCADE,
	"IdPrivileges"		BIGINT 		NOT NULL	REFERENCES "Privileges"("Id"),
	"Note" 			TEXT,
	"CheckPrivileges" 	BOOLEAN 	NOT NULL,
	UNIQUE("IdEmployee")
);
ALTER TABLE "PrivilegeEmployees"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "SocialActivityEmployees"(
	"Id" 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	"IdEmployee"		BIGINT 		NOT NULL	REFERENCES "Employee"("Id") ON UPDATE CASCADE ON DELETE CASCADE,
	"IdSocialActivity"	BIGINT 		NOT NULL	REFERENCES "SocialActivity"("Id"),
	"Note" 			TEXT,
	"CheckSocialActivity" 	BOOLEAN 	NOT NULL,
	UNIQUE("IdEmployee")
);
ALTER TABLE "SocialActivityEmployees"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "PositionEmployees"(
	"Id" 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	"IdEmployee" 		BIGINT 		NOT NULL	REFERENCES "Employee"("Id") ON UPDATE CASCADE ON DELETE CASCADE,
	"IdSubdivision"		BIGINT 		NOT NULL	REFERENCES "Subdivisions"("Id"),
	"IdPosition"		BIGINT 		NOT NULL	REFERENCES "Position"("Id"),
	"CheckPosition" 	BOOLEAN 	NOT NULL,
	"StartDate" 		DATE,
	"EndDate"		DATE, 						
	UNIQUE("IdEmployee")
);
ALTER TABLE "PositionEmployees"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "PublicHouseEmployees"(
        "Id" 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	"IdAddressPublicHouse" 	BIGINT 		NOT NULL 	REFERENCES "AddressPublicHouse"("Id"),
	"IdEmployee"		BIGINT 		NOT NULL 	REFERENCES "Employee"("Id") ON UPDATE CASCADE ON DELETE CASCADE,
	"NumberRoom" 		VARCHAR,
	UNIQUE ("IdAddressPublicHouse", "IdEmployee")
);
ALTER TABLE "PublicHouseEmployees"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "PrivateHouseEmployees"(
	"Id" 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	"IdEmployee"		BIGINT 		NOT NULL 	REFERENCES "Employee"("Id") ON UPDATE CASCADE ON DELETE CASCADE,
	"City"			VARCHAR 	NOT NULL,
	"Street" 		VARCHAR		NOT NULL,
	"NumberHouse" 		VARCHAR,
	"NumberApartment" 	VARCHAR,
	"DateReceiving" 	DATE
);
ALTER TABLE "PrivateHouseEmployees"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "ApartmentAccountingEmployees"(
	"Id" 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	"IdEmployee"		BIGINT 		NOT NULL 	REFERENCES "Employee"("Id") ON UPDATE CASCADE ON DELETE CASCADE,
	"FamilyComposition"	BIGINT 		NOT NULL,
	"NameAdministration"	VARCHAR 	NOT NULL,
	"PriorityType"		VARCHAR 	NOT NULL,
	"DateAdoption"		DATE 		NOT NULL,											
	"DateInclusion"		DATE 				CHECK("DateInclusion" > "DateAdoption"),
	"Position"		VARCHAR		NOT NULL,
	"StartYearWork"		INT 		NOT NULL,
	UNIQUE("IdEmployee","FamilyComposition","NameAdministration","PriorityType","DateAdoption","Position","StartYearWork")
);
ALTER TABLE "ApartmentAccountingEmployees"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "EventChildrens"(
	"Id" 			BIGSERIAL 	NOT NULL	PRIMARY KEY,
	"IdChildren" 		BIGINT 		NOT NULL	REFERENCES "Children"("Id") ON UPDATE CASCADE ON DELETE CASCADE,
	"IdEvent"		BIGINT 		NOT NULL	REFERENCES "Event"("Id"),
	"Amount"		MONEY		NOT NULL,
	"Discount" 		MONEY 		NOT NULL,
	"StartDate"		DATE 		NOT NULL,
	"EndDate"		DATE 		NOT NULL	CHECK("EndDate" >= "StartDate"),
	UNIQUE("IdChildren","IdEvent","StartDate")
);
ALTER TABLE "EventChildrens"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "CulturalChildrens"(
	"Id" 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	"IdChildren"		BIGINT 		NOT NULL	REFERENCES "Children"("Id") ON UPDATE CASCADE ON DELETE CASCADE,
	"IdCultural" 		BIGINT 		NOT NULL	REFERENCES "Cultural"("Id"),
	"Amount"		MONEY		NOT NULL,
	"Discount" 		MONEY 		NOT NULL,
	"DateVisit"		DATE 		NOT NULL,
	UNIQUE("IdChildren","IdCultural","DateVisit")
);
ALTER TABLE "CulturalChildrens"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "HobbyChildrens"(
	"Id" 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	"IdChildren"		BIGINT		NOT NULL	REFERENCES "Children"("Id") ON UPDATE CASCADE ON DELETE CASCADE,
	"IdHobby" 		BIGINT 		NOT NULL	REFERENCES "Hobby"("Id"),
	UNIQUE("IdChildren","IdHobby")
);
ALTER TABLE "HobbyChildrens"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "ActivityChildrens"(
	"Id" 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	"IdChildren"		BIGINT		NOT NULL	REFERENCES "Children"("Id") ON UPDATE CASCADE ON DELETE CASCADE,
	"IdActivities"		BIGINT 		NOT NULL	REFERENCES "Activities"("Id"),
	"DateEvent"		DATE 		NOT NULL,
	UNIQUE("IdChildren","IdActivities","DateEvent")
);
ALTER TABLE "ActivityChildrens"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "GiftChildrens"(
	"Id" 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	"IdChildren"		BIGINT		NOT NULL	REFERENCES "Children"("Id") ON UPDATE CASCADE ON DELETE CASCADE,
	"NameEvent" 		VARCHAR 	NOT NULL,
	"NameGift" 		VARCHAR 	NOT NULL,
	"Price" 		MONEY 		NOT NULL,
	"Discount" 		MONEY 		NOT NULL,
	"DateGift" 		DATE 		NOT NULL,
 	UNIQUE("IdChildren","NameEvent","NameGift","DateGift")
);
ALTER TABLE "GiftChildrens"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "EventGrandChildrens"(
	"Id" 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	"IdGrandChildren" 	BIGINT 		NOT NULL	REFERENCES "GrandChildren"("Id") ON UPDATE CASCADE ON DELETE CASCADE,
	"IdEvent"		BIGINT 		NOT NULL	REFERENCES "Event"("Id"),
	"Amount"		MONEY		NOT NULL,
	"Discount" 		MONEY 		NOT NULL,
	"StartDate"		DATE 		NOT NULL,
	"EndDate"		DATE 		NOT NULL	CHECK("EndDate" >= "StartDate"),
	UNIQUE("IdGrandChildren","IdEvent","StartDate")
);
ALTER TABLE "EventGrandChildrens"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "CulturalGrandChildrens"(
	"Id" 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	"IdGrandChildren" 	BIGINT 		NOT NULL	REFERENCES "GrandChildren"("Id") ON UPDATE CASCADE ON DELETE CASCADE,
	"IdCultural" 		BIGINT 		NOT NULL	REFERENCES "Cultural"("Id"),
	"Amount"		MONEY		NOT NULL,
	"Discount" 		MONEY 		NOT NULL,
	"DateVisit"		DATE 		NOT NULL,
	UNIQUE("IdGrandChildren","IdCultural","DateVisit")
);
ALTER TABLE "CulturalGrandChildrens"
  OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "HobbyGrandChildrens"(
	"Id" 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	"IdGrandChildren" 	BIGINT 		NOT NULL	REFERENCES "GrandChildren"("Id") ON UPDATE CASCADE ON DELETE CASCADE,
	"IdHobby" 		BIGINT 		NOT NULL	REFERENCES "Hobby"("Id"),
	UNIQUE("IdGrandChildren", "IdHobby")
);
ALTER TABLE "HobbyGrandChildrens"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "ActivityGrandChildrens"(
	"Id" 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	"IdGrandChildren"	BIGINT		NOT NULL	REFERENCES "GrandChildren"("Id") ON UPDATE CASCADE ON DELETE CASCADE,
	"IdActivities"		BIGINT 		NOT NULL	REFERENCES "Activities"("Id"),
	"DateEvent"		DATE 		NOT NULL,
	UNIQUE("IdGrandChildren","IdActivities","DateEvent")
);
ALTER TABLE "ActivityGrandChildrens"
OWNER TO postgres;	

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "GiftGrandChildrens"(
	"Id" 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	"IdGrandChildren"	BIGINT		NOT NULL	REFERENCES "GrandChildren"("Id") ON UPDATE CASCADE ON DELETE CASCADE,
	"NameEvent" 		VARCHAR 	NOT NULL,
	"NameGift" 		VARCHAR 	NOT NULL,
	"Price" 		MONEY 		NOT NULL,
	"Discount" 		MONEY 		NOT NULL,
	"DateGift" 		DATE 		NOT NULL,
 	UNIQUE("IdGrandChildren","NameEvent","NameGift","DateGift")
);
ALTER TABLE "GiftGrandChildrens"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "EventFamily"(
	"Id" 			BIGSERIAL 	NOT NULL	PRIMARY KEY,
	"IdFamily"		BIGINT		NOT NULL	REFERENCES "Family"("Id") ON UPDATE CASCADE ON DELETE CASCADE,
	"IdEvent"		BIGINT 		NOT NULL	REFERENCES "Event"("Id"),
	"Amount"		MONEY		NOT NULL,
	"Discount" 		MONEY 		NOT NULL,
	"StartDate"		DATE 		NOT NULL,
	"EndDate"		DATE 		NOT NULL	CHECK("EndDate" >= "StartDate"),
	UNIQUE("IdFamily","IdEvent","StartDate")
);
ALTER TABLE "EventFamily"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "CulturalFamily"(
	"Id" 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	"IdFamily"		BIGINT		NOT NULL	REFERENCES "Family"("Id") ON UPDATE CASCADE ON DELETE CASCADE,
	"IdCultural" 		BIGINT 		NOT NULL	REFERENCES "Cultural"("Id"),
	"Amount"		MONEY		NOT NULL,
	"Discount" 		MONEY 		NOT NULL,
	"DateVisit"		DATE 		NOT NULL,
	UNIQUE("IdFamily","IdCultural","DateVisit")
);
ALTER TABLE "CulturalFamily"
OWNER TO postgres;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE "ActivityFamily"(
	"Id" 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	"IdFamily"		BIGINT		NOT NULL	REFERENCES "Family"("Id") ON UPDATE CASCADE ON DELETE CASCADE,
	"IdActivities"		BIGINT 		NOT NULL	REFERENCES "Activities"("Id"),
	"DateEvent"		DATE 		NOT NULL,
	UNIQUE("IdFamily","IdActivities","DateEvent")
);
ALTER TABLE "ActivityFamily"
OWNER TO postgres;


CREATE EXTENSION "pg_trgm" SCHEMA public VERSION "1.3";

---------------------------------------------------------------------------------------------------------------------

CREATE FUNCTION public."TrigramFullName"(p public."Employee")
RETURNS TEXT LANGUAGE plpgsql IMMUTABLE AS $$
BEGIN

RETURN lower(trim(coalesce(p."FirstName",'') || ' ' ||coalesce(p."SecondName",'') || ' ' ||coalesce(p."Patronymic",'')));

EXCEPTION WHEN others THEN RAISE EXCEPTION '%', sqlerrm; END; $$;
ALTER FUNCTION public."TrigramFullName"(p public."Employee")
OWNER TO postgres;

---------------------------------------------------------------------------------------------------------------------

CREATE INDEX info_gist_idx ON public."Employee"
USING gist(public."TrigramFullName"("Employee") gist_trgm_ops);

CREATE INDEX info_trgm_idx ON public."Employee"
USING gin(public."TrigramFullName"("Employee") gin_trgm_ops);



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
 "IpUser"			  CIDR 					  NOT NULL,
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