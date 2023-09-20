namespace Domain.Entities;

public class PaymentMethod : BaseEntity
{
    public string Description { get; set; }
    public ICollection<Sale> Sales { get; set; }
}
