namespace ORM.models;

/*
 * User2-Klasse OHNE Annotations
 * Konfiguration erfolgt über Fluent-API in separater Konfigurationsklasse
 */
public class User2
{
    public string SocialSecurityNumber { get; set; } = string.Empty;
    
    public string Firstname { get; set; } = string.Empty;
    
    public string Lastname { get; set; } = string.Empty;
    
    // Fullname wird NICHT in der DB gespeichert
    // entspricht Firstname + Lastname
    public string Fullname => $"{Firstname} {Lastname}";
    
    public DateTime Birthdate { get; set; }
}

