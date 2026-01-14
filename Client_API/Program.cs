using System.Net.Http.Json;
using ORM.models;
namespace consoleApp
{
    internal class Program
    {
        /*
         * zum Zugriff auf eine WebAPI wird in C# die Klasse HttpClient verwendet
         *
         *
         * sie besitzt zb.: folgende Methoden
         *
         *  - PostAsync() ... hier müssen wir z.B.: ein Artikelobjekt selber nach Json serialisieren 
         *  - PostAsJsonAsync() ... hier wird das Serialisieren für uns übernommen
         *  - GetAsync() ... 
         *  - GetFromJsonAsync() ...
         *  - ...
         */
       
        private static readonly HttpClient _client = new();
        

        static async Task Main(string[] args)
        {
            InitHttpClient();
            /*
            Console.WriteLine("=================== Create New Article ===================");
            var art = CreateNewArticleAsync(new()
            {
            ArticleId = 0,
            Title = "Airpods",
            Name = "Airpods Pro",
            Price = 190.00m,
            });
            if (art is null)
            {
                Console.WriteLine("Probleme beim Erzeugen des Artikels!");
            }
            else
            {
                Console.WriteLine($"{art.Result.ArticleId} - {art.Result.Title} - {art.Result.Price}");
            }
            
            Console.WriteLine("=================== Update Article ===================");
            var artToUpdate = new Article()
            {
                ArticleId = 303, // ID des zu aktualisierenden Artikels
                Title = "Airpods Updated",
                Name = "Airpods Pro Updated",
                Price = 200.00m,
            };
            var updatedArt = await UpdateArticle(artToUpdate);
            if (updatedArt is null)
            {
                Console.WriteLine("Probleme beim Aktualisieren des Artikels!");
            }
            else
            {
                Console.WriteLine($"Aktualisierter Artikel: {updatedArt.ArticleId} - {updatedArt.Title} - {updatedArt.Price}");
            }
            

            Console.WriteLine("=================== Alle Artikel aus der WebAPI ===================");
            var allArticles = await GetAllArticles();
            if (allArticles != null)
            {
                Console.WriteLine($"FOUND {allArticles.Count} articles: ");

                foreach (var article in allArticles)
                {
                    Console.WriteLine($" ID: {article.ArticleId} | Title: {article.Title} | Price: {article.Price}");
                }
            }
            else
            {
                Console.WriteLine("Error: Coud not retrieve articles.");
            }
            
            Console.WriteLine("\n--- Einzelnen Artikel abrufen (ID: 303) ---");
            
            // Replace '1' with an ID that actually exists in your database
            var singleArticle = await GetArticleById(303);
            
            if (singleArticle != null)
            {
                Console.WriteLine($"Gefunden: {singleArticle.ArticleId} - {singleArticle.Title} - {singleArticle.Price}");
            }
            else
            {
                Console.WriteLine("Artikel mit dieser ID wurde nicht gefunden.");
            }
            
            Console.WriteLine("=================== Delete Article mit ID ===================");
            var articleIdToDelete = 7; // ID des zu löschenden Artikels
            bool deleteResult = await DeleteArticle(articleIdToDelete);
            if (deleteResult)
            {
                Console.WriteLine($"Artikel mit ID {articleIdToDelete} wurde erfolgreich gelöscht.");
            }
            else
            {
                Console.WriteLine($"Fehler beim Löschen des Artikels mit ID {articleIdToDelete}.");
            }
            */
        }

        static void InitHttpClient()
        {
            _client.BaseAddress = new Uri("https://localhost:7133/api/Article/");
        }

        private static async Task<Article?> CreateNewArticleAsync(Article article)
        {
            HttpResponseMessage responseMessage = await _client.PostAsJsonAsync<Article>("", article);

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.Created)
            {
                // Artikel wurde erfolgreich erzeugt
                // hier wird der JSON String in ein Article-Objekt deserialisiert
                var a = await responseMessage.Content.ReadFromJsonAsync<Article>();
                return a;
            }
            else 
            {
                return null;
            }
        }
        
        // alle restlichen Methoden (Get, Put, Delete, ...) programmieren (in Main)
        // UpdateArticle

        private static async Task<Article?> UpdateArticle(Article article)
        {
            // Fix 1: Append the ID to the URL (BaseAddress + ID)
            HttpResponseMessage responseMessage = await _client.PutAsJsonAsync<Article>($"{article.ArticleId}", article);
    
            // Fix 2: Handle success/failure without crashing immediately
            if (!responseMessage.IsSuccessStatusCode)
            {
                return null; 
            }

            // Fix 3: Standard PUT often returns 204 (No Content). Only read if there is content.
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return article; // Success, but server sent no data back
            }

            // If server returned the updated object (Status 200 OK)
            return await responseMessage.Content.ReadFromJsonAsync<Article>();
        }
        // GetAllArticles
        // Change return type to a List<Article>
        private static async Task<List<Article>?> GetAllArticles(string path = "")
        {
            HttpResponseMessage responseMessage = await _client.GetAsync(path);

            if (responseMessage.IsSuccessStatusCode)
            {
                // Deserialization must match the JSON Array returned by the API
                var articles = await responseMessage.Content.ReadFromJsonAsync<List<Article>>();
                return articles;
            }
            else 
            {
                return null;
            }
        }

        // GetArticleById
        private static async Task<Article?> GetArticleById(int id)
        {
            Article? article = null;
        
            // Send the GET request to the path (BaseAddress + ID)
            // Example: https://localhost:7133/api/Article/5
            HttpResponseMessage response = await _client.GetAsync(id.ToString());
        
            // Check if the status code is in the 200-299 range
            if (response.IsSuccessStatusCode)
            {
                // Deserialize the JSON body into an Article object
                article = await response.Content.ReadFromJsonAsync<Article>();
            }
        
            return article;
        }
        // DeleteArticle
        private static async Task<bool> DeleteArticle(int id)
        {
            // Send the DELETE request to the path (BaseAddress + ID)
            HttpResponseMessage response = await _client.DeleteAsync(id.ToString());

            // Return true if the status code indicates success (2xx)
            return response.IsSuccessStatusCode;
        }
    }
}