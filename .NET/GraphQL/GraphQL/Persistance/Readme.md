# -------- Przydatne dla EF Core  --------

## Utworzenie nowej migracji
dotnet ef migrations add AddGroups --context WriteDbContext --output-dir .\Persistance\Migrations --project .\GraphQL\GraphQL.csproj --startup-project .\GraphQL\GraphQL.csproj
