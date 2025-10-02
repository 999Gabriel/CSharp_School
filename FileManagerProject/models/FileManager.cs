namespace FileManagerProject.Models;

public class FileManager
    {
        private string _filePath;

        public FileManager(string filePath)
        {
            _filePath = filePath;
        }

        // Lädt Artikel aus der Textdatei und gibt eine Liste von Article-Objekten zurück
        // <returns>Liste von Article-Objekten</returns>
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

        // Parst eine Zeile aus der Textdatei und erstellt ein Article-Objekt
        // Format: ID;Titel;Autor;Preis;Beschreibung;Kategorie
        // <param name="line">Zeile aus der Textdatei</param>
        // <returns>Article-Objekt oder null bei Fehlern</returns>
        private Article? ParseArticleFromLine(string line)
        {
            try
            {
                string[] parts = line.Split(';');

                if (parts.Length != 6)
                {
                    Console.WriteLine($"Ungültiges Format in Zeile: {line}");
                    return null;
                }

                if (!int.TryParse(parts[0].Trim(), out int id))
                {
                    Console.WriteLine($"Ungültige ID in Zeile: {line}");
                    return null;
                }

                string titel = parts[1].Trim();
                string autor = parts[2].Trim();
                
                if (!decimal.TryParse(parts[3].Trim(), out decimal preis))
                {
                    Console.WriteLine($"Ungültiger Preis in Zeile: {line}");
                    return null;
                }

                string beschreibung = parts[4].Trim();

                if (!Enum.TryParse<Category>(parts[5].Trim(), out Category category))
                {
                    Console.WriteLine($"Ungültige Kategorie in Zeile: {line}");
                    return null;
                }

                // Für die Lesezeit verwenden wir einen Standardwert nur für Bücher, da sie nicht in der Datei steht
                int lesezeit = category == Category.Book ? 30 : 0; // Standard-Lesezeit von 30 Minuten für Bücher
                DateTime publishDate = DateTime.Now; // Aktuelles Datum als Standard

                return new Article(id, titel, autor, preis, beschreibung, publishDate, lesezeit, category);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Parsen der Zeile '{line}': {ex.Message}");
                return null;
            }
        }

        // Speichert Artikel in die Textdatei
        // <param name="articles">Liste von Artikeln zum Speichern</param>
        public void SaveArticles(List<Article> articles)
        {
            try
            {
                List<string> lines = new List<string>();

                foreach (Article article in articles)
                {
                    string line = $"{article.Id};{article.Titel};{article.Author};{article.Preis:F2};{article.Beschreibung};{article.Category1}";
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

        // Fügt einen neuen Artikel zur Datei hinzu
        // <param name="article">Neuer Artikel</param>
        public void AddArticle(Article article)
        {
            try
            {
                string line = $"{article.Id};{article.Titel};{article.Author};{article.Preis:F2};{article.Beschreibung};{article.Category1}";
                File.AppendAllText(_filePath, line + Environment.NewLine);
                Console.WriteLine("Artikel erfolgreich hinzugefügt.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Hinzufügen des Artikels: {ex.Message}");
            }
        }
    }