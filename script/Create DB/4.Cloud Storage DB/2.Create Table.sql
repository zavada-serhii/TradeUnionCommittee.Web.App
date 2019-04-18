CREATE TABLE "PdfBucket"(
    "Id"            BIGSERIAL 	NOT NULL 	PRIMARY KEY,
    "IdEmployee"    BIGINT      NOT NULL,
    "FileName"      VARCHAR     NOT NULL,
    "DateCreated"   TimeStamp   NOT NULL,
    "EmailUser"     VARCHAR     NOT NULL,
    "IpUser"	    CIDR        NOT NULL,
    "TypeReport"    INT         NOT NULL,
    "DateFrom"      DATE        NOT NULL,
    "DateTo"        DATE        NOT NULL
);
ALTER TABLE "PdfBucket"
OWNER TO postgres;