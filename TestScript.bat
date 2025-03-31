dotnet new sln

md ExempleApp

md ExempleApp.Tests

cd ExempleApp

dotnet new classlib

cd ..

dotnet sln add ExempleApp

cd ExempleApp.Tests

dotnet new mstest

dotnet add reference ../ExempleApp

cd ..

dotnet sln add ExempleApp.Tests

 