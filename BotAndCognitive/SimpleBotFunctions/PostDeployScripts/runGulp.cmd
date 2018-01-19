@echo off
setlocal

rem enumerate each folder under root and check for existence of gulpfile.cs
rem if gulpfile exists, run the default gulp task
for /d %%d in (..\wwwroot\*) do (  
  echo check %%d
  pushd %%d
  if exist package.json (
    echo npm install --production
    call npm install --production
  ) else (
    echo no package.json found    
  )
    
  if exist gulpfile.js (
    echo run gulp
    gulp
  ) else (
    echo no gulpfile.js found
  )
  popd 
)

echo record deployment timestamp
date /t >> ..\deployment.log
time /t >> ..\deployment.log
echo ---------------------- >> ..\deployment.log
echo Deployment done

