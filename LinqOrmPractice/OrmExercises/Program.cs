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
             * ORM ist die Abkürzung für Object-Relational Mapping. Es ist eine Technik, die es ermöglicht,
             * Objekte in einer objektorientierten Programmiersprache (wie C#) mit relationalen Datenbanken zu verbinden.
             * Vorteile:
             * 1. Abstraktion: Entwickler können mit Objekten arbeiten, anstatt SQL-Abfragen zu schreiben.
             * 2. Produktivität: Schnellere Entwicklung durch weniger Boilerplate-Code.
             * 3. Wartbarkeit: Änderungen am Datenmodell sind einfacher umzusetzen.
             * 4. Portabilität: Einfacher Wechsel zwischen verschiedenen Datenbanksystemen.
             * 5. Sicherheit: ORM-Frameworks bieten Schutz vor SQL-Injection-Angriffen.
             *
             */

            /*
             * THEORIE FRAGE 2:
             * Was sind Migrations in EF Core und wofür werden die Befehle 'add-migration' und 'update-database' verwendet?
             *
             * Antwort:
             * Migrations sind ein Mechanismus in EF Core, um Änderungen am Datenbankschema zu verwalten.
             * 'add-migration' wird verwendet, um eine neue Migration zu erstellen, die die Änderungen am Datenmodell beschreibt.
             * 'update-database' wird verwendet, um die Datenbank
             * auf den neuesten Stand zu bringen, indem die ausstehenden Migrationen angewendet werden.
             */

            using (var context = new AppDbContext())
            {
                // Sicherstellen, dass die Datenbank existiert (nur für Übungszwecke, normalerweise Migrations nutzen)
                await context.Database.EnsureCreatedAsync();

                // AUFGABE 1: Erstelle eine neue Kategorie "Electronics" und speichere sie in der DB.
                Console.WriteLine("\n--- Aufgabe 1: Kategorie erstellen ---");
                // Dein Code hier:
                Category category = new Category()
                {
                    Name = "Electronics",
                    CategoryId = 1
                };
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();
                if (category.CategoryId > 0)
                {
                    Console.WriteLine($"Kategorie '{category.Name}' mit ID {category.CategoryId} wurde erstellt.");
                }
                else
                {
                    Console.WriteLine("Fehler beim Erstellen der Kategorie.");
                }


                // AUFGABE 2: Füge der Kategorie "Electronics" zwei Produkte hinzu ("Laptop", 1000€) und ("Mouse", 50€).
                Console.WriteLine("\n--- Aufgabe 2: Produkte hinzufügen ---");
                // Dein Code hier:
                var products = await context.Products.ToListAsync();
                Product laptop = new Product()
                {
                    Name = "Laptop",
                    Price = 1000m,
                    CategoryId = category.CategoryId
                };
                Product mouse = new Product()
                {
                    Name = "Mouse",
                    Price = 50m,
                    CategoryId = category.CategoryId
                };
                await context.Products.AddRangeAsync(laptop, mouse);
                await context.SaveChangesAsync();
                Console.WriteLine(
                    $"Produkte '{laptop.Name}' und '{mouse.Name}' wurden der Kategorie '{category.Name}' hinzugefügt.");


                /*
                 * THEORIE FRAGE 3:
                 * Wie funktioniert Lazy Loading vs. Eager Loading? Was macht .Include()?
                 *
                 * Antwort:
                 * Lazy Loading lädt verwandte Daten erst, wenn sie explizit angefordert werden.
                 * Eager Loading lädt verwandte Daten sofort mit der Hauptabfrage.
                 * .Include() wird verwendet, um Eager Loading zu implementieren, indem es angibt,
                 * welche verwandten Entitäten zusammen mit der Hauptentität geladen werden sollen.
                 */

                // AUFGABE 3: Lade die Kategorie "Electronics" inklusive ihrer Produkte (Eager Loading) und gib sie aus.
                Console.WriteLine("\n--- Aufgabe 3: Kategorie mit Produkten laden ---");
                // Dein Code hier:
                var electronicsCategory =
                    await context.Categories.FirstOrDefaultAsync(((c => c.Name == "Elektronics")));
                if (electronicsCategory != null)
                {
                    Console.WriteLine($"Category: {electronicsCategory.Name}");
                    var electronicsProducts = await context.Products
                        .Where(p => p.CategoryId == electronicsCategory.CategoryId)
                        .ToListAsync();
                    foreach (var product in electronicsProducts)
                    {
                        Console.WriteLine($" - Product: {product.Name}, Price: {product.Price}€");
                    }
                }

                // AUFGABE 4: Aktualisiere den Preis der "Mouse" auf 45€.
                Console.WriteLine("\n--- Aufgabe 4: Preis aktualisieren ---");
                // Dein Code hier:
                var mouseProduct = await context.Products.FirstOrDefaultAsync(p => p.Name == "Mouse");
                if (mouseProduct != null)
                {
                    mouseProduct.Price = mouseProduct.Price * 1.2m; // 20% Erhöhung
                    await context.SaveChangesAsync();
                    Console.WriteLine($"Der neue Preis der '{mouseProduct.Name}' ist {mouseProduct.Price}€.");
                }
                else
                {
                    Console.WriteLine("Produkt 'Mouse' nicht gefunden.");
                }

                // AUFGABE 5: Lösche das Produkt "Laptop".
                Console.WriteLine("\n--- Aufgabe 5: Produkt löschen ---");
                // Dein Code hier:
                var laptopProduct = await context.Products.FirstOrDefaultAsync(p => p.Name == "Laptop");
                if (laptopProduct != null)
                {
                    context.Products.Remove(laptopProduct);
                    await context.SaveChangesAsync();
                    Console.WriteLine($"Produkt '{laptopProduct.Name}' wurde gelöscht.");
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
                 * In EF Core werden m:n Beziehungen durch eine Zwischentabelle (Join Table) abgebildet.
                 * Diese Zwischentabelle enthält die Primärschlüssel beider beteiligter Entitäten als Fremdschlüssel.
                 * Dadurch können mehrere Einträge aus der einen Tabelle mit mehreren Einträgen aus der anderen Tabelle verknüpft werden.
                 */

                // AUFGABE 6: Erstelle eine Order und füge die "Mouse" 2 mal hinzu (m:n Beziehung).
                Console.WriteLine("\n--- Aufgabe 6: Order erstellen (m:n) ---");
                // Dein Code hier:
                Order order = new Order()
                {
                    OrderId = 1,
                    OrderDate = DateTime.Now
                };
                var mouseForOrder = await context.Products.FirstOrDefaultAsync(p => p.Name == "Mouse");
                if (mouseForOrder != null)
                {
                    // order.Products.Add(mouseForOrder); --> das funktioniert aber nicht bitte versuche einen anderen weg zu finden weil 
                    order.OrderProducts.Add(new OrderProduct
                    {
                        Order = order,
                        Product = mouseForOrder,
                        Quantity = 2
                    });
                    await context.Orders.AddAsync(order);
                    await context.SaveChangesAsync();
                    Console.WriteLine($"Order mit ID {order.OrderId} wurde erstellt und die 'Mouse' 2 mal hinzugefügt.");
                }
                else
                {
                    Console.WriteLine("Produkt 'Mouse' nicht gefunden. Order konnte nicht erstellt werden.");
                }

                // Starte die fortgeschrittenen Übungen
                await AdvancedOrmExercises.RunAsync();

                Console.WriteLine("\nDrücke eine Taste zum Beenden...");
                Console.ReadKey();
            }
        }
    }
}
