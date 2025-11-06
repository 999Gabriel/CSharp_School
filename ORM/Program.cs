using System;
using System.Threading.Tasks;
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
                    Console.WriteLine("6. BEENDEN");
                    Console.Write("Wähle eine Option (1-6): ");

                    string choice = Console.ReadLine() ?? "";

                    switch (choice)
                    {
                        case "1":
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
                    }
                }
            }
        }

        // ==================== CREATE ====================
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
    }
}