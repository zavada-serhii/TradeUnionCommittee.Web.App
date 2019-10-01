# Trade Union Committee Web App
Information system for the primary trade union organization of the Odessa National Mechnikov University. The application is developed using technologies ASP.NET Core 3.0 MVC &amp; Api, Python/Flask, Docker, ELK Stack, Amazon S3 compatible storage and database management system PostgreSQL 11.

# System architecture
![](https://github.com/zavada-sergey/TradeUnionCommittee.Web.App/blob/master/blob/Architecture.png)

# Road map
- [ ] Data Analysis Service
- [ ] Transfer business logic from stored procedures and triggers
- [ ] Logging
- [ ] Unit Tests

# Docker launch instructions
1. Go to the branch **orchestration** and pull the project.
2. Go to the folder **src** and start the **PowerShell** or **Terminal** as administrator.
3. Run the command **docker-compose -f docker-compose.yml up --build** and wait for the end.
4. After that applications will be available at the following links.
    - Swagger API
        - URL - http://localhost:8510/swagger/index.html
        - Login - stewie.griffin@test.com
        - Password - P@ssw0rd_admin
     - Razor GUI
        - URL - http://localhost:8530
        - Login - stewie.griffin@test.com
        - Password - P@ssw0rd_admin
