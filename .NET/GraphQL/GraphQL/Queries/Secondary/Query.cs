namespace GraphQL.Queries.Secondary
{
    using GraphQL.Queries.Secondary.Types;

    public class Query
    {
        private static readonly Group[] Groups = [
            new()
            {
                Id = 1,
                Code = "A",
                Path = new GroupPath("A")
            },
            new()
            {
                Id = 2,
                Code = "B",
                Path = "A.B"
            },
            new()
            {
                Id = 3,
                Code = "C",
                Path = "A.B.C"
            }
            ];

        [UseProjection]
        [UseFiltering]
        public IQueryable<Group> GetGroups() => Groups.AsQueryable();
    }
}
