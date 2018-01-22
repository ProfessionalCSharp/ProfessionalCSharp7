@echo off
set size=0
call func settings list -data > %temp%\settings-list
call :filesize %temp%\settings-list
if NOT %size% == 0 goto show
@echo ----------------------------------------------------------------------
@echo To fetch your bot service settings run the following command:
@echo     func azure functionapp fetch-app-settings [YOUR_BOT_SERVICE_NAME]
@echo ----------------------------------------------------------------------
goto start

:show
type %temp%\settings-list
erase %temp%\settings-list 

:start
@func host start -p 3978 
goto :eof

:filesize
  set size=%~z1
  exit /b 0

