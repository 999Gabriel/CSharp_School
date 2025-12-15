using System;
using System.Linq;

namespace LinqExercises
{
    class Program
    {
        static void Main(string[] args)
        {
            var students = Data.GetStudents();  
            var courses = Data.GetCourses();

            Console.WriteLine("========== LINQ ÜBUNGEN ==========");

            /*
             * THEORIE FRAGE 1:
             * Was ist der Unterschied zwischen Query Syntax und Method Syntax in LINQ?
             * 
             * Antwort:
             * der unterschied liegt in der Schreibweise:
             * - Query Syntax ähnelt SQL und verwendet Schlüsselwörter wie from, where, select
             * - Method Syntax verwendet Methodenaufrufe wie .Where(), .Select()
             * beide Ansätze führen zum gleichen Ergebnis und können oft ineinander umgewandelt werden.
             * 
             */

            // AUFGABE 1: Filtere alle Studenten, die älter als 21 sind.
            Console.WriteLine("\n--- Aufgabe 1: Studenten älter als 21 ---");
            // Dein Code hier:
            var olderThan21 = from s in students
                where s.Age > 21
                select s;

            Console.WriteLine($"Die folgenden Studenten sind älter als 21:");
            foreach (var s in olderThan21) Console.WriteLine($"{s.Name} ({s.Age})");


            /*
             * THEORIE FRAGE 2:
             * Was bewirkt die Methode .ToList() am Ende einer LINQ-Abfrage und warum ist das wichtig (Deferred Execution)?
             * 
             * Antwort:
             * .ToList() konvertiert das Ergebnis einer LINQ-Abfrage in eine Liste und erzwingt die sofortige
             * Ausführung der Abfrage.
             * Ohne .ToList() wird die Abfrage erst ausgeführt, wenn auf die Daten zugegriffen wird
             * (Deferred Execution).
             * 
             */

            // AUFGABE 2: Projiziere eine Liste, die nur die Namen der Studenten enthält.
            Console.WriteLine("\n--- Aufgabe 2: Nur Namen der Studenten ---");
            // Dein Code hier:
            var studentNames = students.Select(s => s.Name).ToList();
            Console.WriteLine("Studentennamen:");
            foreach (var name in studentNames) Console.WriteLine(name);


            // AUFGABE 3: Sortiere die Studenten nach Alter absteigend.
            Console.WriteLine("\n--- Aufgabe 3: Studenten sortiert nach Alter (absteigend) ---");
            // Dein Code hier:
            var sortedByAgeDesc = students
                .OrderByDescending(s => s.Age)
                .Select(s => s.Name)
                .ToList();
            Console.WriteLine("Studenten sortiert nach Alter (absteigend):");
            foreach (var name in sortedByAgeDesc) Console.WriteLine(name);


            /*
             * THEORIE FRAGE 3:
             * Wofür wird 'GroupBy' verwendet? Gib ein Beispiel.
             * 
             * Antwort:
             * GroupBy wird verwendet, um Elemente in einer Sammlung basierend auf einem gemeinsamen Schlüssel zu
             * gruppieren.
             * Beispiel: Gruppiere Studenten nach ihrem Alter, um zu sehen, wie viele Studenten jedes Alters es gibt.
             * 
             */

            // AUFGABE 4: Gruppiere die Studenten nach ihrem Alter.
            Console.WriteLine("\n--- Aufgabe 4: Gruppierung nach Alter ---");
            // Dein Code hier:
            var groupedByAge = students.Select
                (
                    s => new {s.Name, s.Age}
                )
                .GroupBy(s => s.Age)
                .ToList();
            foreach (var ageGroup in groupedByAge)
            {
                Console.WriteLine($"Alter: {ageGroup.Key}");
                foreach (var student in ageGroup)
                {
                    Console.WriteLine($" - {student.Name}");
                }
            }


            /*
             * THEORIE FRAGE 4:
             * Erkläre den Unterschied zwischen .First(), .FirstOrDefault(), .Single() und .SingleOrDefault().
             *
             * Antwort:
             * - .First(): Gibt das erste Element einer Sequenz zurück. Wirft eine Ausnahme, wenn die Sequenz leer ist.
             * - .FirstOrDefault(): Gibt das erste Element einer Sequenz zurück oder den Standard
             * Wert (z.B. null), wenn die Sequenz leer ist.
             * - .Single(): Gibt das einzige Element einer Sequenz zurück. Wirft eine Ausnahme
             * wenn die Sequenz leer ist oder mehr als ein Element enthält.
             * - .SingleOrDefault(): Gibt das einzige Element einer Sequenz zurück oder den Standard
             * Wert (z.B. null), wenn die Sequenz leer ist. Wirft eine
             * Ausnahme, wenn die Sequenz mehr als ein Element enthält.
             *
             */

            // AUFGABE 5: Finde den ersten Studenten, der "David" heißt.
            Console.WriteLine("\n--- Aufgabe 5: Finde David ---");
            // Dein Code hier:
            var david = students.FirstOrDefault(s => s.Name == "David");
            if (david != null)
            {
                Console.WriteLine($"Gefundener Student: {david.Name}, Alter: {david.Age}");
            }
            else
            {
                Console.WriteLine("Student 'David' nicht gefunden.");
            }

            // AUFGABE 6: Join - Verbinde Studenten mit ihren Kursen und gib "StudentName - KursTitel" aus.
            Console.WriteLine("\n--- Aufgabe 6: Join Studenten und Kurse ---");
            // Dein Code hier:
            var studentCourses = from s in students
                join course in courses on s.CourseId equals course.Id
                    group new {s, course} by s.Name into scGroup
                    select scGroup.First();

            foreach (var course in studentCourses)
            {
                Console.WriteLine($"{course.s.Name} - {course.course.Title}");
            }


            /*
             * THEORIE FRAGE 5:
             * Was ist der Unterschied zwischen .Select() und .SelectMany()?
             *
             * Antwort:
             * - .Select(): Projiziert jedes Element einer Sequenz in eine neue Form.
             * - .SelectMany(): Projiziert jedes Element einer Sequenz in eine IEnumerable und
             * flacht die resultierenden Sequenzen zu einer einzigen Sequenz ab.
             * Beispiel: Wenn du eine Liste von Studenten hast, bei denen jeder Student
             * eine Liste von Kursen belegt hat, kannst du mit .SelectMany() alle
             * Kurse aller Studenten in einer einzigen Liste erhalten.
             *
             */
            
            // AUFGABE 7: Berechne das Durchschnittsalter aller Studenten.
            Console.WriteLine("\n--- Aufgabe 7: Durchschnittsalter ---");
            // Dein Code hier:
            var avergeAge = students.Average(s => s.Age);
            Console.WriteLine($"Das Durchschnittsalter der Studenten ist: {avergeAge}");

            // Starte die fortgeschrittenen Übungen
            AdvancedLinqExercises.Run();

            Console.WriteLine("\nDrücke eine Taste zum Beenden...");
            Console.ReadKey();
        }
    }
}
