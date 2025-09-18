// Namespace entspricht einem Package in Java

// using entspricht import in Java
using System;
using System.Collections.Generic;
using Csharp_code_school.Models;

namespace Csharp_code_school
{
    public class Program
    {
        public static void Main(String[] args)
        {

            /*
             * Datentypen
             * int ... Ganzzahlen
             * String ... Texte
             * char ... Zeichen
             * double ... Gleitkommazahlen
             * float ... Gleitkommazahlen
             * decimal ... Dezimalzahlen(Währungsbeträge)
             * byte ... Ganzzahlen
             * sbyte ... Ganzzahlen
             * long ... Ganzzahlen
             * short ... Ganzzahlen
             * bool ... Wahrheitswerte
             * datetime ... Datum und Uhrzeit
             *
             * DateTime ... Datum und Uhrzeit
             *
             * und alle Klassen
             *
             * Auswahlanweisungen
             *
             *  - if/else if/else
             *  - switch
             * Schleifen
             *
             *  - while
             *  - do while
             *  - for
             *  - foreach --> für Arrays, listenartige STrukturen
             *
             * Klassen und Enums ... siehe Beispiele
             *
             * Methoden
             *  - Codewiederholung vermeiden
             *  - übersichtlicher Code
             *  - Fehler passieren nur an einer Stelle
             *
             *
             *  - sinnvolle Namen verwenden (keine Sonderzeichen, keine Leerzeichen)
             *  - C#: beginnen immer mit einem Großbuchstaben
             *          jedes weitere Wort mit einem Großbuchstaben
             *
             */
            /*
             
            *Stunde am 18.9.2025!!!*
            
            
            Person p1 = new();
            // in kurzer Schreibeweise
            Person p2 = new(); //Standardkonstruktor
            Person p3 = new();

            // hier wird die set Methode von dem Property Id aufgerufen
            // in java: p2.setId(101);
            // Ein- und Ausgabe
            Console.Write("Person 2: ");
            // Console.ReadLine() liest die Eingabe im Terminal als Typ String!!!
            p2.FirstName = Console.ReadLine();
            Console.Write("Vorname von Person 2: ");
            Console.WriteLine(p2.FirstName);
            // Klasse Convert um von String zu Decimal umzuwandeln
            Console.Write("Gehalt von Person 2: ");
            p2.Salary = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Gehalt von" + p2.FirstName +" : ");
            Console.WriteLine(p2.Salary);
            Console.WriteLine("Geburtsdatum von Person 2: ");
            int year, month, day;
            Console.Write("Jahr: ");
            year = Convert.ToInt16(Console.ReadLine());
            Console.Write("Monat: ");
            month = Convert.ToInt16(Console.ReadLine());
            Console.Write("Tag: ");
            day = Convert.ToInt16(Console.ReadLine());
            // Datum
            p2.Birthday = new DateTime(year, month, day);
            Console.WriteLine("Geburtsdatum von Person 2: ");
            Console.WriteLine(p2.Birthday);

            //p1
            Console.Write("Person 2: ");
            // Console.ReadLine() liest die Eingabe im Terminal als Typ String!!!
            p1.FirstName = Console.ReadLine();
            Console.Write("Vorname von Person 2: ");
            Console.WriteLine(p2.FirstName);
            // Klasse Convert um von String zu Decimal umzuwandeln
            Console.Write("Gehalt von Person 2: ");
            p1.Salary = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Gehalt von" + p2.FirstName +" : ");
            Console.WriteLine(p2.Salary);
            Console.WriteLine("Geburtsdatum von Person 2: ");
            Console.Write("Jahr: ");
            year = Convert.ToInt16(Console.ReadLine());
            Console.Write("Monat: ");
            month = Convert.ToInt16(Console.ReadLine());
            Console.Write("Tag: ");
            day = Convert.ToInt16(Console.ReadLine());
            // Datum
            p1.Birthday = new DateTime(year, month, day);
            Console.WriteLine("Geburtsdatum von Person 2: ");
            Console.WriteLine(p1.Birthday);

            //p3
            Console.Write("Person 3: ");
            // Console.ReadLine() liest die Eingabe im Terminal als Typ String!!!
            p3.FirstName = Console.ReadLine();
            Console.Write("Vorname von Person 3: ");
            Console.WriteLine(p3.FirstName);
            // Klasse Convert um von String zu Decimal umzuwandeln
            Console.Write("Gehalt von Person 3: ");
            p3.Salary = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Gehalt von" + p3.FirstName +" : ");
            Console.WriteLine(p3.Salary);
            Console.WriteLine("Geburtsdatum von Person 3: ");
            Console.Write("Jahr: ");
            year = Convert.ToInt16(Console.ReadLine());
            Console.Write("Monat: ");
            month = Convert.ToInt16(Console.ReadLine());
            Console.Write("Tag: ");
            day = Convert.ToInt16(Console.ReadLine());
            // Datum
            p3.Birthday = new DateTime(year, month, day);
            Console.WriteLine("Geburtsdatum von Person 3: ");
            Console.WriteLine(p3.Birthday);

            // Personen Daten ausgaben
            Console.WriteLine(p2.ToString()); // Console.WriteLine(p2); sind komplett identisch

            // Arrays
            // def ein Array für 100 Kommazahlen
            double[] numbers = new double[100];
            // dem 7. Element die Zahl 19.90 zuweisen
            numbers[6] = 19.90; // Array beginnt bei 0

            // Listen (sind einfacher zu verwenden als Arrays)
            List<String> students = new();
            // einen Namen hinzufügen
            students.Add("Simon Kuttner");

            // Liste mit Person erzeugen
            List<Person> people = new();
            //p1 entfernen
            people.Remove(p1);
            // alle Personen ausgeben
            foreach (Person person in people)
            {
                Console.WriteLine(person.ToString());
            }

            //p1 und die andere entfernte Person wieder hinzufügen
            people.Add(p1);
            people.Add(p3);
            // alle Personen ausgeben
            Console.WriteLine("Alle Personen aus der Liste persons: ");
            people.ForEach(person => Console.WriteLine(person.ToString()));
            Console.WriteLine($"Es sind {people.Count} Personen in der Liste");

            // Dictoionary (Java: eine HashMap)
            Dictionary<string, int> studentsCount = new();
            // füge die Klasse 4AHWII mit 22 Schülern und
            // die Klasse 4BHWII mit 19 Schülern hinzu
            studentsCount.Add("4AHWII", 22);
            studentsCount.Add("4BHWII", 19);
            // 4BHWII die Anzahl auf 21 ändern
            studentsCount["4BHWII"] = 21;

            // Dic ausgeben
            // var ... der Compiler setzt für uns den Datentyp automatisch
            // dieser ist dann nicht mehr änderbar
            foreach (var s in studentsCount)
            {
                Console.WriteLine(s.Key + " " + s.Value);
            }*/

            // Aufgabe: ein Dict erzeugen mit PersonenId als Key und
            // Liste von Artikeln als Value
            // 2 Personen und deren Artikel hinzufügen
            // alles ausgaben

             Dictionary<int, (Person Person, List<Article> Articles)> purchases = new();

            // Ask how many customers
            Console.Write("How many customers? ");
            int customerCount;
            while (!int.TryParse(Console.ReadLine(), out customerCount) || customerCount < 0)
            {
                Console.Write("Please enter a valid number: ");
            }

            // For each customer
            for (int i = 0; i < customerCount; i++)
            {
                Console.WriteLine($"\n--- Customer {i + 1} ---");

                // Get person ID with uniqueness check
                Console.Write("Person Id: ");
                int personId;
                while (true)
                {
                    if (int.TryParse(Console.ReadLine(), out personId) && personId >= 0)
                    {
                        if (!purchases.ContainsKey(personId))
                        {
                            break;
                        }
                        Console.Write("This ID already exists. Please enter a different ID: ");
                    }
                    else
                    {
                        Console.Write("Please enter a valid ID: ");
                    }
                }

                // Get first name
                Console.Write("First name: ");
                string firstName = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(firstName))
                {
                    Console.Write("First name cannot be empty: ");
                    firstName = Console.ReadLine();
                }

                // Get last name
                Console.Write("Last name: ");
                string lastName = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(lastName))
                {
                    Console.Write("Last name cannot be empty: ");
                    lastName = Console.ReadLine();
                }

                // Create person
                var person = new Person { Id = personId, FirstName = firstName.Trim(), LastName = lastName.Trim() };
                var articles = new List<Article>();

                // Ask how many products
                Console.Write("How many products does this customer buy? ");
                int productCount;
                while (!int.TryParse(Console.ReadLine(), out productCount) || productCount < 0)
                {
                    Console.Write("Please enter a valid number: ");
                }

                // For each product
                for (int j = 0; j < productCount; j++)
                {
                    Console.WriteLine($"\n  Product {j + 1}:");

                    // Get article ID
                    Console.Write("    Article Id: ");
                    int articleId;
                    while (!int.TryParse(Console.ReadLine(), out articleId) || articleId < 0)
                    {
                        Console.Write("    Please enter a valid article ID: ");
                    }

                    // Get article name
                    Console.Write("    Article name: ");
                    string articleName = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(articleName))
                    {
                        Console.Write("    Article name cannot be empty: ");
                        articleName = Console.ReadLine();
                    }

                    // Get price
                    Console.Write("    Price (€): ");
                    decimal price;
                    while (!decimal.TryParse(Console.ReadLine(), out price) || price < 0)
                    {
                        Console.Write("    Please enter a valid price: ");
                    }

                    // Get quantity
                    Console.Write("    Quantity: ");
                    int quantity;
                    while (!int.TryParse(Console.ReadLine(), out quantity) || quantity < 0)
                    {
                        Console.Write("    Please enter a valid quantity: ");
                    }

                    // Get category
                    Console.WriteLine("    Available categories:");
                    Console.WriteLine("    0 = Book");
                    Console.WriteLine("    1 = Shoes");
                    Console.WriteLine("    2 = Computer");
                    Console.WriteLine("    3 = Handy");
                    Console.WriteLine("    4 = Laptop");
                    Console.Write("    Choose category (0-4): ");

                    int categoryChoice;
                    Category selectedCategory;
                    while (true)
                    {
                        if (int.TryParse(Console.ReadLine(), out categoryChoice) && categoryChoice >= 0 && categoryChoice <= 4)
                        {
                            selectedCategory = (Category)categoryChoice;
                            break;
                        }
                        Console.Write("    Please enter a number between 0-4: ");
                    }

                    // Create article
                    bool available = quantity > 0;
                    var article = new Article(articleId, articleName.Trim(), price, quantity, selectedCategory, available)
                    {
                        Articlename = articleName.Trim()
                    };

                    articles.Add(article);
                    Console.WriteLine("    -> Product added!");
                }

                // Store customer and their articles
                purchases[personId] = (person, articles);
            }

            // Display results
            Console.WriteLine("\n================ PURCHASE SUMMARY ================");
            foreach (var purchase in purchases)
            {
                var person = purchase.Value.Person;
                var articleList = purchase.Value.Articles;

                Console.WriteLine($"\nCustomer {person.Id}: {person.FirstName} {person.LastName}");

                if (articleList.Count == 0)
                {
                    Console.WriteLine("  (No products purchased)");
                }
                else
                {
                    foreach (var article in articleList)
                    {
                        Console.WriteLine($"  - {article.Articlename} | Category: {article.Category} | Quantity: {article.StockQuantity} | Price: €{article.Price}");
                    }
                }
            }

            Console.WriteLine("\nFinished!");
        }
    }
}


