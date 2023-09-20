namespace Domain.Entities;

public class JobTitle : BaseEntity
{
    public string Description { get; set; }
    public ICollection<Person> People { get; set; }
    public ICollection<UserRole> UsersRoles { get; set; }
}
