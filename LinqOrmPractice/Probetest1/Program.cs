using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Probetest1
{
    // ==========================================
    // MODEL KLASSEN (Vorgabe)
    // ==========================================

    public class Article
    {
        public int ArticleNumber { get; set; } // Primärschlüssel
        public string Name { get; set; }
        public decimal Price { get; set; }

        // Für EF Core m:n brauchen wir eine Liste von Baskets
        public List<Basket> Baskets { get; set; } = new List<Basket>();
    }

    public class Basket
    {
        public int BasketId { get; set; } // Primärschlüssel (Tippfehler in Aufgabe "Basketld" -> BasketId)
        public string UserId { get; set; } // "Userld" -> UserId

        // Navigation Property
        public List<Article> Articles { get; set; } = new List<Article>();
    }

    // ==========================================
    // DATENBANK CONTEXT (DbManager)
    // ==========================================

    public class DbManager : DbContext
    {
        // Properties müssen Baskets und Articles lauten (Vorgabe)
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Article> Articles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Verbindung zur Datenbank (MySQL)
            // Passe den Connection String ggf. an deine lokale Umgebung an!
            var connectionString = "server=localhost;user=root;password=Nij43Bq8;database=probetest_db";
            var serverVersion = new MySqlServerVersion(new System.Version(8, 0, 29));
            optionsBuilder.UseMySql(connectionString, serverVersion);

            // Für Logging (optional, hilft beim Debuggen)
            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konfiguration der Primärschlüssel, falls nicht per Konvention erkannt
            modelBuilder.Entity<Article>().HasKey(a => a.ArticleNumber);
            modelBuilder.Entity<Basket>().HasKey(b => b.BasketId);

            // Konfiguration der m:n Beziehung
            // EF Core erkennt List<T> <-> List<T> automatisch als m:n und erstellt eine Zwischentabelle.
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========== PROBETEST 1 ==========");

            /*
             * AUFGABE 1: THEORIE
             *
             * a) Erkläre, welche Beziehung du zwischen Basket und Article verwendest und wieso.
             * ANTWORT:
             * zwischen Basket und Article wird eine 1:n Beziehung verwendet, da ein Warenkorb mehrere Artikel haben kann,
             * und ein Artikel zu genau einem Warenkorb gehört.
             *
             * RICHTIG:
             * wir haben eine m:n Beziehung, da ein Basket mehrere Articles enthalten kann,
             * und ein Article in mehreren Baskets enthalten sein kann.
             * Beispiel: Ein Artikel "Mouse" kann in den Baskets von User1 und User2 enthalten sein.
             * 
             * 
             * b) Wie wird die obige Beziehung programmiert?
             * ANTWORT:
             * einne 1:n Bzeihung wird programmiert, indem in der Klasse Basket eine List von Articles als Navigation
             * Property definiert wird, und in der Klasse Article eine Referenz auf den Basket als Navigation Property.
             * 
             *
             * c) Erkläre alle Beziehungstypen zwischen 2 Klassen und wie diese programmiert werden.
             * ANTWORT:
             * es gibt 3 Arten von Beziehungen:
             * 1. 1:n: eine Klasse hat eine Liste von Objekten der anderen Klasse, und die andere Klasse hat eine
             * Referenz auf die erste Klasse.
             * 2. n:1: eine Klasse hat eine Referenz auf die andere Klasse, und die andere Klasse hat eine
             * Liste von Objekten der ersten Klasse.
             * 3. m:n: beide Klassen haben eine Liste von Objekten der jeweils anderen Klasse.
             * Bei m:n Beziehungen wird in EF Core automatisch eine Zwischentabelle erstellt.
             * eine Zwischentabelle ist eine zusätzliche Klasse, die die Primärschlüssel beider Klassen als
             * Fremdschlüssel enthält. Sie dient dazu, die m:n Beziehung zu modellieren.
             * 
             */

            // AUFGABE 2: HAUPTPROGRAMM
            // c) Exceptionhandling für das komplette Beispiel
            try
            {
                using (var db = new DbManager())
                {
                    // 1. Aufgabe: Erstelle eine Order und füge die "Mouse" 2 mal hinzu (m:n Beziehung).
                    Console.WriteLine("\n--- Aufgabe 2: Erstelle eine Order und füge die 'Mouse' 2 mal hinzu ---");
                    // Artikel erstellen
                    Article mouseArticle = new Article();
                    mouseArticle.ArticleNumber = 1;
                    mouseArticle.Name = "Mouse";
                    mouseArticle.Price = 40m;
                    
                    Article mouseArticle2 = new Article();
                    mouseArticle2.ArticleNumber = 2;
                    mouseArticle2.Name = "Mouse 2";
                    mouseArticle2.Price = 450m;
                    
                    //Basket erstellen
                    Basket basket = new Basket()
                    {
                        BasketId = 1,
                        UserId = "917",
                    };
                    
                    // Artikel dem Warenkorb hinzufügen
                    basket.Articles.Add(mouseArticle);
                    basket.Articles.Add(mouseArticle2);
                    // Warenkorb dem Artikel hinzufügen (für m:n Beziehung)
                    mouseArticle.Baskets.Add(basket);
                    mouseArticle2.Baskets.Add(basket);
                    // In die Datenbank einfügen
                    db.Articles.Add(mouseArticle);
                    db.Articles.Add(mouseArticle2);
                    db.Baskets.Add(basket);
                    // FALSCH:
                    // db.SaveChanges();
                    // if (db.SaveChanges() > 0) { ... }

                    // RICHTIG:
                    if (db.SaveChanges() > 0)
                    {
                        Console.WriteLine("Artikel und Warenkorb erfolgreich hinzugefügt.");
                    }
                    else 
                    {
                        // Dies passiert, wenn keine Änderungen erkannt wurden (nicht zwingend ein Fehler, aber hier unerwartet)
                        Console.WriteLine("Keine Änderungen gespeichert.");
                    }

                    // 2. Aufgabe: Lade alle Warenkörbe für den User mit der UserId "917" und gib die enthaltenen
                    // Artikel aus.
                    Console.WriteLine("\n--- Aufgabe 3: Lade alle Warenkörbe für UserId '917' und gib die Artikel aus ---");
                    var userBasket = db.Baskets
                        .Where(b => b.UserId == "917")
                        .Include(b => b.Articles)
                        .ToList();
                    foreach (var b in userBasket)
                    {
                        Console.WriteLine($"Warenkorb ID: {b.BasketId}, User ID: {b.UserId}");
                        foreach (var art in b.Articles)
                        {
                            Console.WriteLine($" - Artikelnummer: {art.ArticleNumber}, Name: {art.Name}, Preis: {art.Price}");
                        }
                    }
                    
                    // Ende
                    Console.WriteLine("\n========== ENDE PROBETEST 1 ==========");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler: {ex.Message}");
            }
        }
    }
}

