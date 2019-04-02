# Trade Union Committee Web App
Information system for the primary trade union organization of the Odessa National Mechnikov University. The application is developed using technologies ASP.NET Core 2.2 MVC &amp; Api, Docker, Elasticsearch, Kibana, Entity Framework Core, HTML, CSS, JS and database management system PostgreSQL 11.

# System architecture
![](https://github.com/zavada-sergey/TradeUnionCommittee.WebApp.Core/blob/master/blob/Architecture.png)

# Road map
- [ ] Transfer business logic from stored procedures
- [ ] Data Analysis API && Data Analysis Service
- [ ] Logging
- [ ] Unit Tests

# Docker launch instructions
1. Go to the project root and start the PowerShell or Terminal as administrator.
2. Run the command **docker-compose -f docker-compose.yml up --build** and wait for the end.
3. After that, connect to **PostgreSQL** 
    - Host name/address - http://localhost:9595
    - Login - postgres 
    - Password - postgres.
4. Create an empty database named **TradeUnionCommitteeEmployeesCore** and then run the script that is located on this
[link](https://github.com/zavada-sergey/TradeUnionCommittee.WebApp.Core/blob/master/TradeUnionCommittee.DB/Scripts/0.FullScript.sql).
5. After that applications will be available at the following links.
    - Site
        - URL - http://localhost:9090
        - Login - stewie.griffin@test.com
        - Password - P@ssw0rd_admin
    - API for this site
        - URL - http://localhost:9092/swagger
        - Login - stewie.griffin@test.com
        - Password - P@ssw0rd_admin
    - PostgreSQL
        - Host name/address - http://localhost:9595
        - Login - postgres 
        - Password - postgres.
    - Kibana 
        - URL - http://localhost:5601
    - ElasticSearch
        - URL - http://localhost:9200