@echo off
echo ===================================================
echo Building All Parking System Projects
echo ===================================================
echo.

echo Checking for and closing running applications...
taskkill /f /im ParkingIN.exe 2>nul
taskkill /f /im ParkingOUT.exe 2>nul
taskkill /f /im ParkingServer.exe 2>nul
taskkill /f /im dotnet.exe /fi "WINDOWTITLE eq ParkingIN" 2>nul
taskkill /f /im dotnet.exe /fi "WINDOWTITLE eq ParkingOUT" 2>nul
taskkill /f /im dotnet.exe /fi "WINDOWTITLE eq ParkingServer" 2>nul
timeout /t 2 /nobreak > nul
echo All running applications closed.
echo.

echo [1/3] Building ParkingIN...
cd /d D:\21maret\clean_code\ParkingIN
dotnet build --configuration Release
if %ERRORLEVEL% NEQ 0 (
  echo Error: ParkingIN build failed!
  goto :error
)
echo ParkingIN build successful!
echo.

echo [2/3] Building ParkingOUT...
cd /d D:\21maret\clean_code\ParkingOUT
dotnet build ParkingOUT.csproj --configuration Release
if %ERRORLEVEL% NEQ 0 (
  echo Error: ParkingOUT build failed!
  goto :error
)
echo ParkingOUT build successful!
echo.

echo [3/3] Building ParkingServer...
cd /d D:\21maret\clean_code\ParkingServer
dotnet build --configuration Release
if %ERRORLEVEL% NEQ 0 (
  echo Error: ParkingServer build failed!
  goto :error
)
echo ParkingServer build successful!
echo.

echo ===================================================
echo All projects built successfully!
echo ===================================================
goto :end

:error
echo.
echo ===================================================
echo Build process failed! See errors above.
echo ===================================================
exit /b 1

:end
pause