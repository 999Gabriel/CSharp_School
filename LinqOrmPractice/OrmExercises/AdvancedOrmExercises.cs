using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrmExercises.Models;

namespace OrmExercises
{
    public class AdvancedOrmExercises
    {
        public static async Task RunAsync()
        {
            Console.WriteLine("\n========== FORTGESCHRITTENE ORM ÜBUNGEN ==========");

            using (var context = new AppDbContext())
            {
                // Setup: Clean DB
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();

                // Seed Data
                var electronics = new Category { Name = "Electronics" };
                var books = new Category { Name = "Books" };
                await context.Categories.AddRangeAsync(electronics, books);

                var p1 = new Product { Name = "High-End Laptop", Price = 2500, Category = electronics };
                var p2 = new Product { Name = "Budget Laptop", Price = 500, Category = electronics };
                var p3 = new Product { Name = "Smartphone", Price = 800, Category = electronics };
                var p4 = new Product { Name = "C# Programming", Price = 45, Category = books };
                await context.Products.AddRangeAsync(p1, p2, p3, p4);

                await context.SaveChangesAsync();

                /*
                 * AUFGABE 1: Transaktionen
                 * Szenario: Wir wollen den Preis aller "Electronics"-Produkte um 10% erhöhen.
                 * Wenn dabei ein Fehler auftritt (simuliert), soll KEINE Änderung gespeichert werden.
                 *
                 * Aufgabe: Implementiere eine explizite Transaktion (BeginTransactionAsync).
                 * Erhöhe die Preise. Wirf dann eine Exception, um einen Fehler zu simulieren.
                 * Fange die Exception und führe ein Rollback durch.
                 * Überprüfe am Ende, ob die Preise unverändert sind.
                 */
                Console.WriteLine("\n--- Aufgabe 1: Transaktionen & Rollback ---");
                // Dein Code hier:
                var electronicsProducts = await context.Products.ToListAsync();
                using (var transaction = await context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        foreach (var product in electronicsProducts)
                        {
                            if (product.Category.Name == "Electronics")
                            {
                                product.Price *= 1.1m; // 10% Erhöhung
                                context.Products.Update(product);
                                await context.SaveChangesAsync();
                                Console.WriteLine(product.Price);
                            }

                        }
                        // FEEDBACK:
                        // Du hast vergessen, die Exception zu werfen, um den Fehler zu simulieren!
                        // Der Code läuft erfolgreich durch, das Rollback wird nie getestet.
                        // throw new Exception("Simulierter Fehler!");
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        Console.WriteLine("Transaktion fehlgeschlagen. Änderungen wurden zurückgerollt.");
                    }
                }

                /*
                 * AUFGABE 2: Komplexe Abfragen & Projektion (DTOs)
                 * Aufgabe: Finde die Top 2 teuersten Produkte aus der Kategorie "Electronics".
                 * Projiziere das Ergebnis in ein anonymes Objekt (oder DTO) mit den Eigenschaften:
                 * - ProductName
                 * - CategoryName
                 * - FormattedPrice (String, z.B. "2.500,00 €")
                 */
                Console.WriteLine("\n--- Aufgabe 2: Projektion & Top N ---");
                // Dein Code hier:
                // FEEDBACK:
                // 1. Performance: Du lädst erst ALLES (ToListAsync) und filterst dann im Speicher.
                //    Das ist bei großen Datenbanken sehr langsam. Besser: Erst filtern, dann laden.
                // 2. Projektion: Du hast nur den Preis ausgewählt. Gefragt war ein Objekt mit Name, Kategorie und Preis.
                //
                // KORREKTUR:
                /*
                var top2Expensive = await context.Products
                    .Where(p => p.Category.Name == "Electronics")
                    .OrderByDescending(p => p.Price)
                    .Take(2)
                    .Select(p => new 
                    {
                        ProductName = p.Name,
                        CategoryName = p.Category.Name,
                        FormattedPrice = p.Price.ToString("C")
                    })
                    .ToListAsync();
                */
                var mostExpensiveElectronics = await context.Products.ToListAsync();
                var top2Expensive = mostExpensiveElectronics
                    .Where(p => p.Category.Name == "Electronics")
                    .OrderByDescending(p => p.Price)
                    .Take(2)
                    .Select(p => p.Price)
                    .ToList();

                foreach (var price in top2Expensive)
                {
                    Console.WriteLine($"Formatted Price: {price:N2} €");
                }

                /*
                 * AUFGABE 3: N+1 Problem verstehen
                 * Aufgabe: Lade alle Kategorien und iteriere über sie.
                 * Gib für jede Kategorie den Namen und die Anzahl der Produkte aus.
                 *
                 * THEORIE: Warum könnte der naive Ansatz (ohne Include) hier ineffizient sein?
                 * Wie löst man das effizient? (Implementiere die effiziente Lösung)
                 */
                Console.WriteLine("\n--- Aufgabe 3: N+1 Problem & Aggregation ---");
                // Dein Code hier:
                // FEEDBACK:
                // Du hast genau das N+1 Problem implementiert, statt es zu lösen!
                // In der Schleife rufst du für JEDE Kategorie erneut die Datenbank auf (await context.Products.CountAsync...).
                // Bei 100 Kategorien sind das 101 Datenbankabfragen.
                //
                // KORREKTUR (Lösung mit Projektion):
                /*
                var stats = await context.Categories
                    .Select(c => new { c.Name, Count = c.Products.Count })
                    .ToListAsync();
                */
                var categoriesWithProducts = await context.Categories.ToListAsync();
                foreach (var category in categoriesWithProducts)
                {
                    var categoryName = category.Name;
                    var productCount = await context.Products.CountAsync(p => p.CategoryId == category.CategoryId);
                    Console.WriteLine($"Category: {categoryName}, Product Count: {productCount}");
                }

                /*
                 * AUFGABE 4: Raw SQL
                 * Aufgabe: Manchmal ist LINQ zu langsam oder limitiert.
                 * Führe ein rohes SQL-Update aus, das alle Produkte, die "Laptop" im Namen haben,
                 * als "Ausverkauft" markiert (füge dazu " [SOLD OUT]" an den Namen an).
                 * Nutze context.Database.ExecuteSqlRawAsync().
                 */
                Console.WriteLine("\n--- Aufgabe 4: Raw SQL Update ---");
                // Dein Code hier:
                // FEEDBACK:
                // Die Idee ist richtig!
                // Hinweis: In MySQL funktioniert '+' für String-Konkatenation oft nicht (ergibt 0).
                // Besser: CONCAT(Name, ' [SOLD OUT]')
                var sql = "UPDATE Products SET Name = Name + ' [SOLD OUT]' WHERE Name LIKE '%Laptop%'";
                var affectedRows = await context.Database.ExecuteSqlRawAsync(sql);
                Console.WriteLine($"Anzahl der aktualisierten Produkte: {affectedRows}");

            }
        }
    }
}
