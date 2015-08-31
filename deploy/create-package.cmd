@echo off

:: When running as admin, the current path generally defaults to System32, not
:: where the CMD file was run from, so change directory...
cd /d %~dp0

echo info: Create NuGet package

nuget.exe pack .\Cerulean.Consul.nuspec
