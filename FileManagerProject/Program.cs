using FileManagerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileManagerProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Artikelverwaltungssystem mit Filemanagement ===");
            Console.WriteLine();

            // FileManager für Artikel erstellen
            FileManager fileManager = new FileManager("/Users/gabriel/RiderProjects/Csharp-code-school/FileManagerProject/articles.txt");

            // Artikel aus der Textdatei laden
            List<Article> articles = fileManager.LoadArticles();

            // Hauptmenü
            bool programmLaeuft = true;
            while (programmLaeuft)
            {
                Console.WriteLine("\n=== Hauptmenü ===");
                Console.WriteLine("1. Alle Artikel anzeigen");
                Console.WriteLine("2. Artikel nach Kategorie filtern");
                Console.WriteLine("3. Neuen Artikel hinzufügen");
                Console.WriteLine("4. Artikel nach Autor suchen");
                Console.WriteLine("5. Bücher nach Lesezeit filtern");
                Console.WriteLine("6. Artikel speichern");
                Console.WriteLine("0. Programm beenden");
                Console.Write("Wählen Sie eine Option: ");

                string? eingabe = Console.ReadLine();
                Console.WriteLine();

                switch (eingabe?.Trim())
                {
                    case "1":
                        ShowAllArticles(articles);
                        break;
                    case "2":
                        FilterArticlesByCategory(articles);
                        break;
                    case "3":
                        AddNewArticle(fileManager, articles);
                        break;
                    case "4":
                        SearchArticlesByAuthor(articles);
                        break;
                    case "5":
                        FilterArticlesByReadingTime(articles);
                        break;
                    case "6":
                        fileManager.SaveArticles(articles);
                        break;
                    case "0":
                        programmLaeuft = false;
                        Console.WriteLine("Programm wird beendet...");
                        break;
                    default:
                        Console.WriteLine("Ungültige Eingabe! Bitte wählen Sie eine Option zwischen 0-6.");
                        break;
                }
            }

            Console.WriteLine("Auf Wiedersehen!");
        }

        // Zeigt alle Artikel an
        // <param name="articles">Liste der Artikel</param>
        private static void ShowAllArticles(List<Article> articles)
        {
            Console.WriteLine("=== Alle Artikel ===");
            if (articles.Count == 0)
            {
                Console.WriteLine("Keine Artikel vorhanden.");
                return;
            }

            int index = 0;
            while (index < articles.Count)
            {
                Console.WriteLine($"{index + 1}. {articles[index].ToString()}");
                index++;
            }
        }

        // Filtert Artikel nach Kategorie
        // <param name="articles">Liste der Artikel</param>
        private static void FilterArticlesByCategory(List<Article> articles)
        {
            Console.WriteLine("=== Artikel nach Kategorie filtern ===");
            Console.WriteLine("Verfügbare Kategorien:");
            Console.WriteLine("1. Book");
            Console.WriteLine("2. Laptop");
            Console.WriteLine("3. Shoes");
            Console.WriteLine("4. Handy");
            Console.WriteLine("5. NotSpecified");
            Console.Write("Wählen Sie eine Kategorie (1-5): ");

            if (int.TryParse(Console.ReadLine(), out int categoryChoice))
            {
                Category selectedCategory = categoryChoice switch
                {
                    1 => Category.Book,
                    2 => Category.Laptop,
                    3 => Category.Shoes,
                    4 => Category.Handy,
                    5 => Category.NotSpecified,
                    _ => Category.NotSpecified
                };

                Console.WriteLine($"\n=== Artikel der Kategorie '{selectedCategory}' ===");
                int index = 0;
                int gefundeneArtikel = 0;
                while (index < articles.Count)
                {
                    if (articles[index].Category1 == selectedCategory)
                    {
                        Console.WriteLine($"{gefundeneArtikel + 1}. {articles[index].ToString()}");
                        gefundeneArtikel++;
                    }
                    index++;
                }

                if (gefundeneArtikel == 0)
                {
                    Console.WriteLine("Keine Artikel in dieser Kategorie gefunden.");
                }
            }
            else
            {
                Console.WriteLine("Ungültige Eingabe!");
            }
        }

        // Sucht Artikel nach Autor
        // <param name="articles">Liste der Artikel</param>
        private static void SearchArticlesByAuthor(List<Article> articles)
        {
            Console.WriteLine("=== Artikel nach Autor suchen ===");
            Console.Write("Geben Sie den Autornamen ein: ");
            string? suchbegriff = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(suchbegriff))
            {
                Console.WriteLine("Ungültiger Suchbegriff!");
                return;
            }

            Console.WriteLine($"\n=== Suchergebnisse für '{suchbegriff}' ===");
            int index = 0;
            int gefundeneArtikel = 0;
            while (index < articles.Count)
            {
                if (articles[index].Author.Contains(suchbegriff, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"{gefundeneArtikel + 1}. {articles[index].ToString()}");
                    gefundeneArtikel++;
                }
                index++;
            }

            if (gefundeneArtikel == 0)
            {
                Console.WriteLine("Keine Artikel von diesem Autor gefunden.");
            }
        }

        // Filtert Bücher nach Lesezeit
        // <param name="articles">Liste der Artikel</param>
        private static void FilterArticlesByReadingTime(List<Article> articles)
        {
            Console.WriteLine("=== Bücher nach Lesezeit filtern ===");
            Console.WriteLine("Hinweis: Lesezeit ist nur für Bücher relevant.");
            Console.Write("Geben Sie die maximale Lesezeit in Minuten ein: ");

            if (int.TryParse(Console.ReadLine(), out int maxLesezeit))
            {
                Console.WriteLine($"\n=== Bücher mit Lesezeit <= {maxLesezeit} Minuten ===");
                int index = 0;
                int gefundeneArtikel = 0;
                while (index < articles.Count)
                {
                    if (articles[index].Category1 == Category.Book && articles[index].Lesezeit.HasValue && articles[index].Lesezeit.Value <= maxLesezeit)
                    {
                        Console.WriteLine($"{gefundeneArtikel + 1}. {articles[index].ToString()}");
                        gefundeneArtikel++;
                    }
                    index++;
                }

                if (gefundeneArtikel == 0)
                {
                    Console.WriteLine($"Keine Bücher mit Lesezeit <= {maxLesezeit} Minuten gefunden.");
                }
            }
            else
            {
                Console.WriteLine("Ungültige Eingabe!");
            }
        }

        // Hilfsmethode zum Hinzufügen eines neuen Artikels
        // <param name="fileManager">FileManager-Instanz</param>
        // <param name="articles">Liste der Artikel</param>
        private static void AddNewArticle(FileManager fileManager, List<Article> articles)
        {
            try
            {
                Console.WriteLine("\n=== Neuen Artikel hinzufügen ===");
                
                // ID automatisch generieren (höchste vorhandene ID + 1)
                int neueId = articles.Count > 0 ? articles.Max(a => a.Id) + 1 : 1;
                
                Console.Write("Titel: ");
                string titel = Console.ReadLine() ?? "";
                
                Console.Write("Autor: ");
                string autor = Console.ReadLine() ?? "";
                
                Console.Write("Preis: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal preis))
                {
                    Console.WriteLine("Ungültiger Preis! Verwende 0.00.");
                    preis = 0.00m;
                }
                
                Console.Write("Beschreibung: ");
                string beschreibung = Console.ReadLine() ?? "";
                
                Console.WriteLine("Verfügbare Kategorien:");
                Console.WriteLine("1. Book");
                Console.WriteLine("2. Laptop");
                Console.WriteLine("3. Shoes");
                Console.WriteLine("4. Handy");
                Console.WriteLine("5. NotSpecified");
                Console.Write("Kategorie (1-5): ");
                
                Category category = Category.NotSpecified;
                if (int.TryParse(Console.ReadLine(), out int categoryChoice))
                {
                    category = categoryChoice switch
                    {
                        1 => Category.Book,
                        2 => Category.Laptop,
                        3 => Category.Shoes,
                        4 => Category.Handy,
                        5 => Category.NotSpecified,
                        _ => Category.NotSpecified
                    };
                }
                
                Console.Write("Veröffentlichungsdatum (yyyy-mm-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime publishDate))
                {
                    Console.WriteLine("Ungültiges Datum! Verwende aktuelles Datum.");
                    publishDate = DateTime.Now;
                }
                
                int lesezeit = 0;
                if (category == Category.Book)
                {
                    Console.Write("Lesezeit (Minuten): ");
                    if (!int.TryParse(Console.ReadLine(), out lesezeit))
                    {
                        Console.WriteLine("Ungültige Lesezeit! Verwende 30.");
                        lesezeit = 30;
                    }
                }
                
                Article newArticle = new Article(neueId, titel, autor, preis, beschreibung, publishDate, lesezeit, category);
                
                // Artikel zur lokalen Liste hinzufügen
                articles.Add(newArticle);
                
                // Artikel zur Datei hinzufügen
                fileManager.AddArticle(newArticle);
                
                Console.WriteLine("Artikel erfolgreich hinzugefügt!");
                Console.WriteLine(newArticle.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Hinzufügen des Artikels: {ex.Message}");
            }
        }
    }
}