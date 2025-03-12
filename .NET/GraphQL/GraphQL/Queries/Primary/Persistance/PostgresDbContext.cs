namespace GraphQL.Queries.Primary.Persistance
{
    using GraphQL.Queries.Primary.Persistance.Entities;
    using Microsoft.EntityFrameworkCore;

    public class PostgresDbContext(DbContextOptions<PostgresDbContext> options) : DbContext(options)
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentItem> DocumentItems { get; set; }

        public DbSet<Group> Groups { get; set; }
    }
}
