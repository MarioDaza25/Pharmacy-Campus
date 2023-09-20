namespace Domain.Entities;

public class Role : BaseEntity
{
    public string Description { get; set; }
    public ICollection<Person> People { get; set; }
}
