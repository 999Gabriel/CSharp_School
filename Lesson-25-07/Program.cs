using Lesson_25_07.models;

namespace Lesson_25_07
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Einfaches Beispiel mit den Klassen
            Console.WriteLine("=== Personen Beispiel ===");

            // Person erstellen
            Person person1 = new Person(1, "Max", "Mustermann", 2500.50m, new DateTime(1990, 5,
                15));
            Person person2 = new Person(2, "Anna", "Schmidt", 3200.75m, new DateTime(1985, 8,
                22));

            Console.WriteLine("Person 1:");
            Console.WriteLine(person1.ToString());
            Console.WriteLine();

            Console.WriteLine("Person 2:");
            Console.WriteLine(person2.ToString());
            Console.WriteLine();

            // Artikel erstellen
            Console.WriteLine("=== Artikel Beispiel ===");

            Article artikel1 = new Article("C# Grundlagen", "Max Mustermann", new DateTime(2024, 1, 15),
                30, Category.Book);
            Article artikel2 = new Article("Neues iPhone Review", "Anna Schmidt", new DateTime(2024, 2,
                10), 15, Category.Handy);

            Console.WriteLine("Artikel 1:");
            Console.WriteLine(artikel1.ToString());
            Console.WriteLine();

            Console.WriteLine("Artikel 2:");
            Console.WriteLine(artikel2.ToString());
            Console.WriteLine();

            // Kategorien anzeigen
            Console.WriteLine("=== Verfügbare Kategorien ===");
            foreach (Category category in Enum.GetValues<Category>())
            {
                Console.WriteLine($"- {category}");
            }
        }
    }
}