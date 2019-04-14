
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

---------------------------------------------------------------------------------------------------------------------

--GIST
SELECT  e."Id", public."TrigramFullName"(e) <-> 'Enter the name you are looking for!!!' AS "ResultIds"
FROM public."Employee" AS e
ORDER BY "ResultIds" ASC LIMIT 10;

--GIN
SELECT e."Id", similarity(public."TrigramFullName"(e), 'Enter the name you are looking for!!!' ) AS "ResultIds"
FROM public."Employee" AS e
WHERE TRUE AND public."TrigramFullName"(e) % 'Enter the name you are looking for!!!'
ORDER BY "ResultIds" DESC LIMIT 10;

---------------------------------------------------------------------------------------------------------------------