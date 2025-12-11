using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ORM.models;

namespace ORM.configuration;

/*
 * Fluent-API Konfigurationsklasse für User2
 * Diese Klasse konfiguriert die User2-Entität für die Datenbank
 * Variante 2: Konfiguration in separater Klasse (übersichtlicher bei großen Projekten)
 */
public class User2Configuration : IEntityTypeConfiguration<User2>
{
    public void Configure(EntityTypeBuilder<User2> builder)
    {
        // Tabellenname: members2
        builder.ToTable("members2");
        
        // Primary Key: SocialSecurityNumber
        builder.HasKey(u => u.SocialSecurityNumber);
        
        // SocialSecurityNumber Konfiguration
        builder.Property(u => u.SocialSecurityNumber)
            .HasColumnName("socialsecuritynumber") // Spaltenname in Kleinbuchstaben
            .HasMaxLength(100); // max. 100 Zeichen
        
        // Firstname Konfiguration
        builder.Property(u => u.Firstname)
            .HasColumnName("firstname") // Spaltenname in Kleinbuchstaben
            .HasMaxLength(100); // max. 100 Zeichen
        
        // Lastname Konfiguration
        builder.Property(u => u.Lastname)
            .HasColumnName("lastname") // Spaltenname in Kleinbuchstaben
            .HasMaxLength(100); // max. 100 Zeichen
        
        // Fullname wird NICHT in der DB gespeichert
        builder.Ignore(u => u.Fullname);
        
        // Birthdate Konfiguration
        builder.Property(u => u.Birthdate)
            .HasColumnName("birthdate"); // Spaltenname in Kleinbuchstaben
    }
}

