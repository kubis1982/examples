namespace GraphQL.Persistance.Entities
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;

    public class Group
    {
        [Key]
        [ID]
        public int Id { get; set; }

        public string Code { get; set; } = default!;

        [GraphQLIgnore]
        public LTree Path { get; set; }
    }
}
