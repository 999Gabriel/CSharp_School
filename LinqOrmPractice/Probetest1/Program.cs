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

        // Navigation Property für die Beziehung (m:n oder 1:n)
        // Da ein Artikel in mehreren Baskets sein kann und ein Basket mehrere Artikel hat,
        // ist m:n am sinnvollsten. Aber oft wird in solchen Schulaufgaben 1:n (Basket -> Article)
        // oder m:n vereinfacht verlangt.
        // Laut Aufgabe: "Beachte beim Programmieren der Klassen die Beziehung zwischen Basket und Article."
        // "Verwende den beste Beziehungstyp".
        // Ein Artikel (z.B. "iPhone") kann in vielen Warenkörben liegen.
        // Ein Warenkorb hat viele Artikel. -> m:n (Many-to-Many).
        
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
             * Ich verwende eine Many-to-Many (m:n) Beziehung.
             * Begründung: Ein Warenkorb (Basket) kann mehrere Artikel enthalten. Gleichzeitig kann derselbe Artikel
             * (z.B. ein spezifisches Produkt wie "USB-Stick") in den Warenkörben von mehreren verschiedenen Usern liegen.
             * Daher ist m:n der logischste Beziehungstyp.
             * 
             * b) Wie wird die obige Beziehung programmiert?
             * ANTWORT:
             * In EF Core wird dies durch Collection-Properties in beiden Klassen realisiert.
             * Klasse Basket hat: public List<Article> Articles { get; set; }
             * Klasse Article hat: public List<Basket> Baskets { get; set; }
             * EF Core erstellt automatisch eine Zwischentabelle (Join Table) in der Datenbank.
             * 
             * c) Erkläre alle Beziehungstypen zwischen 2 Klassen und wie diese programmiert werden.
             * ANTWORT:
             * 1. One-to-One (1:1):
             *    Ein Datensatz gehört genau zu einem anderen.
             *    Code: Referenz-Property auf beiden Seiten (z.B. User.Profile und Profile.User).
             *    
             * 2. One-to-Many (1:n):
             *    Ein Datensatz hat viele Kind-Datensätze (z.B. Blog -> Posts).
             *    Code: Die "One"-Seite hat eine List<Child>, die "Many"-Seite hat eine Referenz und einen Foreign Key.
             *    
             * 3. Many-to-Many (m:n):
             *    Viele Datensätze sind mit vielen anderen verknüpft (z.B. Student <-> Kurs).
             *    Code: List<T> auf beiden Seiten. EF Core nutzt eine Zwischentabelle.
             */

            // AUFGABE 2: HAUPTPROGRAMM
            // c) Exceptionhandling für das komplette Beispiel
            try
            {
                using (var db = new DbManager())
                {
                    db.Database.EnsureCreated();
                    Console.WriteLine("Datenbank und Tabellen wurden erstellt (falls nicht vorhanden).");

                    // Hier könnten weitere Operationen folgen, z.B. Daten einfügen, abfragen, etc.
                    //a) Neue Basket und Article Objekte erzeugen und speichern
                    var article1 = new Article()
                    {
                        ArticleNumber = 1,
                        Name = "Laptop",
                        Price = 1000m
                    };
                    var article2 = new Article()
                    {
                        ArticleNumber = 2,
                        Name = "Smartphone",
                        Price = 500m
                    };
                    var basket = new Basket()
                    {
                        BasketId = 1,
                        UserId = "917",
                        Articles = new List<Article> { article1, article2 }
                    };

                    db.Baskets.Add(basket);
                    db.Articles.Add(article1);
                    db.Articles.Add(article2);
                    db.SaveChangesAsync();
                    
                    //b) Alle Baskets mit ihren Artikeln auslesen und ausgeben
                    var basketsWithArticles = db.Baskets
                        .Where(b => b.UserId == "917")
                        .Include(b => b.Articles);

                    foreach (var b in basketsWithArticles)
                    {
                        Console.WriteLine($"Basket ID : {b.BasketId} +  User ID: {b.UserId}");
                        foreach (var a in b.Articles)
                        {
                            Console.WriteLine($"-- Article Number: {a.ArticleNumber}, Name: {a.Name}, Price: {a.Price}");
                        }
                    }
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine($"Fehler: {e.Message}");
                throw;
            }
        }
    }
}

