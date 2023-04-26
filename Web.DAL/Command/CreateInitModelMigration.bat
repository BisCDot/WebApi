@echo off
cd ..
cd ..
dotnet ef --startup-project Web.NetCore/Web.NetCore.csproj migrations add InitialModel -c MainDbContext -p Web.DAL/Web.DAL.csproj --verbose -o Migrations/Core
pause