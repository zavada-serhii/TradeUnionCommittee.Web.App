# Trade Union Committee Web App
Information system for the primary trade union organization of the Odessa National Mechnikov University. The application is developed using technologies ASP.NET Core 2.2 MVC &amp; Api, Docker, Elasticsearch, Kibana, Entity Framework Core, HTML, CSS, JS and database management system PostgreSQL 11.

# System architecture
![](https://github.com/zavada-sergey/TradeUnionCommittee.WebApp.Core/blob/master/blob/System%20architecture.png)

# Road map
- [ ] Transfer business logic from stored procedures
- [ ] Data Analysis API && Data Analysis Service
- [ ] Logging
- [ ] Unit Tests

# Docker launch instructions
1. Go to the project root and start the PowerShell or Terminal as administrator.
2. Run the command **docker-compose -f docker-compose.yml up --build** and wait for the end.
3. After that applications will be available at the following links.
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
    - MinIO
        - URL - http://localhost:9001-9004
        - Login - minio
        - Password - minio123