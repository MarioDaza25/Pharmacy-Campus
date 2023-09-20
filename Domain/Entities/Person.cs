namespace Domain.Entities;

public class Person : BaseEntity
{
  public string Identification { get; set; }
  public int IdentificationType_Fk { get; set; }
  public IdentificationType IdentificationType { get; set; }
  public string Name { get; set; }
  public int PersonType_Fk { get; set; }
  public PersonType PersonType { get; set; }
  public int Role_Fk { get; set; }
  public Role Role { get; set; }
  public int JobTitle_Fk { get; set; }
  public JobTitle JobTitle { get; set; }
  public DateTime HireDate { get; set; }

  public ICollection<Address> Addresses { get; set; }
  public ICollection<Email> Emails { get; set; }
  public ICollection<Product> Products { get; set; }
  public ICollection<Purchase> Purchases { get; set; }
  public ICollection<Sale> SalesEmployees { get; set; }
  public ICollection<Sale> SalesPatients { get; set; }
  public ICollection<User> Users { get; set; }
  public ICollection<Telephone> Telephones { get; set; }
}
