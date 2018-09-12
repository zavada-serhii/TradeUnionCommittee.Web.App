CREATE TABLE "Roles"(
	"Id" 			    BIGSERIAL 	NOT NULL 	PRIMARY KEY,
    "Name"              VARCHAR     NOT NULL    UNIQUE
);
ALTER TABLE "Roles"
OWNER TO postgres;

CREATE TABLE "Users"(
	"Id" 			    BIGSERIAL 	NOT NULL 	PRIMARY KEY,
    "IdRole"            BIGINT      NOT NULL    REFERENCES "Roles"("Id") ON UPDATE CASCADE ON DELETE CASCADE,
	"Email" 		    VARCHAR 	NOT NULL    CHECK ("Email" ~ '^[-._a-z0-9]+@(?:[a-z0-9][-a-z0-9]+\.)+[a-z]{2,6}$'::TEXT),
	"Password" 	        TEXT		NOT NULL,
	UNIQUE("Email")
);
ALTER TABLE "Users"
OWNER TO postgres;

INSERT INTO "Roles" ("Name") VALUES
('Admin'),
('Accountant'),
('Deputy');

INSERT INTO "Users" ("IdRole","Email","Password") VALUES
(1,'stewie.griffin@test.com','ACbdDpAP6Dwu1djzNCN2CpcP9U9Cctq3AalWMA6qipuF7Qf3SjNnXzeCZ1KJZj0aKQ=='),
(2,'accountant@test.com','ACWKqqqrtM65KHIxy3LAVuq92NUgjnhfu7BBcn6sAVZhKagfU+5kzTq+c6CFA9M46w=='),
(3,'deputy@test.com','AFwdvz+/flWC6PP+AjwcsZeH+Oos7iKras6k0Qq+48srGTI/z5oUHe5+k3SDFkfjHg==');