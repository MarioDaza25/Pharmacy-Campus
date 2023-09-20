namespace Domain.Entities;

public class Neighborhood : BaseEntity
{
    public string Name { get; set; }
    public int City_Fk { get; set; }
    public City City { get; set; }
    public ICollection<Address> Addresses { get; set; }
}
