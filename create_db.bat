ECHO off

sqlcmd -S localhost -E -i DataDictionary.sql

rem server is localhost

ECHO.
ECHO if no errors appear DB was created
PAUSE

