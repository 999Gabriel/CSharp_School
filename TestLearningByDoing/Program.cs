/*
 * Mini-Aufgaben (von leicht → mittel)
   1) Console-Input Basics
   Aufgabe: Lies eine ganze Zahl von der Konsole ein und gib sie wieder aus.
   Ziel: Console.ReadLine(), Convert.ToInt32/int.TryParse.
   Akzeptanzkriterien:
   Programm fragt: „Gib eine ganze Zahl ein:“
   Bei gültiger Zahl: „Eingegeben: X“
   Bei ungültiger Zahl: „Fehler: Bitte ganze Zahl!“ (ohne Absturz)
   Hinweis: Nutze int.TryParse.
 ----------------------------------------------------
 Code:
using System;

namespace TestLearningByDoing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int zahl;

            while (true)
            {
                Console.Write("Gib eine ganze Zahl ein: ");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out zahl))
                    break;

                Console.WriteLine("Fehler: Bitte gib eine ganze Zahl ein!");
            }

            Console.WriteLine("Eingegebene Zahl: " + zahl);
        }
    }
}
*/

/*
 * 2) String-Input in Objekt umsetzen
   Aufgabe: Lies Firstname und Lastname von der Konsole und erstelle ein Person-Objekt.
   Ziel: Objektkonstruktion aus Input.
   Akzeptanzkriterien:
   Leere Eingaben sind nicht erlaubt → gib eine Fehlermeldung aus.
   ToString() der Person gibt „Lastname, Firstname“ aus.
   ---------------------------------------------------------
   Code:

using TestLearningByDoing.models;

namespace TestLearningByDoing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Personen Beispiel ===");

            string firstName = ReadNonEmpty("Gib den Vornamen ein: ");
            string lastName  = ReadNonEmpty("Gib den Nachnamen ein: ");

            // Konstruktion – schlägt bei ungültigen Daten mit Exception fehl
            var person = new Person(firstName, lastName);

            // Akzeptanzkriterium: "Lastname, Firstname"
            Console.WriteLine(person.ToString());
        }

        // Kleine, fokussierte Hilfsmethode (validiert leere Eingaben)
        private static string ReadNonEmpty(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                    return input.Trim();

                Console.WriteLine("Fehler: Eingabe darf nicht leer sein.");
            }
        }
    }
}
*/

/*
 * // 3) Auswahl per switch
 * // Aufgabe: Menü mit switch: (1) Person anlegen, (2) Programm beenden.
 * // Ziel: Menüführung & Kontrollfluss.
 * // Akzeptanzkriterien:
 * // Ungültige Eingabe → „Ungültige Option“
 * // Bei (1) wird Aufgabe 2 ausgeführt, danach zurück ins Menü.
-----------------------------------------------------------------


using TestLearningByDoing.models;
using System;
namespace TestLearningByDoing;
public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== Personen Beispiel ===");
        while (true)
        {
            Console.WriteLine("Menü:");
            Console.WriteLine("1) Person anlegen");
            Console.WriteLine("2) Programm beenden");
            Console.Write("Wähle eine Option: ");
            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    // Person anlegen
                    Console.WriteLine("Gib deiner Person einen Namen!");
                    string firstName = Console.ReadLine();
                    Console.WriteLine("Gib ihr nun einen Nachnamen!");
                    string lastName  = Console.ReadLine();
                    try
                    {
                        var person = new Person(firstName, lastName);
                        Console.WriteLine("Erstellt: " + person);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine("Fehler bei der Personenerstellung: " + ex.Message);
                    }

                    break;

                case "2":
                    // Programm beenden
                    Console.WriteLine("Programm wird beendet.");
                    return;

                default:
                    Console.WriteLine("Ungültige Option. Bitte versuche es erneut.");
                    break;
            }
        }
    }
}
*/


//4) Article mit Validierung
// Aufgabe: Erstelle Article mit Properties: ArticleName (string), Category (Enum), Price (decimal), StockQuantity (int).
// Ziel: Fail-fast in Settern/Konstruktor.
// Akzeptanzkriterien:
// Price < 0 → ArgumentOutOfRangeException
// StockQuantity < 0 → ArgumentOutOfRangeException
// IsAvailable ist berechnete Property: StockQuantity > 0 (nicht speichern!)
// ToString() nutzt nur Properties und formatiert Price als Währung ({Price:C}).
/*
using TestLearningByDoing.models;
using System;

namespace TestLearningByDoing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Article article = new Article("Laptop", Category.ELECTRONICS, 999.99m, 10);
                Console.WriteLine(article.ToString());

                // Teste ungültige Werte
                var invalidArticle = new Article("Invalid", Category.FOOD, -5.00m, 5); // Preis < 0
                var invalidArticle2 = new Article("Invalid2", Category.CLOTHING, 50.00m, -1); // StockQuantity < 0
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Fehler bei der Artikelerstellung: " + ex.Message);
            }
        }
    }
}
*/

//5) Überladene Konstruktoren
// Aufgabe: Füge zu Article einen parameterlosen Konstruktor hinzu, der sinnvolle Defaults setzt und den Vollkonstruktor aufruft.
// Ziel: Konstruktor-Kaskadierung.
// Akzeptanzkriterien:
// this(...) wird verwendet.
// Defaults: ArticleName = "", Category = NotSpecified, Price = 0m, StockQuantity = 0.

