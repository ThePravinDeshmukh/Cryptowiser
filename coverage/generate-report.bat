#!/bin/bash

cd ..\src

ECHO "Deleting old coverage files"
del /S /F /Q coverage.cobertura.xml

ECHO "Run dotnet test and collect coverage"
dotnet test --collect:"XPlat Code Coverage"

ECHO "Try install donet reportgenerator"
dotnet tool install -g dotnet-reportgenerator-globaltool

ECHO "Generate Html Coverage Report"
reportgenerator "-reports:..\src\*\*\*\coverage.cobertura.xml" "-targetdir:..\coverage\coveragereport" -reporttypes:Html

ECHO "Open Html Coverage Report"
START ..\coverage\coveragereport\index.html

pause..