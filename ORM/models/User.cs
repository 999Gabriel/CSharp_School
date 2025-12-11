using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM.models;

/*
 * User-Klasse mit Data Annotations für EF Core
 * Aufgabenstellung:
 * Annotations werden in den Datenklassen (z.B. Klasse Article, User, …) direkt oberhalb des
 * zu konfigurierenden Properties verwendet. Sie werden in [] angegeben (z.B. [Required]).
 //================================================================================================================//
 * Aufgabe dazu:
 * Erzeuge eine neue Klasse User (SocialSecurityNumber, Firstname, Lastname, Fullname, Birthdate).
 * Konfiguriere diese Klasse folgendermaßen:
 *  SocialSecurityNumber … Primary Key
 *  Fullname (entspricht Firstname + Lastname) soll nicht in der DB abgespeichert werden
 *  alle erzeugten Spaltennamen sollen als Kleinbuchstaben (z.B.firstname, lastname, ….) in der DB angezeigt werden
 *  Firstname und Lastname sollen max. 100 Zeichen lang sein
 *  Der Tabellenname soll members (über Annotations konfigurieren) lauten.
 * Erzeuge die Migrations und überprüfe das Ergebnis in der Datenbank.
*/
[Table("members")] // Tabellenname in der DB
public class User
{
    // Primary Key - SocialSecurityNumber als PK
    [Key]
    [Column("socialsecuritynumber")] // Spaltenname in Kleinbuchstaben
    [MaxLength(100)] // max. 100 Zeichen
    public string SocialSecurityNumber { get; set; } = string.Empty;
    
    
    [Column("firstname")] // Spaltenname in Kleinbuchstaben
    [MaxLength(100)] // max. 100 Zeichen
    public string Firstname { get; set; } = string.Empty;
    
    [Column("lastname")] // Spaltenname in Kleinbuchstaben
    [MaxLength(100)] // max. 100 Zeichen
    public string Lastname { get; set; } = string.Empty;
    
    // Fullname wird NICHT in der DB gespeichert (NotMapped)
    // entspricht Firstname + Lastname
    [NotMapped]
    public string Fullname => $"{Firstname} {Lastname}";
    
    [Column("birthdate")] // Spaltenname in Kleinbuchstaben
    public DateTime Birthdate { get; set; }
}

