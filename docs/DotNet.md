To create a new Web API project:
dotnet new webapi --use-controllers -n MyApiProject

To create a new class library project:
dotnet new classlib -n MyLibrary

To add it to the existing solution:
dotnet sln add TextTransform/TextTransform.csproj

To reference it from the API project:
dotnet add NeedToDo.APIs/NeedToDo.APIs.csproj reference TextTransform/TextTransform.csproj