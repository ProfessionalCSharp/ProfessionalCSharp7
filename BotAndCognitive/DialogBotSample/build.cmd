@echo off
setlocal

set DEPLOYMENT_SOURCE=
set IN_PLACE_DEPLOYMENT=1

if exist ..\wwwroot\deploy.cmd (
  pushd ..\wwwroot
  call deploy.cmd
  popd
)

endlocal

