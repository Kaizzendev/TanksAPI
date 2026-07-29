namespace TanksAPI.Models;

public class User
{
    public Guid Id { get; set; }
    public String Username { get; set; }
    public String PasswordHash { get; set; }    
}