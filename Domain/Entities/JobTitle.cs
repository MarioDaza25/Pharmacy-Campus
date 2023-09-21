namespace Domain.Entities;

public class JobTitle : BaseEntity
{
    public string Description { get; set; }
    public ICollection<Person> People { get; set; }
    public ICollection<User> Users { get; set; } = new HashSet<User>();
    public ICollection<UserRole> UsersRole { get; set; }
}
