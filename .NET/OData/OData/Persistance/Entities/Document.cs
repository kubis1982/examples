namespace OData.Persistance.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Document
    {
        [Key]
        public int Id { get; set; }

        public string Number { get; set; } = default!;

        public DateTime? ExecuteDate { get; set; }

        public string? Description { get; set; }

        [ForeignKey("ContractorId")]
        public Contractor Contractor { get; set; } = default!;

        public ICollection<DocumentItem> Items { get; set; } = [];
    }
}
