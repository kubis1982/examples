namespace OData.Persistance
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.EntityFrameworkCore.Diagnostics;

    public class WriteDbContextFactory : IDesignTimeDbContextFactory<WriteDbContext>
    {
        public WriteDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<WriteDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseNpgsql("User ID=postgres;Password=mypassword;Host=localhost;Port=5432;Database=OData", n => {
                n.MigrationsHistoryTable("MigrationHistory", "OData");
            });
            dbContextOptionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
            return new WriteDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
