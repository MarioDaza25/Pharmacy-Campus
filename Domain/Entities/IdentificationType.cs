namespace Domain.Entities;

public class IdentificationType : BaseEntity
{
    public string Description { get; set; }
    public ICollection<Person> People { get; set; }
}
