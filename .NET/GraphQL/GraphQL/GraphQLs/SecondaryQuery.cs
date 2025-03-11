namespace GraphQL.GraphQLs
{
    using GraphQL.Persistance;
    using GraphQL.Persistance.Entities;
    using HotChocolate.Authorization;
    using HotChocolate.Data;
    using Microsoft.EntityFrameworkCore;

    public class SecondaryQuery
    {

        [UseProjection]
        [UseFiltering]
        public IQueryable<Group> GetGroups([Service] WriteDbContext writeDbContext) => writeDbContext.Groups.AsNoTracking();
    }
}
