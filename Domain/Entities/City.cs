namespace Domain.Entities;

public class City : BaseEntity
{
    public string Name { get; set; }
    public int State_Fk { get; set; }
    public State State { get; set; }
    public ICollection<Neighborhood> Neighborhoods { get; set; }
}
