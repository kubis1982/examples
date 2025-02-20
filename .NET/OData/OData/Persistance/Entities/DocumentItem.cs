namespace OData.Persistance.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class DocumentItem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("DocumentId")]
        public Document Document { get; set; } = default!;

        public decimal Quantity { get; set; }

        public string? Description { get; set; }

        [ForeignKey("ArticleId")]
        public Article Article { get; set; } = default!;
    }
}
