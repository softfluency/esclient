@echo off
set appArgs=%*
dotnet run --project .\src\esclient\esclient.csproj -- %appArgs%