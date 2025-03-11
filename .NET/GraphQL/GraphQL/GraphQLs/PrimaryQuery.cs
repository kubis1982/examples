namespace GraphQL.GraphQLs
{
    using GraphQL.Persistance;
    using GraphQL.Persistance.Entities;
    using HotChocolate.Authorization;
    using HotChocolate.Data;
    using HotChocolate.Resolvers;
    using Microsoft.EntityFrameworkCore;

    [Authorize]
    public class PrimaryQuery
    {

        [UsePaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [Authorize(Roles = new[] { "Admin" })]
        public IQueryable<Document> GetDocuments([Service] WriteDbContext writeDbContext) => writeDbContext.Documents.AsNoTracking();

        [Authorize(Roles = new[] { "User" })]
        [UsePaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Article> GetArticles([Service] WriteDbContext writeDbContext) => writeDbContext.Articles.AsNoTracking();

        [UsePaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<IReadOnlyList<Document>> GetDocuments2(IResolverContext context, [Service] WriteDbContext writeDbContext)
        {
            var query = writeDbContext.Documents.AsNoTracking().Project(context).AsNoTracking();
            return await query.ToListAsync();
        }

        [UsePaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Contractor> GetContractors([Service] WriteDbContext writeDbContext) => writeDbContext.Contractors.AsNoTracking();
    }
}
