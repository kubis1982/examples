namespace GraphQL.Persistance.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Article
    {
        [Key]
        public int Id { get; set; }

        public string Code { get; set; } = default!;

        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        public string Unit { get; set; } = default!;
    }
}
