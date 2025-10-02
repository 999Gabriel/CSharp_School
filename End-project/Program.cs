using End_project.Models;

namespace End_project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Artikelverwaltungssystem mit Filemanagement ===");
            Console.WriteLine();

            // FileManager für Artikel erstellen
            FileManager fileManager = new FileManager("articles.txt");

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
                Console.WriteLine("5. Artikel nach Lesezeit filtern");
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

        /// <summary>
        /// Zeigt alle Artikel an
        /// </summary>
        /// <param name="articles">Liste der Artikel</param>
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

        /// <summary>
        /// Filtert Artikel nach Kategorie
        /// </summary>
        /// <param name="articles">Liste der Artikel</param>
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

        /// <summary>
        /// Sucht Artikel nach Autor
        /// </summary>
        /// <param name="articles">Liste der Artikel</param>
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

        /// <summary>
        /// Filtert Artikel nach Lesezeit
        /// </summary>
        /// <param name="articles">Liste der Artikel</param>
        private static void FilterArticlesByReadingTime(List<Article> articles)
        {
            Console.WriteLine("=== Artikel nach Lesezeit filtern ===");
            Console.Write("Geben Sie die maximale Lesezeit in Minuten ein: ");

            if (int.TryParse(Console.ReadLine(), out int maxLesezeit))
            {
                Console.WriteLine($"\n=== Artikel mit Lesezeit <= {maxLesezeit} Minuten ===");
                int index = 0;
                int gefundeneArtikel = 0;
                while (index < articles.Count)
                {
                    if (articles[index].Lesezeit <= maxLesezeit)
                    {
                        Console.WriteLine($"{gefundeneArtikel + 1}. {articles[index].ToString()}");
                        gefundeneArtikel++;
                    }
                    index++;
                }

                if (gefundeneArtikel == 0)
                {
                    Console.WriteLine($"Keine Artikel mit Lesezeit <= {maxLesezeit} Minuten gefunden.");
                }
            }
            else
            {
                Console.WriteLine("Ungültige Eingabe!");
            }
        }

        /// <summary>
        /// Hilfsmethode zum Hinzufügen eines neuen Artikels
        /// </summary>
        /// <param name="fileManager">FileManager-Instanz</param>
        /// <param name="articles">Liste der Artikel</param>
        private static void AddNewArticle(FileManager fileManager, List<Article> articles)
        {
            try
            {
                Console.WriteLine("\n=== Neuen Artikel hinzufügen ===");
                
                Console.Write("Titel: ");
                string titel = Console.ReadLine() ?? "";
                
                Console.Write("Autor: ");
                string autor = Console.ReadLine() ?? "";
                
                Console.Write("Veröffentlichungsdatum (yyyy-mm-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime publishDate))
                {
                    Console.WriteLine("Ungültiges Datum! Verwende aktuelles Datum.");
                    publishDate = DateTime.Now;
                }
                
                Console.Write("Lesezeit (Minuten): ");
                if (!int.TryParse(Console.ReadLine(), out int lesezeit))
                {
                    Console.WriteLine("Ungültige Lesezeit! Verwende 0.");
                    lesezeit = 0;
                }
                
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
                
                Article newArticle = new Article(titel, autor, publishDate, lesezeit, category);
                
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