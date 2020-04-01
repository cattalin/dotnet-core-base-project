
This is a base setup of a .NET Core WebApi Backend. 
It contains infrastructure logic, basic authentication, basic architecture and structure of classes/files and many useful utils

Here are the steps required to create an run a new Database Migration in the existing folders structure
$env:ASPNETCORE_ENVIRONMENT='Development'

Add-Migration 'Test' -Project Core -StartupProject Core -Context DatabaseContext

Update-Database -Project Core -StartupProject Core -Context DatabaseContext
