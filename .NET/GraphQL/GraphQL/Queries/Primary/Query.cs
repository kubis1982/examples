namespace GraphQL.Queries.Primary
{
    using GraphQL.Queries.Primary.Persistance;
    using GraphQL.Queries.Primary.Persistance.Entities;
    using HotChocolate.Authorization;
    using HotChocolate.Data;
    using HotChocolate.Resolvers;
    using Microsoft.EntityFrameworkCore;

    [Authorize]
    public class Query
    {

        [UsePaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [Authorize(Roles = new[] { "Admin" })]
        public IQueryable<Document> GetDocuments([Service] PostgresDbContext writeDbContext) => writeDbContext.Documents.AsNoTracking();

        [Authorize(Roles = new[] { "User" })]
        [UsePaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Article> GetArticles([Service] PostgresDbContext writeDbContext) => writeDbContext.Articles.AsNoTracking();

        [UsePaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<IReadOnlyList<Document>> GetDocuments2(IResolverContext context, [Service] PostgresDbContext writeDbContext)
        {
            var query = writeDbContext.Documents.AsNoTracking().Project(context).AsNoTracking();
            return await query.ToListAsync();
        }

        [UsePaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Contractor> GetContractors([Service] PostgresDbContext writeDbContext) => writeDbContext.Contractors.AsNoTracking();
    }
}
