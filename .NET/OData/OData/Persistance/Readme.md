# -------- Przydatne dla EF Core  --------

## Utworzenie nowej migracji
dotnet ef migrations add InitialDb --context WriteDbContext --output-dir .\Persistance\Migrations --project .\OData\OData.csproj --startup-project .\OData\OData.csproj
## Aktualizacja bazy danych
dotnet ef database update --context WriteDbContext --project .\OData\OData.csproj --startup-project .\OData\OData.csproj
## Usunięcie ostatniej migracji
dotnet ef migrations remove --context WriteDbContext --project .\OData\OData.csproj --startup-project .\OData\OData.csproj

