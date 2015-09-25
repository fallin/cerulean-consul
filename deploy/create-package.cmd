@echo off

:: When running as admin, the current path generally defaults to System32, not
:: where the CMD file was run from, so change directory...
cd /d %~dp0

echo info: Build cerulean-consul
call "%VS120COMNTOOLS%\vsvars32.bat"
msbuild ..\cerulean-consul.sln /t:Rebuild /p:Configuration=Release
if ERRORLEVEL 1 (
    echo error: Failed to build solution
    goto ERROR
)

echo info: Create NuGet package
nuget.exe pack .\Cerulean.Consul.nuspec
if ERRORLEVEL 1 (
    echo error: Failed to create nuget package
    goto ERROR
)

goto END

:ERROR
pause

:END
echo info: Complete
