namespace Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public int  Employee_Fk { get; set; }
    public Person Employee { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; }
    public ICollection<UserRole> UsersRole { get; set; }
}
