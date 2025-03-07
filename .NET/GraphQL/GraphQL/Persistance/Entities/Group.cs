namespace GraphQL.Persistance.Entities
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;

    public class Group
    {
        [Key]
        public int Id { get; set; }

        public string Code { get; set; } = default!;

        public LTree Path { get; set; }
    }
}
