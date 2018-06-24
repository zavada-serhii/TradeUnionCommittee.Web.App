CREATE SCHEMA lists
  AUTHORIZATION admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE lists.AwardEmployees(
	Id 			BIGSERIAL 	NOT NULL	PRIMARY KEY,
	IdEmployee		BIGINT 		NOT NULL	REFERENCES main.Employee(Id) ON UPDATE CASCADE ON DELETE CASCADE,
	IdAward			BIGINT 		NOT NULL	REFERENCES directories.Award(Id),
	Amount 			MONEY		NOT NULL,
	DateIssue		DATE		NOT NULL,
	UNIQUE(IdEmployee,IdAward,DateIssue)
);
ALTER TABLE lists.AwardEmployees
OWNER TO admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE lists.MaterialAidEmployees(
	Id 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	IdEmployee		BIGINT 		NOT NULL	REFERENCES main.Employee(Id) ON UPDATE CASCADE ON DELETE CASCADE,
	IdMaterialAid		BIGINT 		NOT NULL	REFERENCES directories.MaterialAid(Id),
	Amount 			MONEY		NOT NULL,
	DateIssue		DATE 		NOT NULL,
	UNIQUE(IdEmployee,IdMaterialAid,DateIssue)
);
ALTER TABLE lists.MaterialAidEmployees
OWNER TO admin;	

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE lists.HobbyEmployees(
	Id 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	IdEmployee		BIGINT 		NOT NULL 	REFERENCES main.Employee(Id) ON UPDATE CASCADE ON DELETE CASCADE,
	IdHobby 		BIGINT 		NOT NULL	REFERENCES directories.Hobby(Id),
	UNIQUE(IdEmployee, IdHobby)
);
ALTER TABLE lists.HobbyEmployees
OWNER TO admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE lists.FluorographyEmployees(
	Id 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	IdEmployee		BIGINT 		NOT NULL 	REFERENCES main.Employee(Id) ON UPDATE CASCADE ON DELETE CASCADE,
	PlacePassing 		VARCHAR 	NOT NULL,
	Result 			VARCHAR 	NOT NULL	CHECK (Result ~ '^Norm$'::TEXT OR Result ~ '^Deviation$'::TEXT), 
	DatePassage		DATE 		NOT NULL,
	UNIQUE (IdEmployee,Result,DatePassage)
);
ALTER TABLE lists.FluorographyEmployees
OWNER TO admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE lists.EventEmployees(
	Id 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	IdEmployee 		BIGINT 		NOT NULL	REFERENCES main.Employee(Id) ON UPDATE CASCADE ON DELETE CASCADE,
	IdEvent			BIGINT 		NOT NULL	REFERENCES directories.Event(Id),		
 	Amount			MONEY		NOT NULL,
 	Discount 		MONEY 		NOT NULL,
	StartDate		DATE 		NOT NULL,
	EndDate			DATE 		NOT NULL	CHECK(EndDate >= StartDate),
	UNIQUE(IdEmployee,IdEvent,StartDate)
);
ALTER TABLE lists.EventEmployees
OWNER TO admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE lists.CulturalEmployees(
	Id 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	IdEmployee 		BIGINT 		NOT NULL	REFERENCES main.Employee(Id) ON UPDATE CASCADE ON DELETE CASCADE,
	IdCultural		BIGINT 		NOT NULL	REFERENCES directories.Cultural(Id),
	Amount			MONEY		NOT NULL,
	Discount 		MONEY 		NOT NULL,
	DateVisit		DATE 		NOT NULL,
	UNIQUE(IdEmployee,IdCultural,DateVisit)
);
ALTER TABLE lists.CulturalEmployees
OWNER TO admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE lists.ActivityEmployees(
	Id 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	IdEmployee 		BIGINT 		NOT NULL	REFERENCES main.Employee(Id) ON UPDATE CASCADE ON DELETE CASCADE,
	IdActivities		BIGINT 		NOT NULL	REFERENCES directories.Activities(Id),
	DateEvent		DATE 		NOT NULL,
	UNIQUE(IdEmployee,IdActivities,DateEvent)
);
ALTER TABLE lists.ActivityEmployees
OWNER TO admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE lists.GiftEmployees(
	Id 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	IdEmployee		BIGINT 		NOT NULL	REFERENCES main.Employee(Id) ON UPDATE CASCADE ON DELETE CASCADE,
	NameEvent 		VARCHAR 	NOT NULL,
	NameGift 		VARCHAR 	NOT NULL,
	Price 			MONEY 		NOT NULL,
	Discount 		MONEY 		NOT NULL,
	DateGift 		DATE 		NOT NULL,
 	UNIQUE(IdEmployee,NameEvent,NameGift,DateGift)
);
ALTER TABLE lists.GiftEmployees
OWNER TO admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE lists.PrivilegeEmployees(
	Id 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	IdEmployee	 	BIGINT 		NOT NULL	REFERENCES main.Employee(Id) ON UPDATE CASCADE ON DELETE CASCADE,
	IdPrivileges		BIGINT 		NOT NULL	REFERENCES directories.Privileges(Id),
	Note 			TEXT,
	CheckPrivileges 	BOOLEAN 	NOT NULL,
	UNIQUE(IdEmployee)
);
ALTER TABLE lists.PrivilegeEmployees
OWNER TO admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE lists.SocialActivityEmployees(
	Id 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	IdEmployee		BIGINT 		NOT NULL	REFERENCES main.Employee(Id) ON UPDATE CASCADE ON DELETE CASCADE,
	IdSocialActivity	BIGINT 		NOT NULL	REFERENCES directories.SocialActivity(Id),
	Note 			TEXT,
	CheckSocialActivity 	BOOLEAN 	NOT NULL,
	UNIQUE(IdEmployee)
);
ALTER TABLE lists.SocialActivityEmployees
OWNER TO admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE lists.PositionEmployees(
	Id 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	IdEmployee 		BIGINT 		NOT NULL	REFERENCES main.Employee(Id) ON UPDATE CASCADE ON DELETE CASCADE,
	IdSubdivision		BIGINT 		NOT NULL	REFERENCES directories.Subdivisions(Id),
	IdPosition		BIGINT 		NOT NULL	REFERENCES directories.Position(Id),
	CheckPosition 		BOOLEAN 	NOT NULL,
	StartDate 		DATE,
	EndDate			DATE, 						
	UNIQUE(IdEmployee)
);
ALTER TABLE lists.PositionEmployees
OWNER TO admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE lists.PublicHouseEmployees(
	IdAddressPublicHouse 	BIGINT 		NOT NULL 	REFERENCES directories.AddressPublicHouse(Id),
	IdEmployee		BIGINT 		NOT NULL 	REFERENCES main.Employee(Id) ON UPDATE CASCADE ON DELETE CASCADE,
	NumberRoom 		VARCHAR,
	PRIMARY KEY (IdAddressPublicHouse, IdEmployee));
ALTER TABLE lists.PublicHouseEmployees
OWNER TO admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE lists.PrivateHouseEmployees(
	Id 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	IdEmployee		BIGINT 		NOT NULL 	REFERENCES main.Employee(Id) ON UPDATE CASCADE ON DELETE CASCADE,
	City			VARCHAR 	NOT NULL,
	Street 			VARCHAR		NOT NULL,
	NumberHouse 		VARCHAR,
	NumberApartment 	VARCHAR,
	DateReceiving 		DATE
);
ALTER TABLE lists.PrivateHouseEmployees
OWNER TO admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE lists.ApartmentAccountingEmployees(
	ID 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	IdEmployee		BIGINT 		NOT NULL 	REFERENCES main.Employee(Id) ON UPDATE CASCADE ON DELETE CASCADE,
	FamilyComposition	BIGINT 		NOT NULL,
	NameAdministration	VARCHAR 	NOT NULL,
	PriorityType		VARCHAR 	NOT NULL,
	DateAdoption		DATE 		NOT NULL,											
	DateInclusion		DATE 				CHECK(DateInclusion > DateAdoption),
	Position		VARCHAR		NOT NULL,
	StartYearWork		INT 		NOT NULL,
	UNIQUE(IdEmployee,FamilyComposition,NameAdministration,PriorityType,DateAdoption,Position,StartYearWork)
);
ALTER TABLE lists.ApartmentAccountingEmployees
OWNER TO admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE lists.EventChildrens(
	Id 			BIGSERIAL 	NOT NULL	PRIMARY KEY,
	IdChildren 		BIGINT 		NOT NULL	REFERENCES main.Children(Id) ON UPDATE CASCADE ON DELETE CASCADE,
	IdEvent			BIGINT 		NOT NULL	REFERENCES directories.Event(Id),
	Amount			MONEY		NOT NULL,
	Discount 		MONEY 		NOT NULL,
	StartDate		DATE 		NOT NULL,
	EndDate			DATE 		NOT NULL	CHECK(EndDate >= StartDate),
	UNIQUE(IdChildren,IdEvent,StartDate)
);
ALTER TABLE lists.EventChildrens
OWNER TO admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE lists.CulturalChildrens(
	Id 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	IdChildren		BIGINT 		NOT NULL	REFERENCES main.Children(Id) ON UPDATE CASCADE ON DELETE CASCADE,
	IdCultural 		BIGINT 		NOT NULL	REFERENCES directories.Cultural(Id),
	Amount			MONEY		NOT NULL,
	Discount 		MONEY 		NOT NULL,
	DateVisit		DATE 		NOT NULL,
	UNIQUE(IdChildren,IdCultural,DateVisit)
);
ALTER TABLE lists.CulturalChildrens
OWNER TO admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE lists.HobbyChildrens(
	Id 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	IdChildren		BIGINT		NOT NULL	REFERENCES main.Children(Id) ON UPDATE CASCADE ON DELETE CASCADE,
	IdHobby 		BIGINT 		NOT NULL	REFERENCES directories.Hobby(Id),
	UNIQUE(IdChildren, IdHobby)
);
ALTER TABLE lists.HobbyChildrens
OWNER TO admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE lists.ActivityChildrens(
	Id 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	IdChildren		BIGINT		NOT NULL	REFERENCES main.Children(Id) ON UPDATE CASCADE ON DELETE CASCADE,
	IdActivities		BIGINT 		NOT NULL	REFERENCES directories.Activities(Id),
	DateEvent		DATE 		NOT NULL,
	UNIQUE(IdChildren,IdActivities,DateEvent)
);
ALTER TABLE lists.ActivityChildrens
OWNER TO admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE lists.GiftChildrens(
	Id 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	IdChildren		BIGINT		NOT NULL	REFERENCES main.Children(Id) ON UPDATE CASCADE ON DELETE CASCADE,
	NameEvent 		VARCHAR 	NOT NULL,
	NameGift 		VARCHAR 	NOT NULL,
	Price 			MONEY 		NOT NULL,
	Discount 		MONEY 		NOT NULL,
	DateGift 		DATE 		NOT NULL,
 	UNIQUE(IdChildren,NameEvent,NameGift,DateGift)
);
ALTER TABLE lists.GiftChildrens
OWNER TO admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE lists.EventGrandChildrens(
	Id 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	IdGrandChildren 	BIGINT 		NOT NULL	REFERENCES main.GrandChildren(Id) ON UPDATE CASCADE ON DELETE CASCADE,
	IdEvent			BIGINT 		NOT NULL	REFERENCES directories.Event(Id),
	Amount			MONEY		NOT NULL,
	Discount 		MONEY 		NOT NULL,
	StartDate		DATE 		NOT NULL,
	EndDate			DATE 		NOT NULL	CHECK(EndDate >= StartDate),
	UNIQUE(IDGrandChildren,IDEvent,StartDate)
);
ALTER TABLE lists.EventGrandChildrens
OWNER TO admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE lists.CulturalGrandChildrens(
	Id 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	IdGrandChildren 	BIGINT 		NOT NULL	REFERENCES main.GrandChildren(Id) ON UPDATE CASCADE ON DELETE CASCADE,
	IdCultural 		BIGINT 		NOT NULL	REFERENCES directories.Cultural(ID),
	Amount			MONEY		NOT NULL,
	Discount 		MONEY 		NOT NULL,
	DateVisit		DATE 		NOT NULL,
	UNIQUE(IdGrandChildren,IdCultural,DateVisit)
);
ALTER TABLE lists.CulturalGrandChildrens
  OWNER TO admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE lists.HobbyGrandChildrens(
	Id 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	IdGrandChildren 	BIGINT 		NOT NULL	REFERENCES main.GrandChildren(Id) ON UPDATE CASCADE ON DELETE CASCADE,
	IdHobby 		BIGINT 		NOT NULL	REFERENCES directories.Hobby(Id),
	UNIQUE(IdGrandChildren, IdHobby)
);
ALTER TABLE lists.HobbyGrandChildrens
OWNER TO admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE lists.ActivityGrandChildrens(
	Id 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	IdGrandChildren		BIGINT		NOT NULL	REFERENCES main.GrandChildren(Id) ON UPDATE CASCADE ON DELETE CASCADE,
	IdActivities		BIGINT 		NOT NULL	REFERENCES directories.Activities(Id),
	DateEvent		DATE 		NOT NULL,
	UNIQUE(IdGrandChildren,IdActivities,DateEvent)
);
ALTER TABLE lists.ActivityGrandChildrens
OWNER TO admin;	

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE lists.GiftGrandChildrens(
	Id 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	IdGrandChildren		BIGINT		NOT NULL	REFERENCES main.GrandChildren(Id) ON UPDATE CASCADE ON DELETE CASCADE,
	NameEvent 		VARCHAR 	NOT NULL,
	NameGifts 		VARCHAR 	NOT NULL,
	Price 			MONEY 		NOT NULL,
	Discount 		MONEY 		NOT NULL,
	DateGift 		DATE 		NOT NULL,
 	UNIQUE(IdGrandChildren,NameEvent,NameGifts,DateGift)
);
ALTER TABLE lists.GiftGrandChildrens
OWNER TO admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE lists.EventFamily(
	Id 			BIGSERIAL 	NOT NULL	PRIMARY KEY,
	IdFamily		BIGINT		NOT NULL	REFERENCES main.Family(Id) ON UPDATE CASCADE ON DELETE CASCADE,
	IdEvent			BIGINT 		NOT NULL	REFERENCES directories.Event(Id),
	Amount			MONEY		NOT NULL,
	Discount 		MONEY 		NOT NULL,
	StartDate		DATE 		NOT NULL,
	EndDate			DATE 		NOT NULL	CHECK(EndDate >= StartDate),
	UNIQUE(IdFamily,IdEvent,StartDate)
);
ALTER TABLE lists.EventFamily
OWNER TO admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE lists.CulturalFamily(
	Id 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	IdFamily		BIGINT		NOT NULL	REFERENCES main.Family(Id) ON UPDATE CASCADE ON DELETE CASCADE,
	IdCultural 		BIGINT 		NOT NULL	REFERENCES directories.Cultural(Id),
	Amount			MONEY		NOT NULL,
	Discount 		MONEY 		NOT NULL,
	DateVisit		DATE 		NOT NULL,
	UNIQUE(IdFamily,IdCultural,DateVisit)
);
ALTER TABLE lists.CulturalFamily
OWNER TO admin;

-----------------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE lists.ActivityFamily(
	Id 			BIGSERIAL 	NOT NULL 	PRIMARY KEY,
	IdFamily		BIGINT		NOT NULL	REFERENCES main.Family(Id) ON UPDATE CASCADE ON DELETE CASCADE,
	IdActivities		BIGINT 		NOT NULL	REFERENCES directories.Activities(Id),
	DateEvent		DATE 		NOT NULL,
	UNIQUE(IdFamily,IdActivities,DateEvent)
);
ALTER TABLE lists.ActivityFamily
OWNER TO admin;