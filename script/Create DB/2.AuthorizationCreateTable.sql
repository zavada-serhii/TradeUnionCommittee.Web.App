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