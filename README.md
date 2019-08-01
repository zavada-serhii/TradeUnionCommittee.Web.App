# Trade Union Committee App
Information system for the primary trade union organization of the Odessa National Mechnikov University. The application is developed using technologies ASP.NET Core 2.2 MVC &amp; Api, Python/Flask, Docker, Elasticsearch, Kibana, HTML, CSS, JS and database management system PostgreSQL 11.

# System architecture
![](https://github.com/zavada-sergey/TradeUnionCommittee.WebApp.Core/blob/master/blob/Architecture.png)

# Road map
- [ ] Data Analysis Service
- [ ] Transfer business logic from stored procedures
- [ ] Logging
- [ ] Unit Tests

# Docker launch instructions
1. Go to the branch **orchestration** and pull the project.
2. Go to the folder **src** and start the **PowerShell** or **Terminal** as administrator.
3. Run the command **docker-compose -f docker-compose.yml up --build** and wait for the end.
4. After that applications will be available at the following links.
    - Site
        - URL - http://localhost:8580
        - Login - stewie.griffin@test.com
        - Password - P@ssw0rd_admin
    - API for this site
        - URL - http://localhost:8590/swagger
        - Login - stewie.griffin@test.com
        - Password - P@ssw0rd_admin
    - PostgreSQL
        - Host name/address - http://localhost:8600
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
