namespace API.Entities;

public class AppUser
{
    public required string? UserName { get; set; }
    public int Id { get; set; }

    public required byte[] PasswordHash { get; set; }

    public required byte[] PasswordSalt {get; set; }
}