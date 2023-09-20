namespace Domain.Entities;

public class State : BaseEntity
{
    public string Name { get; set; }
    public int Country_Fk { get; set; }
    public Country Country { get; set; }
    public ICollection<City> Cities { get; set; }
}
