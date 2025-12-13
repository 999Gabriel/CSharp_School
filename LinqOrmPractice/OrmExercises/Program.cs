using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrmExercises.Models;
using OrmExercises;
namespace OrmExercises
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("========== ORM ÜBUNGEN (EF Core) ==========");

            /*
             * THEORIE FRAGE 1:
             * Was ist ein ORM und welche Vorteile bietet es gegenüber reinem SQL?
             * 
             * Antwort:
             * ORM ist ein Object Relational Mapper, der es ermöglicht, Datenbankoperationen
             * in Form von Objekten und Klassen durchzuführen, anstatt direkt SQL-Abfragen
             * zu schreiben. Vorteile sind:
             * - Abstraktion der Datenbanklogik
             * - Automatisches Mapping zwischen Objekten und Datenbanktabellen
             * - Erleichterte Wartbarkeit und Lesbarkeit des Codes
             * - Plattformunabhängigkeit durch Unterstützung verschiedener Datenbanksysteme
             */

            /*
             * THEORIE FRAGE 2:
             * Was sind Migrations in EF Core und wofür werden die Befehle 'add-migration' und 'update-database' verwendet?
             * 
             * Antwort:
             * sie sind eine Möglichkeit, Änderungen am Datenbankschema zu verwalten und zu versionieren.
             * - 'add-migration': Erstellt eine neue Migrationsklasse basierend auf den Änderungen im Datenmodell.
             * - 'update-database': Wendet die ausstehenden Migrationen auf die Datenbank an und aktualisiert das Schema entsprechend.
             */

            using (var context = new AppDbContext())
            {
                // Sicherstellen, dass die Datenbank existiert (nur für Übungszwecke, normalerweise Migrations nutzen)
                await context.Database.EnsureCreatedAsync();

                // AUFGABE 1: Erstelle eine neue Kategorie "Electronics" und speichere sie in der DB.
                Console.WriteLine("\n--- Aufgabe 1: Kategorie erstellen ---");
                // Dein Code hier:
                
                var category = new Category
                {
                    Name = "Electronics"
                };
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();
                Console.WriteLine($"Kategorie erstellt mit ID: {category.CategoryId}");


                // AUFGABE 2: Füge der Kategorie "Electronics" zwei Produkte hinzu ("Laptop", 1000€) und ("Mouse", 50€).
                Console.WriteLine("\n--- Aufgabe 2: Produkte hinzufügen ---");
                // Dein Code hier:
                // 
                var Laptop = new Product
                {
                    Name = "Laptop",
                    Price = 1000m,
                    // Verknüpfung zur Kategorie über CategoryId zu Electoronics wessen id 1 ist
                    CategoryId = category.CategoryId
                };
                await context.Products.AddAsync(Laptop);
                await context.SaveChangesAsync();
                Console.WriteLine($"Produkt mit Kategorie-ID hinzugefügt: {Laptop.Name}, Preis: {Laptop.Price}");

                var Mouse = new Product()
                {
                    Name = "Mouse",
                    Price = 50m,
                    CategoryId = category.CategoryId
                };
                await context.Products.AddAsync(Mouse);
                await context.SaveChangesAsync();
                Console.WriteLine($"Produkt mit Kategorie-ID hinzugefügt: {Mouse.Name}, Preis: {Mouse.Price}");
                
                
                /*
                 * THEORIE FRAGE 3:
                 * Wie funktioniert Lazy Loading vs. Eager Loading? Was macht .Include()?
                 * 
                 * Antwort:
                 * Lazy loading lädt verwandte Daten erst, wenn sie explizit angefordert werden,
                 * während Eager Loading verwandte Daten sofort mit der Hauptabfrage lädt.
                 * 
                 */
                
                // AUFGABE 3: Lade die Kategorie "Electronics" inklusive ihrer Produkte (Eager Loading) und gib sie aus.
                Console.WriteLine("\n--- Aufgabe 3: Kategorie mit Produkten laden ---");
                // Dein Code hier:
                var loadedCategory = await context.Categories
                    .Include(c => c.Products)
                    .Where(c => c.CategoryId == 3) // Angenommen, die ID der Electronics-Kategorie ist 3
                    .FirstOrDefaultAsync();
                
                if (loadedCategory != null)
                {
                    Console.WriteLine($"Kategorie: {loadedCategory.Name}");
                    foreach (var product in loadedCategory.Products)
                    {
                        Console.WriteLine($"- Produkt: {product.Name}, Preis: {product.Price}€");
                    }
                }
                else
                {
                    Console.WriteLine("Kategorie 'Electronics' nicht gefunden.");
                }
                
                // AUFGABE 4: Aktualisiere den Preis der "Mouse" auf 45€.
                Console.WriteLine("\n--- Aufgabe 4: Preis aktualisieren ---");
                // Dein Code hier:
                var mouseProduct = await context.Products
                    .Where(p => p.Name == "Mouse")
                    .FirstOrDefaultAsync();
                if (mouseProduct != null)
                {
                    mouseProduct.Price = 45m;
                    await context.SaveChangesAsync();
                    Console.WriteLine($"Preis der 'Mouse' aktualisiert auf: {mouseProduct.Price}€");
                }
                else
                {
                    Console.WriteLine("Produkt 'Mouse' nicht gefunden.");
                }

                // AUFGABE 5: Lösche das Produkt "Laptop".
                Console.WriteLine("\n--- Aufgabe 5: Produkt löschen ---");
                // Dein Code hier:
                var laptopProduct = await context.Products
                    .Where(p => p.Name == "Laptop")
                    .FirstOrDefaultAsync();
                if (laptopProduct != null)
                {
                    context.Products.Remove(laptopProduct);
                    await context.SaveChangesAsync();
                    Console.WriteLine("Produkt 'Laptop' wurde gelöscht.");
                }
                else
                {
                    Console.WriteLine("Produkt 'Laptop' nicht gefunden.");
                }

                /*
                 * THEORIE FRAGE 4:
                 * Wie werden m:n Beziehungen in EF Core abgebildet? (Stichwort: Zwischentabelle)
                 *
                 * Antwort:
                 * m:n Beziehungen werden in EF Core durch die Verwendung einer Zwischentabelle (Join-Tabelle)
                 * eine Zwischentabelle enthält Fremdschlüssel zu beiden beteiligten Tabellen und
                 * ermöglicht so die Verknüpfung mehrerer Einträge aus beiden Tabellen.
                 *
                 * bedeutet code technisch, dass man eine zusätzliche Klasse (z.B. InvoiceArticle) erstellt,
                 * die die beiden Fremdschlüssel als Eigenschaften enthält und in der DbContext-Klasse
                 * eine DbSet für diese Zwischentabelle definiert.
                 * 
                 *
                 */

                // AUFGABE 6: Erstelle eine Order und füge die "Mouse" 2 mal hinzu (m:n Beziehung).
                Console.WriteLine("\n--- Aufgabe 6: Order erstellen (m:n) ---");
                // Dein Code hier:
                
                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    OrderProducts = new System.Collections.Generic.List<OrderProduct>
                    {
                        new OrderProduct
                        {
                            ProductId = mouseProduct.ProductId,
                            Quantity = 2
                        }
                    }
                };
                await context.Orders.AddAsync(order);
                await context.SaveChangesAsync();
                Console.WriteLine($"Order erstellt mit ID: {order.OrderId}");
            }
            

            Console.WriteLine("\nDrücke eine Taste zum Beenden...");
            Console.ReadKey();
        }
    }
}
