namespace iOSApp1.Models;

public class User
{
    public int UserId { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    
    public override string ToString()
    {
        return $"Name: {Firstname} {Lastname}\nEmail: {Email}\nDOB: {DateOfBirth:d}";
    }
}

