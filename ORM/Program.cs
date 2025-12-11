using Microsoft.EntityFrameworkCore;
using ORM.Services;
using ORM.models;

namespace ORM
{
    /*
     * ORM ... Object-Relational Mapping
     *
     * der ORM übernimmt das mapping (abbildung) unserer Klassen auf Tabellen in der Database
     *
     * wir benötigen kein SQL mehr
     * der ORM erzeugt die Database, die Tabellen; es sind keine JOINS usw mehr notwendig
     * - wird alles vom ORM erzeugt und erledigt
     *
     * Entity Framework Core (EF Core) ist der ORM von Microsoft für .NET Core
     * EF Core ... Basispaket (immer benötigt)
     * EF Core Tools ... für Migrations
     * Pomelo ... spezifische EF Core Treiber für MySQL
     *
     *  Migrations
     * add-Migration <Name> ... erzeugt eine Migration mit dem angegebenen Namen
     * update-Database ... wendet die Migrationen auf die Database an
     *
     * remove-Migration ... entfernt die letzte Migration (nur wenn sie noch nicht auf die Database angewendet wurde)
     * muss vor update-Database ausgeführt werden
     *  sie löscht die letzte Migration falls diese nicht benötigt wird
     *
     * update-database ... es werden alle noch nicht angewendeten Migrationen auf die Database angewendet
     * - erst zu diesem Zeitpunkt wird die Database tatsächlich verändert
     *
     * =================================================================================================================
     *
     * 13.11.2025
     * Nur eine einzige Tabelle
     * CRUD-Operationen
     *  - GetAllArticlesAsync()
     *  - GetArticlesByIdAsync()
     *  - AddArticleAsync()
     *  - RemoveArticleAsync()
     *  - UpdateArticleAsync()
     *
     * Beziehungen
     *  1:n-Beziehung (bsp Class: Article - Reviews)
     *
     *
     * damit der ORM eine 1:n Beziehung erkennt (z.B. Article zu Reviews), muss ein Artikel eine
     * List <R> und in R eine Instanz A enthalten sein.
     * --> Navigation Properties
     *
     *
     * CRUD-Operationen
     *  - eine Review zu einem Artikel hinzufügen
     *  - alle Reviews zu einem Artikel auslesen (Artikel und Reviews)
     *  - Review zu einem Artikel ändern/updaten
     *  - Review zu einem Artikel löschen
     *
     *
     *
     * m:n Beziehung
     * SQL: m:n-Beziehungen müssen immer in 2 1:n-Beziehungen aufgelöst werden
     *  Aus A -- B wird A -- A_B -- B (1:n und n:1)
     *  Die n-Seite ist dabei immer dort wo die PK (FK) sich befinden (in A_B).
     *
     *
     * Fall 1:
     *  befinden sich in der Zwischentabelle nur die PK von A bzw B (keine zusätzlichen Felder), dann
     *  kann der ORM die Zwischenklasse selber erzeugen.
     *
     *  
     *
     * Fall 2:
     *
     *  Befinden sich in der Zwischentabelle neben den PK vonn A bzw B weitere Felder
     *  dann muss die Zwischenklasse A_B ausprogrammiert werden.
     *
     * ORM:
     *  in A muss eine List<A_B>
     *  in B muss eine List<A_B>
     *  in A_B muss eine Instanz von A und eine Instanz von B
     *
     *
     * CRUD-Operationen
     *  -   eine Invoice mit mehreren Artikeln erzeugen
     *  -   eine Invoice inkl. aller Artikel ermitteln
     *  -   Anzahl eines best. Artikels ändern
     *  -   Invoice löschen
     *  
     *
     *
     *
     * 
     */
    public class Program
    {
        public static async Task Main(string[] args)
        {
            using (DbManager dbManager = new())
            {
                bool running = true;
                while (running)
                {
                    Console.WriteLine("\n========== CRUD MENÜ ==========");
                    Console.WriteLine("1. CREATE - Artikel hinzufügen");
                    Console.WriteLine("2. READ - Alle Artikel anzeigen");
                    Console.WriteLine("3. READ - Artikel nach ID suchen");
                    Console.WriteLine("4. UPDATE - Artikel aktualisieren");
                    Console.WriteLine("5. DELETE - Artikel löschen");
                    Console.WriteLine("6. CREATE - Review hinzufügen");
                    Console.WriteLine("7. READ - Alle Reviews anzeigen");
                    Console.WriteLine("8. UPDATE - Review aktualisieren");
                    Console.WriteLine("9. DELETE - Review löschen");
                    Console.WriteLine("10. BEENDEN");
                    Console.Write("Wähle eine Option (1-10): ");


                    string choice = Console.ReadLine() ?? "";

                    switch (choice)
                    {
                        /*case "1":
                            await CreateArticle(dbManager);
                            break;
                        case "2":
                            await ReadAllArticlesAsync(dbManager);
                            break;
                        case "3":
                            await ReadArticleByIdAsync(dbManager);
                            break;
                        case "4":
                            await UpdateArticleAsync(dbManager);
                            break;
                        case "5":
                            await DeleteArticleAsync(dbManager);
                            break;
                        case "6":
                            running = false;
                            Console.WriteLine("Auf Wiedersehen!");
                            break;
                        default:
                            Console.WriteLine("❌ Ungültige Option!");
                            break;
                            */
                        case "6":
                            await CreateReviewAsync(dbManager);
                            break;
                        case "7":
                            await ReadAllReviewsAsync(dbManager);
                            break;
                        case "8":
                            await UpdateReviewAsync(dbManager);
                            break;
                        case "9":
                            await DeleteReviewAsync(dbManager);
                            break;
                        case "10":
                            running = false;
                            Console.WriteLine("Auf Wiedersehen!");
                            break;
                        
                    }
                }
            }
        }

        /*// ==================== CREATE ====================
        private static async Task CreateArticle(DbManager dbManager)
        {
            Console.WriteLine("\n--- Neuer Artikel ---");
            Console.Write("Titel: ");
            string title = Console.ReadLine() ?? "";

            Console.Write("Name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Preis (€): ");
            decimal price = decimal.Parse(Console.ReadLine() ?? "0");

            Console.Write("Veröffentlichungsdatum (yyyy-MM-dd): ");
            DateTime releaseDate = DateTime.Parse(Console.ReadLine() ?? DateTime.Now.ToString("yyyy-MM-dd"));

            Article newArticle = new Article()
            {
                ArticleId = 0,
                Title = title,
                Name = name,
                Price = price,
                ReleaseDate = releaseDate
            };

            if (await dbManager.CreateArticleAsync(newArticle))
            {
                Console.WriteLine("✅ Artikel wurde hinzugefügt!");
            }
            else
            {
                Console.WriteLine("❌ Fehler beim Hinzufügen des Artikels!");
            }
        }

        // ==================== READ ALL ====================
        private static async Task ReadAllArticlesAsync(DbManager dbManager)
        {
            Console.WriteLine("\n--- Alle Artikel ---");
            var articles = await dbManager.GetAllArticlesAsync();

            if (articles.Count == 0)
            {
                Console.WriteLine("📭 Keine Artikel vorhanden!");
                return;
            }

            foreach (var article in articles)
            {
                PrintArticle(article);
            }
        }

        // ==================== READ BY ID ====================
        private static async Task ReadArticleByIdAsync(DbManager dbManager)
        {
            Console.Write("\nArtikel-ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var article = await dbManager.GetArticleByIdAsync(id);

                if (article != null)
                {
                    Console.WriteLine("\n--- Gefundener Artikel ---");
                    PrintArticle(article);
                }
                else
                {
                    Console.WriteLine("❌ Artikel nicht gefunden!");
                }
            }
            else
            {
                Console.WriteLine("❌ Ungültige ID!");
            }
        }

        // ==================== UPDATE ====================
        private static async Task UpdateArticleAsync(DbManager dbManager)
        {
            Console.Write("\nArtikel-ID zum Aktualisieren: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var article = await dbManager.GetArticleByIdAsync(id);

                if (article != null)
                {
                    Console.WriteLine("\n--- Aktuellen Werte ---");
                    PrintArticle(article);

                    Console.WriteLine("\n--- Neue Werte eingeben ---");
                    Console.Write("Neuer Titel (oder Enter zum Überspringen): ");
                    string newTitle = Console.ReadLine() ?? "";
                    if (!string.IsNullOrEmpty(newTitle))
                        article.Title = newTitle;

                    Console.Write("Neuer Name (oder Enter zum Überspringen): ");
                    string newName = Console.ReadLine() ?? "";
                    if (!string.IsNullOrEmpty(newName))
                        article.Name = newName;

                    Console.Write("Neuer Preis (oder Enter zum Überspringen): ");
                    string priceInput = Console.ReadLine() ?? "";
                    if (!string.IsNullOrEmpty(priceInput) && decimal.TryParse(priceInput, out decimal newPrice))
                        article.Price = newPrice;

                    Console.Write("Neues Veröffentlichungsdatum (oder Enter zum Überspringen): ");
                    string dateInput = Console.ReadLine() ?? "";
                    if (!string.IsNullOrEmpty(dateInput) && DateTime.TryParse(dateInput, out DateTime newDate))
                        article.ReleaseDate = newDate;

                    if (await dbManager.UpdateArticleAsync(article))
                    {
                        Console.WriteLine("✅ Artikel wurde aktualisiert!");
                    }
                    else
                    {
                        Console.WriteLine("❌ Fehler beim Aktualisieren!");
                    }
                }
                else
                {
                    Console.WriteLine("❌ Artikel nicht gefunden!");
                }
            }
            else
            {
                Console.WriteLine("❌ Ungültige ID!");
            }
        }

        // ==================== DELETE ====================
        private static async Task DeleteArticleAsync(DbManager dbManager)
        {
            Console.Write("\nArtikel-ID zum Löschen: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var article = await dbManager.GetArticleByIdAsync(id);

                if (article != null)
                {
                    Console.WriteLine("\n--- Artikel zum Löschen ---");
                    PrintArticle(article);

                    Console.Write("\nWirklich löschen? (ja/nein): ");
                    string confirm = Console.ReadLine()?.ToLower() ?? "";

                    if (confirm == "ja")
                    {
                        if (await dbManager.DeleteArticleAsync(article))
                        {
                            Console.WriteLine("✅ Artikel wurde gelöscht!");
                        }
                        else
                        {
                            Console.WriteLine("❌ Fehler beim Löschen!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("⚠️ Löschen abgebrochen!");
                    }
                }
                else
                {
                    Console.WriteLine("❌ Artikel nicht gefunden!");
                }
            }
            else
            {
                Console.WriteLine("❌ Ungültige ID!");
            }
        }

        // ==================== HELPER ====================
        private static void PrintArticle(Article article)
        {
            Console.WriteLine($"  ID: {article.ArticleId}");
            Console.WriteLine($"  Titel: {article.Title}");
            Console.WriteLine($"  Name: {article.Name}");
            Console.WriteLine($"  Preis: € {article.Price:F2}");
            Console.WriteLine($"  Veröffentlichung: {article.ReleaseDate:yyyy-MM-dd}");
            Console.WriteLine();
        }
        */

        
        // ==================== CREATE REVIEW ====================
        private static async Task CreateReviewAsync(DbManager dbManager)
        {
            Console.WriteLine("\n--- Neue Review hinzufügen ---");
            Console.Write("Artikel-ID: ");
            if (!int.TryParse(Console.ReadLine(), out int articleId))
            {
                Console.WriteLine("❌ Ungültige Artikel-ID!");
                return;
            }

            var article = await dbManager.GetArticleByIdAsync(articleId);
            if (article == null)
            {
                Console.WriteLine("❌ Artikel nicht gefunden!");
                return;
            }

            Console.Write("Review-Text: ");
            string reviewText = Console.ReadLine() ?? "";

            Console.Write("Rating (1-5): ");
            if (!int.TryParse(Console.ReadLine(), out int rating) || rating < 1 || rating > 5)
            {
                Console.WriteLine("❌ Ungültiges Rating!");
                return;
            }

            Review newReview = new Review
            {
                ReviewText = reviewText,
                Rating = rating,
                ArticleId = articleId
            };

            if (await dbManager.CreateReviewAsync(newReview))
            {
                Console.WriteLine("✅ Review wurde hinzugefügt!");
            }
            else
            {
                Console.WriteLine("❌ Fehler beim Hinzufügen der Review!");
            }
        }

// ==================== READ ALL REVIEWS ====================
        private static async Task ReadAllReviewsAsync(DbManager dbManager)
        {
            Console.WriteLine("\n--- Alle Reviews ---");
            await dbManager.GetAllReviewsAsync();
        }

// ==================== UPDATE REVIEW ====================
        private static async Task UpdateReviewAsync(DbManager dbManager)
        {
            Console.Write("\nReview-ID zum Aktualisieren: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("❌ Ungültige ID!");
                return;
            }

            var review = await dbManager.Reviews.FirstOrDefaultAsync(r => r.ReviewId == id);
            if (review == null)
            {
                Console.WriteLine("❌ Review nicht gefunden!");
                return;
            }

            Console.WriteLine($"\nAktueller Text: {review.ReviewText}");
            Console.WriteLine($"Aktuelles Rating: {review.Rating}");

            Console.Write("\nNeuer Review-Text (oder Enter zum Überspringen): ");
            string newText = Console.ReadLine() ?? "";
            if (!string.IsNullOrEmpty(newText))
                review.ReviewText = newText;

            Console.Write("Neues Rating (1-5, oder Enter zum Überspringen): ");
            string ratingInput = Console.ReadLine() ?? "";
            if (!string.IsNullOrEmpty(ratingInput) && int.TryParse(ratingInput, out int newRating) && newRating >= 1 &&
                newRating <= 5)
                review.Rating = newRating;

            if (await dbManager.UpdateReviewAsync(review))
            {
                Console.WriteLine("✅ Review wurde aktualisiert!");
            }
            else
            {
                Console.WriteLine("❌ Fehler beim Aktualisieren!");
            }
        }

// ==================== DELETE REVIEW ====================
        private static async Task DeleteReviewAsync(DbManager dbManager)
        {
            Console.Write("\nReview-ID zum Löschen: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("❌ Ungültige ID!");
                return;
            }

            var review = await dbManager.Reviews.FirstOrDefaultAsync(r => r.ReviewId == id);
            if (review == null)
            {
                Console.WriteLine("❌ Review nicht gefunden!");
                return;
            }

            Console.WriteLine($"\nReview: {review.ReviewText}");
            Console.WriteLine($"Rating: {review.Rating}");
            Console.Write("\nWirklich löschen? (ja/nein): ");
            string confirm = Console.ReadLine()?.ToLower() ?? "";

            if (confirm == "ja")
            {
                if (await dbManager.DeleteReviewAsync(review))
                {
                    Console.WriteLine("✅ Review wurde gelöscht!");
                }
                else
                {
                    Console.WriteLine("❌ Fehler beim Löschen!");
                }
            }
            else
            {
                Console.WriteLine("⚠️ Löschen abgebrochen!");
            }
        }
    }
}