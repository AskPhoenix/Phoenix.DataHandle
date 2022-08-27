Rem Pack script for Windows Console

@echo off

echo *** NuGet Pack script ***
echo.

echo Removing old packages...
del ".\bin\Release\*.nupkg"

echo Installing dependencies...
dotnet restore

echo Building Release...
dotnet build --configuration Release --no-restore

echo Packing Release...
dotnet pack --configuration Release --no-restore

echo Pushing package...
dotnet nuget push "**/*.nupkg" --source "AskPhoenix" --skip-duplicate

pause