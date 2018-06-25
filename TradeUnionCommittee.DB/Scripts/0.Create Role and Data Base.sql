CREATE ROLE AdminTradeUnionCommitteeEmployees LOGIN PASSWORD 'admin'				
NOSUPERUSER NOINHERIT NOCREATEDB NOCREATEROLE NOREPLICATION;

CREATE DATABASE "TradeUnionCommitteeEmployeesCore"
WITH OWNER = AdminTradeUnionCommitteeEmployees
ENCODING = 'UTF8'
TABLESPACE = pg_default
LC_COLLATE = 'English_United States.1252'
LC_CTYPE = 'English_United States.1252'
CONNECTION LIMIT = -1;