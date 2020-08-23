@echo off

echo If you haven't authenticated GitHub stream, then use the command below:
echo dotnet nuget add source https://nuget.pkg.github.com/AskPhoenix/index.json -n AskPhoenix -u AskPhoenix -p GH_TOKEN
echo.
echo.

echo Packing for Release configuration...
echo.

dotnet pack --configuration Release

echo.
echo.
set /p p="Enter version for package to publish: "
dotnet nuget push "bin/Release/Phoenix.DataHandle.%p%.nupkg" --source "github"

Pause