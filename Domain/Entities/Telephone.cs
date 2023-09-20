namespace Domain.Entities;

public class Telephone : BaseEntity
{
    public int TelephoneType_Fk { get; set; }
    public TelephoneType TelephoneType { get; set; }
    public string PhoneNumber { get; set; }
    public int Person_Fk { get; set; }
    public Person Person { get; set; }

}
