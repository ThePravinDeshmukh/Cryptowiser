
# Cryptowiser

<p align="left">
  <a href="https://github.com/ThePravinDeshmukh/cryptowiser"><img alt="GitHub Actions status" src="https://github.com/ThePravinDeshmukh/cryptowiser/workflows/.NET/badge.svg"></a>
</p>

The project has an ASP.NET Core app and a React app. The ASP.NET Core app is intended to be used for data access, authorization, and other server-side concerns. The React app, residing in the ../src/Cryptowiser/ClientApp subdirectory, is intended to be used for all UI concerns.

# Quick Start - How to Run

You can use either the latest version of Visual Studio or simply Docker CLI and .NET CLI for Windows, Mac and Linux.

## Windows 
Prerequisite

	Required - Dotnet Core 5 SDK  https://dotnet.microsoft.com/download/dotnet/5
	Optional - Visual Studio 2019  https://visualstudio.microsoft.com/vs/
Run
```run\run-on-windows.bat```

Browse to
  http://localhost:5000/

![](img/run-via-dotnet-cli.gif)

## Docker

    Required - Docker Engine Installed https://docs.docker.com/engine/install/

Run
```run\run-on-docker.bat```

Browse to
  http://localhost:5000/

![](img/run-on-docker.gif)

## Planning & Documentation
Confluence - https://techfactor.atlassian.net/l/c/EK5xW6qT

## Architecture Overview
This application is cross-platform at the server and client side, thanks to .NET 5 services capable of running on Linux or Windows containers depending on your Docker host. The architecture proposes a microservice oriented architecture using Http as the communication protocol between the client apps and the microservices.

![](img/architecture-overview.png)


## Authentication Overview
JWT authentication to validate API Requests.

![](img/authentication-overview.png)


## Tech-Stack

| Component        	| Attribute					| Value | MoSCoW  	| Status  | Reference |
| ------------- 	|-------------		| -------------	| -------------	| ------------- |-------------|
| Web API | Repository 					| GitHub | Must have | Done | https://github.com/ThePravinDeshmukh/cryptowiser |
|  | Framework 							| dotnet 5 | Must have | Done | https://dotnet.microsoft.com/download/dotnet/5.0 |
|  | Authentication 					| JWT Token based Auth | Must have | Done | https://docs.microsoft.com/en-us/ef/core/ |
|  | API Documentation & Testing 		| Swagger | Must have | Done | https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-5.0 |
|  | Logging 							| Serilog | Must have | Done | https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-5.0 |
|  | Tests 								| XUnit with Moq | Must have | Not Started | EF Core Testing https://docs.microsoft.com/en-us/ef/core/testing/ |
|  | Code Coverage 						| Cobertura | Must have | Done | https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-code-coverage?tabs=windows |
|  | Code Analysis 						| EnableNETAnalyzers, EnforceCodeStyleInBuild | Must have | Done | https://docs.microsoft.com/en-us/dotnet/fundamentals/code-analysis/overview#code-quality-analysis |
|  | CI | GitHub Actions 				| Should have | Not Started | https://github.com/ThePravinDeshmukh/cryptowiser/actions |
|  | Rate Limit 						| AspNetCoreRateLimit Middleware | Should have | Not Started | https://github.com/stefanprodan/AspNetCoreRateLimit |
|  | Service Discovery 					| Eureka | Could have | Not Started | https://steeltoe.io/service-discovery/get-started/eureka |
| Front End			 					| Framework | React | Must have  | Done | https://docs.microsoft.com/en-us/ef/core/providers/in-memory/?tabs=dotnet-core-cli |
| Internal Database 					| SqlLite | Demonstration | Must have  | Done | https://docs.microsoft.com/en-us/ef/core/providers/in-memory/?tabs=dotnet-core-cli |
| Deployment and Runtime | Container 	| Docker | Must have | Done | Run ```run\via-docker.bat``` |
|  | Windows | Dotnet core 5 runtime 	| Must have | Done | Run ```run\via-dotnet-cli.bat``` |



# How to Generate Coverage Report

You can generate coverage report for solution and view in html

![](img/coverage-report.JPG)

Run
```coverage\generate-report.bat```

This will 

    Run Tests in solution
    Generate coverage report in html
    Open report in browser
	
![](img/generate-coverage-report.gif)

 
