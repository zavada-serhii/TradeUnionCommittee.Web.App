# Trade Union Committee Web App
Information system for the primary trade union organization of the Odessa National Mechnikov University. The application is developed using technologies ReactJS with Redux, ASP.NET Core 5.0 MVC &amp; Api, Python/Flask, ELK Stack, Amazon S3 compatible storage (MinIO) and database management system PostgreSQL 12.

# System architecture
![](https://github.com/zavada-sergey/TradeUnionCommittee.Web.App/blob/master/blob/Architecture.png)

# Road map
- [ ] Transfer business logic from stored procedures and triggers
- [ ] Logging
- [ ] Unit Tests

# Docker - build images and run containers.
1. Go to the folder **src**.
2. If you are using **Windows OS**, execute **docker_deploy.bat** script, if you are using **Linux/macOS OS**, execute **docker_deploy.sh** script.
3. After that applications will be available at the following links.
    - React WEB GUI - 1% completed.
        - URL - http://localhost:8500
        - Login - stewie.griffin@test.com
        - Password - P@ssw0rd_admin
    - Swagger WEB API - 10% completed.
        - URL - http://localhost:8510/swagger/index.html
        - Login - stewie.griffin@test.com
        - Password - P@ssw0rd_admin
    - Data Analysis API - 100% completed.
        - URL - http://localhost:8520
    - Razor WEB GUI - 100% completed. (Obsolete).
        - URL - http://localhost:8530
        - Login - stewie.griffin@test.com
        - Password - P@ssw0rd_admin
    - PostgreSQL 12
        - URL - tcp://localhost:5432
        - Login - postgres
        - Password - P@ssw0rd
    - pgAdmin4
        - URL - http://localhost:15432
        - Login - postgres@sandbox.com
        - Password - postgres
    - Elasticsearch
        - URL - http://localhost:9200
    - Kibana
        - URL - http://localhost:5601
    - MinIO
        - URL - http://localhost:9000
        - Access Key - minio
        - Secret Key - minio123
    - HAProxy
        - URL - http://localhost:8080
        - Login - haproxy
        - Password - haproxy
