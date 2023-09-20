namespace Domain.Entities;

public class Email : BaseEntity
{
    public string Description { get; set; }
    public int Person_Fk { get; set; }
    public Person Person { get; set; }
    public int EmailType_Fk { get; set; }
    public EmailType EmailType { get; set; }

}
