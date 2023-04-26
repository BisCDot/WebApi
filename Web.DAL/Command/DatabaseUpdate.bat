@echo off
cd ..
cd ..
dotnet ef --startup-project Web.NetCore/Web.NetCore.csproj -p Web.DAL/Web.DAL.csproj database update --context MainDbContext
pause