namespace GraphQL.Queries.Primary.Persistance.Entities
{
    using Microsoft.EntityFrameworkCore;

    public class Group
    {
        [ID]
        public int Id { get; set; }

        public string Code { get; set; } = default!;

        public LTree Path { get; set; }
    }
}
