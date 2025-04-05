namespace GraphQL.Queries.Primary.Persistance.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Contractor
    {
        [Key]
        public int Id { get; set; }

        public string Code { get; set; } = default!;

        public string Name { get; set; } = default!;

        public string? Description { get; set; }
    }
}
