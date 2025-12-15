using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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

    public class Program
    {
        public static void Main(string[] args)
        {
            using (var db = new DbManager())
            {
                try
                {
                    Article article = new Article()
                    {
                        ArticleNumber = 1,
                        Name = "Test Article",
                        Price = 9.99m
                    };
                    Basket basket = new Basket()
                    {
                        BasketId = 1,
                        UserId = "917",
                    };
                    // Artikel dem Warenkorb hinzufügen
                    basket.Articles.Add(article);
                    // Warenkorb dem Artikel hinzufügen (für m:n Beziehung)
                    article.Baskets.Add(basket);
                    // In die Datenbank einfügen
                    db.Articles.Add(article);
                    db.Baskets.Add(basket);
                    db.SaveChanges();
                    Console.WriteLine("Artikel und Warenkorb erfolgreich hinzugefügt.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
                
                var userBaskets = db.Baskets
                    .Where(b => b.UserId == "917")
                    .Include(b => b.Articles)
                    .ToList();

                foreach (var basket in userBaskets)
                {
                    Console.WriteLine($"Warenkorb ID: {basket.BasketId}, User ID: {basket.UserId}");
                    foreach (var art in basket.Articles)
                    {
                        Console.WriteLine($" - Artikelnummer: {art.ArticleNumber}, Name: {art.Name}, Preis: {art.Price}");
                    }
                }
            }
        }
    }
