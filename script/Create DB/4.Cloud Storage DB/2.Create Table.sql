CREATE TABLE "ReportPdfBucket"(
    "Id"            BIGSERIAL 	NOT NULL 	PRIMARY KEY,
    "IdEmployee"    BIGINT      NOT NULL,
    "FileName"      VARCHAR     NOT NULL,
    "DateCreated"   TimeStamp   NOT NULL,
    "EmailUser"     VARCHAR     NOT NULL,
    "IpUser"	    VARCHAR     NOT NULL,
    "TypeReport"    INT         NOT NULL,
    "DateFrom"      DATE        NOT NULL,
    "DateTo"        DATE        NOT NULL
);
ALTER TABLE "ReportPdfBucket"
OWNER TO postgres;