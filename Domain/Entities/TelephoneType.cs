namespace Domain.Entities;

public class TelephoneType : BaseEntity
{
    public string Description { get; set; }
    public ICollection<Telephone> Telephones { get; set; }
}
