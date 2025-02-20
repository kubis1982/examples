namespace OData.Persistance
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.OData;
    using OData.Persistance.Entities;

    public class WriteDbContext(DbContextOptions<WriteDbContext> options) : DbContext(options)
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentItem> DocumentItems { get; set; }
    }
}
