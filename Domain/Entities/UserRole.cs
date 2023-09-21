namespace Domain.Entities;

public class UserRole 
{
    public int User_Fk { get; set; }
    public User User { get; set; }
    public int Role_Fk { get; set; }
    public JobTitle JobTitle { get; set; }
}
