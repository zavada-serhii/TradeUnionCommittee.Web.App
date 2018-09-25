
---------------------------------------------------------------------------------------------------------------------

CREATE FUNCTION public."TrigramFullName"(p public."Employee")
RETURNS TEXT
LANGUAGE plpgsql
IMMUTABLE
AS $$
BEGIN

RETURN lower(trim(coalesce(p."FirstName",'') || ' ' ||coalesce(p."SecondName",'') || ' ' ||coalesce(p."Patronymic",'')));

EXCEPTION WHEN others THEN RAISE EXCEPTION '%', sqlerrm; END; $$;
ALTER FUNCTION public."TrigramFullName"(p public."Employee")
OWNER TO postgres;

---------------------------------------------------------------------------------------------------------------------

CREATE INDEX info_gist_idx ON public."Employee"
USING gist(public."TrigramFullName"("Employee") gist_trgm_ops);

---------------------------------------------------------------------------------------------------------------------

SELECT  e."Id", public."TrigramFullName"(e) <-> '' AS "ResultIds"
FROM public."Employee" AS e
ORDER BY "ResultIds" ASC LIMIT 10;

---------------------------------------------------------------------------------------------------------------------