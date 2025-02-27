namespace GraphQL
{
    using GraphQL.Persistance;
    using GraphQL.Persistance.Entities;
    using HotChocolate.Authorization;
    using HotChocolate.Data;
    using Microsoft.EntityFrameworkCore;

    public class Query
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [Authorize(Roles = new [] { "Admin" })]
        public IQueryable<Document> GetDocuments([Service] WriteDbContext writeDbContext) => writeDbContext.Documents.AsNoTracking();

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [Authorize(Roles = new[] { "User" })]
        public IQueryable<Article> GetArticles([Service] WriteDbContext writeDbContext) => writeDbContext.Articles.AsNoTracking();

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Contractor> GetContractors([Service] WriteDbContext writeDbContext) => writeDbContext.Contractors.AsNoTracking();
    }
}
