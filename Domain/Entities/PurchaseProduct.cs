namespace Domain.Entities;

public class PurchaseProduct : BaseEntity
{
    public int Purchase_Fk { get; set; }
    public Purchase Purchase { get; set; }
    public int Product_Fk { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
