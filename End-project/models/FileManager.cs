using End_project.Models;

namespace End_project.Models
{
    public class FileManager
    {
        private string _filePath;

        public FileManager(string filePath)
        {
            _filePath = filePath;
        }

        /// <summary>
        /// Lädt Artikel aus der Textdatei und gibt eine Liste von Article-Objekten zurück
        /// </summary>
        /// <returns>Liste von Article-Objekten</returns>
        public List<Article> LoadArticles()
        {
            List<Article> articles = new List<Article>();

            try
            {
                if (!File.Exists(_filePath))
                {
                    Console.WriteLine($"Datei {_filePath} nicht gefunden!");
                    return articles;
                }

                string[] lines = File.ReadAllLines(_filePath);

                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    Article article = ParseArticleFromLine(line);
                    if (article != null)
                    {
                        articles.Add(article);
                    }
                }

                Console.WriteLine($"{articles.Count} Artikel erfolgreich geladen.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Laden der Artikel: {ex.Message}");
            }

            return articles;
        }

        /// <summary>
        /// Parst eine Zeile aus der Textdatei und erstellt ein Article-Objekt
        /// Format: Titel|Autor|Datum|Lesezeit|Kategorie
        /// </summary>
        /// <param name="line">Zeile aus der Textdatei</param>
        /// <returns>Article-Objekt oder null bei Fehlern</returns>
        private Article? ParseArticleFromLine(string line)
        {
            try
            {
                string[] parts = line.Split('|');

                if (parts.Length != 5)
                {
                    Console.WriteLine($"Ungültiges Format in Zeile: {line}");
                    return null;
                }

                string titel = parts[0].Trim();
                string autor = parts[1].Trim();
                
                if (!DateTime.TryParse(parts[2].Trim(), out DateTime publishDate))
                {
                    Console.WriteLine($"Ungültiges Datum in Zeile: {line}");
                    return null;
                }

                if (!int.TryParse(parts[3].Trim(), out int lesezeit))
                {
                    Console.WriteLine($"Ungültige Lesezeit in Zeile: {line}");
                    return null;
                }

                if (!Enum.TryParse<Category>(parts[4].Trim(), out Category category))
                {
                    Console.WriteLine($"Ungültige Kategorie in Zeile: {line}");
                    return null;
                }

                return new Article(titel, autor, publishDate, lesezeit, category);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Parsen der Zeile '{line}': {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Speichert Artikel in die Textdatei
        /// </summary>
        /// <param name="articles">Liste von Artikeln zum Speichern</param>
        public void SaveArticles(List<Article> articles)
        {
            try
            {
                List<string> lines = new List<string>();

                foreach (Article article in articles)
                {
                    string line = $"{article.Titel}|{article.Author}|{article.Veröffentlichungsdatum:yyyy-MM-dd}|{article.Lesezeit}|{article.Category1}";
                    lines.Add(line);
                }

                File.WriteAllLines(_filePath, lines);
                Console.WriteLine($"{articles.Count} Artikel erfolgreich gespeichert.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Speichern der Artikel: {ex.Message}");
            }
        }

        /// <summary>
        /// Fügt einen neuen Artikel zur Datei hinzu
        /// </summary>
        /// <param name="article">Neuer Artikel</param>
        public void AddArticle(Article article)
        {
            try
            {
                string line = $"{article.Titel}|{article.Author}|{article.Veröffentlichungsdatum:yyyy-MM-dd}|{article.Lesezeit}|{article.Category1}";
                File.AppendAllText(_filePath, line + Environment.NewLine);
                Console.WriteLine("Artikel erfolgreich hinzugefügt.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Hinzufügen des Artikels: {ex.Message}");
            }
        }
    }
}
