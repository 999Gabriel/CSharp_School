using Lesson_09_10.models;

namespace Lesson_09_10
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<Article> articles1 = CreateArticleList();
            // Mit LINQ können wir Listenartige Strukturen abfragen: 
            // Dazu gibt es 3 Möglichkeiten:
            // 1. Erweiterungsmethoden
            // 2. Abfrage-Syntax (wird vom Compiler in Erweiterungsmethoden umgewandelt
            // 3. Mischform von 1. und 2. (brauchen wir ab und zu)
            
            // 1. alle Artikel unter 100€ wollen wir haben 
            //      1a - Erweiterungsmethode (EM)
            var result = articles1.Where(a => a.Price < 100 && a.category == Category.Book);
            Console.WriteLine("=== LINQ - Erweiterungsmethode ===");
            result.ToList().ForEach(a => Console.WriteLine($"{a.Titel} - {a.Price:C}"));
            //      1b - Abfrage-Syntax (AS)
            var result1b = from a in articles1
                                            where a.Price < 100 && 
                                                  a.category == Category.Book
                                            select a;
            result1b.ToList().ForEach(a => Console.WriteLine($"{a.Titel} - {a.Price:C}"));
            
            //alle beispiele in beiden Varianten 
            //  2 - bestimme alle Artikel des aktuellen Jahres (2025)
            //       überspringe die ersten 2 Artikel und verwende dann den nächsten Artikel
            //      sortiert nach der Kategorie
            //          + Ausgabe + Überprüfung
            
            Console.WriteLine("\n=== LINQ - Artikel des Jahres 2025, überspringe 2, nimm 3 ===");
            var result2a = articles1
                .Where(a => a.ReleaseDate.Year == 2025)
                .Skip(2)
                .Take(3);
            result2a.ToList().ForEach(a => Console.WriteLine($"{a.Titel} - {a.ReleaseDate:dd.MM.yyyy}"));
            Console.WriteLine("=== Artikel-Verwaltungssystem ===\n");
            
            // 3. alle Artikel bei denen im Titel und/oder Beschreibung irgend ein bestimmtes Wort vorkommt
            //       3a - Erweiterungsmethode (EM)
            var result3a = articles1.Where(a => a.Titel.Contains("T-Shirt") || a.Description.Contains("T-Shirt"));
            Console.WriteLine("=== LINQ - Erweiterungsmethode ===");
            result3a.ToList().ForEach(a => Console.WriteLine($"{a.Titel} - {a.Price:C}"));
            //       3b - Abfrage-Syntax (AS)
            var result3b = from a in articles1
                                            where a.Titel.Contains("T-Shirt") || 
                                                  a.Description.Contains("T-Shirt")
                                            select a;
            //4. Any() / All()
            // - ermittle, ob bei allen Artikeln eine Kategorie angeg. wurde
            // - ermittle, ob es zumindest einen Artikel in der Kategorie Shirts existiert
            var result4a = articles1.Any(a => a.category == Category.Shirts);
            var result4b = articles1.All(a => a.category == Category.Shirts);
            Console.WriteLine("=== LINQ - Erweiterungsmethode ===");
            Console.WriteLine($"Any: {result4a}");
            Console.WriteLine($"All: {result4b}");
            
            
            // Artikel-Liste erstellen
            List<Article> articles = CreateArticleList();
            
            // Alle Artikel anzeigen
            Console.WriteLine("Alle verfügbaren Artikel:");
            Console.WriteLine("----------------------------------------");
            foreach (var article in articles)
            {
                Console.WriteLine($"ID: {article.Id} | {article.Titel} | Preis: {article.Price:C} | Kategorie: {article.category}");
            }
            
            // Suchmenü
            bool running = true;
            while (running)
            {
                Console.WriteLine("\n=== Suchmenü ===");
                Console.WriteLine("1 - Artikel nach ID suchen");
                Console.WriteLine("2 - Artikel nach Titel suchen");
                Console.WriteLine("3 - Artikel nach Kategorie suchen");
                Console.WriteLine("4 - Artikel nach Preis-Bereich suchen");
                Console.WriteLine("5 - Alle Artikel anzeigen");
                Console.WriteLine("0 - Beenden");
                Console.Write("\nWähle eine Option: ");
                
                string choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        SearchById(articles);
                        break;
                    case "2":
                        SearchByTitle(articles);
                        break;
                    case "3":
                        SearchByCategory(articles);
                        break;
                    case "4":
                        SearchByPriceRange(articles);
                        break;
                    case "5":
                        DisplayAllArticles(articles);
                        break;
                    case "0":
                        running = false;
                        Console.WriteLine("\nProgramm beendet. Auf Wiedersehen!");
                        break;
                    default:
                        Console.WriteLine("\nUngültige Eingabe!");
                        break;
                }
            }
        }

        public static List<Article> CreateArticleList()
        {
            return new List<Article>
            {
                new Article() 
                { 
                    Id = 1, 
                    Titel = "C# Grundlagen", 
                    Price = 50.00m, 
                    category = Category.Book, 
                    Description = "Einführung in C# Programmierung", 
                    ReleaseDate = new DateTime(2024, 1, 15)
                },
                new Article() 
                { 
                    Id = 2, 
                    Titel = "MacBook Pro M3", 
                    Price = 2499.99m, 
                    category = Category.Laptop, 
                    Description = "Leistungsstarker Laptop mit M3 Chip", 
                    ReleaseDate = new DateTime(2025, 11, 7)
                },
                new Article() 
                { 
                    Id = 3, 
                    Titel = "iPhone 15 Pro", 
                    Price = 1199.00m, 
                    category = Category.Handy, 
                    Description = "Neuestes iPhone mit A17 Pro Chip", 
                    ReleaseDate = new DateTime(2024, 9, 22)
                },
                new Article() 
                { 
                    Id = 4, 
                    Titel = "Mac Mini M4", 
                    Price = 699.00m, 
                    category = Category.Computer, 
                    Description = "Kompakter Desktop-Computer", 
                    ReleaseDate = new DateTime(2025, 11, 8)
                },
                new Article() 
                { 
                    Id = 5, 
                    Titel = "Clean Code", 
                    Price = 45.50m, 
                    category = Category.Book, 
                    Description = "Handbuch für agile Software-Entwicklung", 
                    ReleaseDate = new DateTime(2008, 8, 1)
                },
                new Article() 
                { 
                    Id = 6, 
                    Titel = "Galaxy S24 Ultra", 
                    Price = 1399.00m, 
                    category = Category.Handy, 
                    Description = "Premium Android Smartphone", 
                    ReleaseDate = new DateTime(2025, 1, 24)
                },
                new Article() 
                { 
                    Id = 7, 
                    Titel = "Dell XPS 15", 
                    Price = 1799.00m, 
                    category = Category.Laptop, 
                    Description = "Business Laptop mit hoher Performance", 
                    ReleaseDate = new DateTime(2024, 3, 10)
                },
                new Article() 
                { 
                    Id = 8, 
                    Titel = "Basic T-Shirt", 
                    Price = 19.99m, 
                    category = Category.Shirts, 
                    Description = "Einfaches Baumwoll-T-Shirt", 
                    ReleaseDate = new DateTime(2024, 1, 1)
                },
                new Article() 
                { 
                    Id = 9, 
                    Titel = "Airpods", 
                    Price = 199.99m, 
                    category = Category.NotSpecified, 
                    Description = "Airpods Pro 4", 
                    ReleaseDate = new DateTime(2025, 12, 24)
                },
                new Article() 
                { 
                    Id = 10, 
                    Titel = "Poubel Bracelet", 
                    Price = 249.99m, 
                    category = Category.NotSpecified, 
                    Description = "Poubel Connecciour Set", 
                    ReleaseDate = new DateTime(2025, 5, 21)
                }
            };
        }

        public static void SearchById(List<Article> articles)
        {
            Console.Write("\nGib die Artikel-ID ein: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var article = articles.Find(a => a.Id == id);
                if (article != null)
                {
                    DisplayArticleDetails(article);
                }
                else
                {
                    Console.WriteLine("Kein Artikel mit dieser ID gefunden!");
                }
            }
            else
            {
                Console.WriteLine("Ungültige ID!");
            }
        }

        public static void SearchByTitle(List<Article> articles)
        {
            Console.Write("\nGib den Titel (oder Teil davon) ein: ");
            string searchTerm = Console.ReadLine().ToLower();
            
            var results = articles.Where(a => a.Titel.ToLower().Contains(searchTerm)).ToList();
            
            if (results.Count > 0)
            {
                Console.WriteLine($"\n{results.Count} Artikel gefunden:");
                foreach (var article in results)
                {
                    Console.WriteLine($"ID: {article.Id} | {article.Titel} | {article.Price:C}");
                }
            }
            else
            {
                Console.WriteLine("Keine Artikel gefunden!");
            }
        }

        public static void SearchByCategory(List<Article> articles)
        {
            Console.WriteLine("\nVerfügbare Kategorien:");
            Console.WriteLine("0 - Book");
            Console.WriteLine("1 - Laptop");
            Console.WriteLine("2 - Computer");
            Console.WriteLine("3 - Handy");
            Console.WriteLine("4 - Shirts");
            Console.WriteLine("5 - NotSpecified");
            Console.Write("\nWähle eine Kategorie (0-5): ");
            
            if (int.TryParse(Console.ReadLine(), out int catNum) && catNum >= 0 && catNum <= 5)
            {
                Category selectedCategory = (Category)catNum;
                var results = articles.Where(a => a.category == selectedCategory).ToList();
                
                if (results.Count > 0)
                {
                    Console.WriteLine($"\n{results.Count} Artikel in Kategorie '{selectedCategory}' gefunden:");
                    foreach (var article in results)
                    {
                        Console.WriteLine($"ID: {article.Id} | {article.Titel} | {article.Price:C}");
                    }
                }
                else
                {
                    Console.WriteLine($"Keine Artikel in Kategorie '{selectedCategory}' gefunden!");
                }
            }
            else
            {
                Console.WriteLine("Ungültige Kategorie!");
            }
        }

        public static void SearchByPriceRange(List<Article> articles)
        {
            Console.Write("\nMinimalpreis eingeben: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal minPrice))
            {
                Console.WriteLine("Ungültiger Preis!");
                return;
            }
            
            Console.Write("Maximalpreis eingeben: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal maxPrice))
            {
                Console.WriteLine("Ungültiger Preis!");
                return;
            }
            
            var results = articles.Where(a => a.Price >= minPrice && a.Price <= maxPrice).ToList();
            
            if (results.Count > 0)
            {
                Console.WriteLine($"\n{results.Count} Artikel im Preisbereich {minPrice:C} - {maxPrice:C} gefunden:");
                foreach (var article in results)
                {
                    Console.WriteLine($"ID: {article.Id} | {article.Titel} | {article.Price:C} | {article.category}");
                }
            }
            else
            {
                Console.WriteLine("Keine Artikel in diesem Preisbereich gefunden!");
            }
        }

        public static void DisplayAllArticles(List<Article> articles)
        {
            Console.WriteLine("\n=== Alle Artikel ===");
            Console.WriteLine("----------------------------------------");
            foreach (var article in articles)
            {
                DisplayArticleDetails(article);
                Console.WriteLine("----------------------------------------");
            }
        }

        public static void DisplayArticleDetails(Article article)
        {
            Console.WriteLine($"\nID:          {article.Id}");
            Console.WriteLine($"Titel:       {article.Titel}");
            Console.WriteLine($"Preis:       {article.Price:C}");
            Console.WriteLine($"Kategorie:   {article.category}");
            Console.WriteLine($"Beschreibung: {article.Description}");
            Console.WriteLine($"Release:     {article.ReleaseDate:dd.MM.yyyy}");
        }
    }
}