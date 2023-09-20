namespace Domain.Entities;

public class Purchase : BaseEntity
{
    public DateTime PurchaseDate { get; set; }
    public int Supplier_Fk { get; set; }
    public Person Supplier { get; set; }
    public ICollection<PurchaseProduct> PurchaseProducts { get; set; }
}
