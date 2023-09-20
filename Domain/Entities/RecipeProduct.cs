namespace Domain.Entities;

public class RecipeProduct : BaseEntity
{
    public int Product_Fk { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public int Recipe_Fk { get; set; }
    public Recipe Recipe { get; set; }
}
