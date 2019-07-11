INSERT INTO public."Hobby" ("Id", "Name") 
SELECT Id, Name FROM maindb.hobby;

INSERT INTO public."Position" ("Id", "Name") 
SELECT Id, Name FROM maindb.position;

INSERT INTO public."Privileges" ("Id", "Name") 
SELECT Id, Name FROM maindb.privileges;

INSERT INTO public."SocialActivity" ("Id", "Name") 
SELECT Id, Name FROM maindb.socialactivity;

INSERT INTO public."AddressPublicHouse"("Id", "City", "Street", "NumberHouse", "NumberDormitory", "Type") VALUES
(1,'Одеса','вул.Єлісаветинська','1','Гуртожиток №1',0),
(2,'Одеса','вул.Новосельського','64','Гуртожиток №2',0),
(3,'Одеса','вул.Довженка','5','Гуртожиток №4',0),
(4,'Одеса','вул.Довженка','5а','Гуртожиток №5',0),
(5,'Одеса','вул.Довженка','9а','Гуртожиток №6',0),
(6,'Одеса','вул.Довженка','9б','Гуртожиток №7',0),
(7,'Одеса','вул.Довенка','9в','Гуртожиток №8',0),
(8,'Одеса','Шампанський провулок','2','Гуртожиток №9',0),
(9,'Одеса','Парк Шевченка','1',NULL,1),
(10,'Одеса','Новосельского','64',NULL,1),
(11,'Одеса','Преображенська','24',NULL,1),
(12,'Одеса','Щепкіна','12',NULL,1),
(13,'Одеса','Щепкіна','2',NULL,1),
(14,'Одеса','Французский Бульвар','48/50',NULL,1),
(15,'Одеса','Парк Шевченка','Не відомо',NULL,1),
(16,'с.Маяки','Не відомо','Не відомо',NULL,1),
(17,'Одеса','Новікова','12','Гуртожиток №12',0),
(18,'Одеса','Комуннальний спуск','7 а',NULL,1),
(19,'Одеса','Скіданівський узїзд','102',NULL,1),
(20,'Одеса','Маразліївська','1а',NULL,1),
(21,'Одеса','Єлісаветинська 12','19',NULL,1);

INSERT INTO public."Employee"("Id", "FirstName", "SecondName", "Patronymic", "Sex", "BirthDate", 
								"IdentificationСode", "MechnikovCard", "MobilePhone", "CityPhone", 
								"BasicProfession", "StartYearWork", "EndYearWork", "StartDateTradeUnion", 
								"EndDateTradeUnion", 
								"LevelEducation", "NameInstitution", "YearReceiving",
								"ScientificDegree", "ScientificTitle",
								"Note", "DateAdded")
SELECT 	e.id,(FullName).FirstName,(FullName).SecondName,(FullName).Patronymic,
		CASE WHEN sex = 'Чоловіча' THEN 'Male' ELSE 'Female' END AS Sex,
		dateofbirth,identificationcode,mechnikovcard,mobilephonenumber,
		cityphonenumber,basicprofession,startdateofworkinonu, 
		enddateofworkinonu, dateofjoiningthetradeunion, 
		dateofnotjoiningthetradeunion, 
		(education).leveleducation, (education).nameinstitution, CAST((education).datereceiving AS INT),
		(scientific).scientificdegree,(scientific).scientifictitle,
		note, dateadded
FROM maindb.employeesuniversity AS e
INNER JOIN maindb.listeducation AS le
ON e.id = le.idemployees
LEFT JOIN maindb.listscientific as ls
ON e.id = ls.idemployees;

INSERT INTO public."Children" ("Id", "IdEmployee", "FirstName", "SecondName", "Patronymic", "BirthDate") 
SELECT id, idemployees,(FullName).FirstName,(FullName).SecondName,(FullName).Patronymic, dateofbirth FROM maindb.listchildren;

INSERT INTO public."GrandChildren" ("Id", "IdEmployee", "FirstName", "SecondName", "Patronymic", "BirthDate") 
SELECT id, idemployees,(FullName).FirstName,(FullName).SecondName,(FullName).Patronymic, dateofbirth FROM maindb.listgrandchildren;

INSERT INTO public."Family" ("Id", "IdEmployee", "FirstName", "SecondName", "Patronymic", "BirthDate") 
SELECT id, idemployees,(FullName).FirstName,(FullName).SecondName,(FullName).Patronymic, dateofbirth FROM maindb.listspouse;

INSERT INTO public."PrivateHouseEmployees" ("Id", "IdEmployee", "City", "Street", "NumberHouse", "NumberApartment", "DateReceiving") 
SELECT id, idemployees,city,street,numberhouse, numberapartment,datereceiving FROM maindb.listprivatehouse;

INSERT INTO public."PublicHouseEmployees" ("IdAddressPublicHouse", "IdEmployee", "NumberRoom") 
SELECT IdAddressPublicHouse, idemployees,NumberRoom FROM maindb.listpublichouse;

INSERT INTO public."PrivilegeEmployees" ("Id", "IdEmployee", "IdPrivileges", "Note", "CheckPrivileges") 
SELECT id, idemployees,nameprivileges, note, CheckPrivileges  FROM maindb.listprivileges;

INSERT INTO public."SocialActivityEmployees" ("Id", "IdEmployee", "IdSocialActivity", "Note", "CheckSocialActivity") 
SELECT id, idemployees,namesocialactivity, note, checksocialactivity  FROM maindb.listsocialactivity;

INSERT INTO public."HobbyEmployees" ("Id", "IdEmployee", "IdHobby") 
SELECT id, idemployees,namehobby  FROM maindb.listhobbyemployees;

INSERT INTO public."HobbyChildrens" ("Id", "IdChildren", "IdHobby") 
SELECT id, IdChildren,namehobby  FROM maindb.listhobbychildren;

INSERT INTO public."HobbyGrandChildrens" ("Id", "IdGrandChildren", "IdHobby") 
SELECT id, IdGrandChildren,namehobby  FROM maindb.listhobbygrandchildren;



INSERT INTO public."Subdivisions"("Id", "IdSubordinate", "Name", "Abbreviation") VALUES
(1,NULL,'Факультет математики, фізики та інформаційних технологій','ФМФІТ'),
(2,NULL,'Факультет історії та філософії','ФІФ'),
(3,NULL,'Факультет журналістики, реклами та видавничої справи','ФЖРВ'),
(4,NULL,'Факультет міжнародних відносин, політології та соціології','ФМВПС'),
(5,NULL,'Факультет романо-германської філології','ФРГФ'),
(6,NULL,'Факультет психології та соціальної роботи','ФПС'),
(7,NULL,'Філологічний факультет','ФФ'),
(8,NULL,'Біологічний факультет','БФ'),
(9,NULL,'Економіко-правовий факультет','ЕПФ'),
(10,NULL,'Геолого-географічний факультет','ГГФ'),
(11,NULL,'Хімічний факультет','ФХ'),
(12,NULL,'Коледж економіки та соціальної роботи','КЕС'),
(13,NULL,'Наукова бібліотека','НБ'),
(14,NULL,'Бухгалтерія','БУХ'),
(15,NULL,'Відділ кадрів','ВК'),
(16,NULL,'Науково дослідна частина','НДЧ'),
(17,NULL,'Адміністративно-господарська частина ','АГЧ'),
(18,NULL,'Пенсіонери','ПЕНС'),
(19,1,'Кафедра математичного забезпечення комп’ютерних систем','КМЗКС'),
(20,1,'Кафедра оптимального керування та економічної кібернетики','КОКЕК'),
(21,1,'Кафедра методів математичної фізики','КММФ'),
(22,1,'Кафедра системного програмного забезпечення та технологій дистанційного навчання','КСПЗТДН'),
(23,1,'Кафедра комп’ютерної алгебри та дискретної математики','ККАДМ'),
(24,1,'Кафедра математичного аналізу','КМА'),
(25,1,'Кафедра вищої математики','КВМ'),
(26,1,'Кафедра геометрії і топології','КГТ'),
(27,1,'Кафедра диференціальних рівнянь','КДР'),
(28,1,'Кафедра обчислювальної математики','КОМ'),
(29,1,'Кафедра теоретичної механіки','КТМ'),
(30,1,'Кафедра експериментальної фізики','КЕФ'),
(31,1,'Кафедра загальної і хімічної фізики','КЗХФ'),
(32,1,'Кафедра теоретичної фізики та астрономії','КТФА'),
(33,1,'Кафедра теплофізіки','КТ'),
(34,2,'Кафедра археології та етнології України','КАЕУ'),
(35,2,'Кафедра історії стародавнього світу та середніх віків','КІСССВ'),
(36,2,'Кафедра історії України','КІУ'),
(37,2,'Кафедра нової та новітньої історії','КННІ'),
(38,2,'Кафедра філософії та методології пізнання','КФМП'),
(39,2,'Кафедра культурології','КК'),
(40,2,'Кафедра філософії та основ загальногуманітарного знання','КФОЗЗ'),
(41,3,'Кафедра видавничої справи та редагування','КВСР'),
(42,3,'Кафедра журналістики','КЖ'),
(43,3,'Кафедра реклами та зв’язків з громадськістю','КРЗГ'),
(44,4,'Кафедра історії та світової політики','КІСП'),
(45,4,'Кафедра міжнародних відносин','КМВ'),
(46,4,'Кафедра політології','КП'),
(47,4,'Кафедра cоціології','КС'),
(48,4,'Кафедра світового господарства і міжнародних економічних відносин','КСГМЕВ'),
(49,5,'Кафедра граматики англійської мови','КГАМ'),
(50,5,'Кафедра лексикології та стилістики англійської мови','КЛСАМ'),
(51,5,'Кафедра теоретичної та прикладної фонетики англійської мови','КТПФАМ'),
(52,5,'Кафедра теорії та практики перекладу','КТПП'),
(53,5,'Кафедра німецької філології','КНФ'),
(54,5,'Кафедра французької філології','КФФ'),
(55,5,'Кафедра іспанської філології','КІФ'),
(56,5,'Кафедра іноземних мов гуманітарних факультетів','КІМГФ'),
(57,5,'Кафедра педагогіки','КПЕД'),
(58,5,'Кафедра зарубіжної літератури','КЗЛ'),
(59,5,'Кафедра іноземних мов природничих факультетів','КІМПФ'),
(60,6,'Кафедра соціальних теорій','КСТ'),
(61,6,'Кафедра загальної психології та психології розвитку особистості','КЗППРО'),
(62,6,'Кафедра кліничної психології','ККП'),
(63,6,'Кафедра диференціальної і спеціальної психології','КДСП'),
(64,6,'Кафедра соціальної і прикладної психології','КСПС'),
(65,6,'Кафедра соціальної допомоги та практичної психології','КСДПП'),
(66,7,'Кафедра української мови','КУМ'),
(67,7,'Кафедра української літератури','КУЛ'),
(68,7,'Кафедра російської мови','КРМ'),
(69,7,'Кафедра світової літератури','КСЛ'),
(70,7,'Кафедра теорії літератури та компаративістики','КТЛК'),
(71,7,'Кафедра загального та слов’янського мовознавства','КЗСМ'),
(72,7,'Кафедра прикладної лінгвістики','КПЛ'),
(73,7,'Кафедра болгарської філології','КБФ'),
(74,8,'Кафедра ботаніки','КБ'),
(75,8,'Кафедра мікробіології, вірусології та біотехнології','КМВБ'),
(76,8,'Кафедра генетики та молекулярної біології','КГМБ'),
(77,8,'Кафедра гідробіології та загальної екології','КГЗЕ'),
(78,8,'Кафедра фізіології людини та тварин','КФЛТ'),
(79,8,'Кафедра зоології','КЗ'),
(80,8,'Кафедра біохімії','КБІО'),
(81,8,'Кафедра медичних знань та безпеки життєдіяльності','КМЗБЖ'),
(82,8,'Гідробіологічна станція','ГС'),
(83,9,'Кафедра адміністративного та господарського права','КАГП'),
(84,9,'Кафедра загальноправових дисциплін та міжнародного права','КЗДМП'),
(85,9,'Кафедра конституційного права та правосуддя','ККАА'),
(86,9,'Кафедра кримінального права, кримінального процесу та криміналістики','ККПКПК'),
(87,9,'Кафедра цивільно-правових дисциплін','КЦПД'),
(88,9,'Кафедра економіки та управління','КЕУ'),
(89,9,'Кафедра бухгалтерського обліку, аналізу та аудиту','КБОАА'),
(90,9,'Кафедра менеджменту та математичного моделювання ринкових процесів','КМММРП'),
(91,9,'Кафедра економічної теорії та історії економічної думки','КЕТІЕД'),
(92,9,'Кафедра економічної кібернетики та інформаційних технологій','КЕКІТ'),
(93,9,'Кафедра економіки та моделювання ринкових відносин','КЕМРВ'),
(94,10,'Кафедра географії України','КГУ'),
(95,10,'Кафедра загальної та морської геології','КЗМГ'),
(96,10,'Кафедра інженерної геології та гідрогеології','КІГГ'),
(97,10,'Кафедра фізичної географії та природокористування','КФГП'),
(98,10,'Кафедра економічної та соціальної географії і туризму','КЕСГТ'),
(99,10,'Кафедра ґрунтознавства та географії ґрунтів','КҐГҐ'),
(100,10,'Кафедра фізичного виховання та спорту','КФВС'),
(101,11,'Кафедра загальної хімії та полімерів','КЗХП'),
(102,11,'Кафедра органічної хімії','КОХ'),
(103,11,'Кафедра неорганічної хімії та хімічної екології','КНХХЕ'),
(104,11,'Кафедра фармацевтичної хімії','КФХ'),
(105,11,'Кафедра аналітичної хімії','КАХ'),
(106,11,'Кафедра фізичної та колоїдної хімії','КФКХ'),
(107,16,'Інститут горіннята не традиційних технологій','ІГТТ'),
(108,13,'Відділ інформаційних технологій','ВІТ'),
(109,16,'Центр високо-технологічного обладнання і приладів спільного користування','ЦВТОПСК'),
(110,6,'Секція іноземних мов факультету психології та соціальної роботи','СІМФПСР'),
(111,6,'Управління факультету психології та соціальної роботи','УФПСР'),
(113,16,'Міжвідомчий науково-навчальний фізико-технічний центр','МННФТЦ'),
(114,NULL,'Міжвідомчий науково-навчальний фізико-технічний центр 2','МННФТЦ_2'),
(115,NULL,'Навчально-науковий Центр високотехнологічного науково-дослідного обладнання та приладів сумісного користування','ННЦВНДОПСК'),
(116,NULL,'Біотехнологічний науково-навчальний центр','БННЦ'),
(117,8,'Деканат біологічного факультету','ДБФ'),
(118,NULL,'Науковий центр моніторингу','НЦМ'),
(119,8,'База-стоянка маломірних плавзасобів','БСМП'),
(120,8,'Лабораторія інформаційних технологій та технічних засобівнавчання','ЛІТТЗ'),
(121,8,'Лабораторія фізико-хімічних методів дослідження','ЛФХМД'),
(122,8,'Зоомузей','ЗООМ'),
(123,8,'Їдальня','Ї'),
(125,10,'Геологічний музей','ГМ'),
(126,NULL,'Віділення довузовської підготовки','ВДП'),
(127,6,'Кафедра соціальної роботи','КСР'),
(128,4,'Деканат факультету міжнародних відносин, політології та соціології','ДФМВПС'),
(129,NULL,'Інститут міжнародної освіти','ІІО'),
(130,129,'Кафедра мовної та  загальногуманітарної підготовки іноземців','КМЗПІ'),
(131,NULL,'Студентське містечко','СМ'),
(132,131,'Гуртожиток №2','Г№2'),
(133,131,'Гуртожиток №7','Г№7'),
(134,131,'Гуртожиток №6','Г№6');

INSERT INTO public."PositionEmployees" ("Id", "IdEmployee", "IdSubdivision", "IdPosition", "CheckPosition", "StartDate", "EndDate") 
SELECT Id,IdEmployees,nameSubdivision,namePosition,CheckPosition,StartDatePosition,EndDatePosition FROM maindb.listpositionsubdivision;

--------------------------------------------------

SELECT setval('"Hobby_Id_seq"', (SELECT last_value FROM maindb.hobby_id_seq), TRUE);
SELECT setval('"Position_Id_seq"', (SELECT last_value FROM maindb.position_id_seq), TRUE);
SELECT setval('"Privileges_Id_seq"', (SELECT last_value FROM maindb.privileges_id_seq), TRUE);
SELECT setval('"SocialActivity_Id_seq"', (SELECT last_value FROM maindb.socialactivity_id_seq), TRUE);
SELECT setval('"AddressPublicHouse_Id_seq"', (SELECT last_value FROM maindb.addresspublichouse_id_seq), TRUE);
SELECT setval('"Employee_Id_seq"', (SELECT last_value FROM maindb.employeesuniversity_id_seq), TRUE);
SELECT setval('"Children_Id_seq"', (SELECT last_value FROM maindb.listchildren_id_seq), TRUE);
SELECT setval('"GrandChildren_Id_seq"', (SELECT last_value FROM maindb.listgrandchildren_id_seq), TRUE);
SELECT setval('"Family_Id_seq"', (SELECT last_value FROM maindb.listspouse_id_seq), TRUE);
SELECT setval('"PrivateHouseEmployees_Id_seq"', (SELECT last_value FROM maindb.listprivatehouse_id_seq), TRUE);
SELECT setval('"PrivilegeEmployees_Id_seq"', (SELECT last_value FROM maindb.listprivileges_id_seq), TRUE);
SELECT setval('"SocialActivityEmployees_Id_seq"', (SELECT last_value FROM maindb.listsocialactivity_id_seq), TRUE);
SELECT setval('"HobbyEmployees_Id_seq"', (SELECT last_value FROM maindb.listhobbyemployees_id_seq), TRUE);
SELECT setval('"HobbyChildrens_Id_seq"', (SELECT last_value FROM maindb.listhobbychildren_id_seq), TRUE);
SELECT setval('"HobbyGrandChildrens_Id_seq"', (SELECT last_value FROM maindb.listhobbygrandchildren_id_seq), TRUE);
SELECT setval('"Subdivisions_Id_seq"', (SELECT last_value FROM maindb.structuralsubdivision_deptkod_seq), TRUE);
SELECT setval('"PositionEmployees_Id_seq"', (SELECT last_value FROM maindb.listpositionsubdivision_id_seq), TRUE);