namespace Domain.Entities;

public class SaleProduct : BaseEntity
{
    public int Sale_Fk { get; set; }
    public Sale Sale { get; set; }
    public int Product_Fk { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
