using End_project.Models;

namespace End_project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Beispiel zur While-Schleife ===");
            Console.WriteLine();

            // Liste von Personen erstellen
            List<Person> personen = new List<Person>
            {
                new Person(1, "Max", "Mustermann", 2500.50m, new DateTime(1990, 5, 15)),
                new Person(2, "Anna", "Schmidt", 3200.75m, new DateTime(1985, 8, 22)),
                new Person(3, "Peter", "Müller", 1800.00m, new DateTime(1995, 3, 10)),
                new Person(4, "Lisa", "Weber", 2800.25m, new DateTime(1988, 12, 5)),
                new Person(5, "Tom", "Fischer", 3500.00m, new DateTime(1982, 7, 18))
            };

            // Liste von articlen erstellen
            List<Article> article = new List<Article>
            {
                new Article("C# Grundlagen", "Max Mustermann", new DateTime(2024, 1, 15), 30, Category.Book),
                new Article("iPhone 15 Review", "Anna Schmidt", new DateTime(2024, 2, 10), 15, Category.Handy),
                new Article("Gaming Laptop Test", "Peter Müller", new DateTime(2024, 3, 5), 25, Category.Laptop),
                new Article("Neue Sneaker Trends", "Lisa Weber", new DateTime(2024, 3, 20), 10, Category.Shoes),
                new Article("Programmierung lernen", "Gabriel Winkler", new DateTime(2024, 4, 1), 45, Category.Book)
            };

            // While-Schleife Beispiel 1: Personen mit hohem Gehalt anzeigen
            Console.WriteLine("=== Personen mit Gehalt über 2500€ ===");
            int index = 0;
            while (index < personen.Count)
            {
                if (personen[index].Salary > 2500)
                {
                    Console.WriteLine(personen[index].ToString());
                }
                index++;
            }
            Console.WriteLine();

            // While-Schleife Beispiel 2: article nach Kategorie filtern
            Console.WriteLine("=== article der Kategorie 'Book' ===");
            int articleIndex = 0;
            while (articleIndex < article.Count)
            {
                if (article[articleIndex].Category1 == Category.Book)
                {
                    Console.WriteLine(article[articleIndex].ToString());
                }
                articleIndex++;
            }
            Console.WriteLine();

            // While-Schleife Beispiel 3: Benutzerinteraktion
            Console.WriteLine("=== Interaktives Beispiel ===");
            Console.WriteLine("Geben Sie eine Zahl zwischen 1 und 5 ein (0 zum Beenden):");
            
            int eingabe = -1;
            while (eingabe != 0)
            {
                Console.Write("Eingabe: ");
                if (int.TryParse(Console.ReadLine(), out eingabe))
                {
                    if (eingabe >= 1 && eingabe <= 5)
                    {
                        Console.WriteLine($"Person {eingabe}: {personen[eingabe - 1].ToString()}");
                        Console.WriteLine();
                    }
                    else if (eingabe != 0)
                    {
                        Console.WriteLine("Bitte geben Sie eine Zahl zwischen 1 und 5 ein (0 zum Beenden).");
                    }
                }
                else
                {
                    Console.WriteLine("Bitte geben Sie eine gültige Zahl ein.");
                }
            }

            Console.WriteLine("Programm beendet. Auf Wiedersehen!");
        }
    }
}