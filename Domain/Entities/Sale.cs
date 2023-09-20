namespace Domain.Entities;

public class Sale : BaseEntity
{
    public DateTime SaleDate { get; set; }
    public int Patient_Fk { get; set; }
    public Person Patient { get; set; }
    public int Employee_Fk { get; set; }
    public Person Employee { get; set; }
    public int PaymentMethod_Fk { get; set; }
    public PaymentMethod PaymentMethod { get; set; }

    public ICollection<SaleProduct> SaleProducts { get; set; }
}
