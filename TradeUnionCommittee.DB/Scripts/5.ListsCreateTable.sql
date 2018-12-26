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
	"Result" 		VARCHAR 	NOT NULL	CHECK ("Result" ~ '^Norm$'::TEXT OR "Result" ~ '^Deviation$'::TEXT), 
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
	"StartDate" 		DATE		NOT NULL,
	"EndDate"			DATE, 						
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
	"NameGifts" 		VARCHAR 	NOT NULL,
	"Price" 		MONEY 		NOT NULL,
	"Discount" 		MONEY 		NOT NULL,
	"DateGift" 		DATE 		NOT NULL,
 	UNIQUE("IdGrandChildren","NameEvent","NameGifts","DateGift")
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