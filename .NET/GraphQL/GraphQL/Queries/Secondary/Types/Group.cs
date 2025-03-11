namespace GraphQL.Queries.Secondary.Types
{
    public class Group
    {
        [ID]
        public int Id { get; set; }

        public string Code { get; set; } = default!;

        public GroupPath Path { get; set; }
    }
}
