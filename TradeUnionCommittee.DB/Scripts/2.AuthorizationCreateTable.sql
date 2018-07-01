CREATE TABLE "Roles"(
	"Id" 			    BIGSERIAL 	NOT NULL 	PRIMARY KEY,
    "Name"              VARCHAR     NOT NULL    UNIQUE
);
ALTER TABLE "Roles"
OWNER TO AdminTradeUnionCommitteeEmployees;

CREATE TABLE "Users"(
	"Id" 			    BIGSERIAL 	NOT NULL 	PRIMARY KEY,
    "IdRole"            BIGINT      NOT NULL    REFERENCES "Roles"("Id") ON UPDATE CASCADE ON DELETE CASCADE,
	"Email" 		    VARCHAR 	NOT NULL    CHECK ("Email" ~ '^[-._a-z0-9]+@(?:[a-z0-9][-a-z0-9]+\.)+[a-z]{2,6}$'::TEXT),
	"Password" 	        TEXT		NOT NULL,
	UNIQUE("Email")
);
ALTER TABLE "Users"
OWNER TO AdminTradeUnionCommitteeEmployees;