# Initialize the app:

```
dotnet new console -o QLClient 
# create console app

cd QLClient 
# go to the project folder

curl http://localhost:10000/graphql?sdl > schema.graphql 
# fetch graphql schema from server

dotnet new tool-manifest 
# create manifest file to track NuGet tools

dotnet tool install ZeroQL.CLI 
# add ZeroQL.CLI NuGet tool

dotnet add package ZeroQL 
# add ZeroQL NuGet package

dotnet zeroql generate --schema .\schema.graphql --namespace TestServer.Client --client-name TestServerGraphQLClient --output Generated/GraphQL.g.cs 
# generate wrappers from the schema.graphql
```
