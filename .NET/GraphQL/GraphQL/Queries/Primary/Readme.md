# -------- Przydatne dla EF Core  --------

## Utworzenie nowej migracji
dotnet ef migrations add RemoveGroups --context PostgresDbContext --output-dir .\Queries\Primary\Persistance\Migrations --project .\GraphQL\GraphQL.csproj --startup-project .\GraphQL\GraphQL.csproj
