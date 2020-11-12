@echo off

echo If you haven't authenticated GitHub stream, then use the command below:
echo dotnet nuget add source https://nuget.pkg.github.com/AskPhoenix/index.json -n AskPhoenix -u AskPhoenix -p GH_TOKEN
echo.
echo.

echo Installing dependencies
echo.

dotnet restore

echo Packing for Release configuration...
echo.

dotnet pack --configuration Release --no-restore

echo.
echo.
nuget.exe push "**/*.nupkg" -Source "AskPhoenix" -SkipDuplicate

Pause