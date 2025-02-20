namespace GraphQL
{
    using GraphQL.Persistance;
    using GraphQL.Persistance.Entities;
    using HotChocolate.Data;
    using Microsoft.EntityFrameworkCore;

    public class Query
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Document> GetDocuments([Service] WriteDbContext writeDbContext) => writeDbContext.Documents.AsNoTracking();
    }
}
