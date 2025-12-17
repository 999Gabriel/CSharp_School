using System;
using System.Data.Common;
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
             * Query Syntax ähnelt SQL und verwendet Schlüsselwörter wie from, where und select usw.
             * Method Syntax verwendet Methodenaufrufe wie .Where(), .Select() usw. und ist oft kürzer.
             * 
             */

            // AUFGABE 1: Filtere alle Studenten, die älter als 21 sind.
            Console.WriteLine("\n--- Aufgabe 1: Studenten älter als 21 ---");
            // Dein Code hier:
            var olderThan21 = from s in students
                where s.Age > 21
                select s;

            foreach (var student in olderThan21)
            {
                Console.WriteLine($"Name: {student.Name}, Age: {student.Age}");
            }

            /*
             * THEORIE FRAGE 2:
             * Was bewirkt die Methode .ToList() am Ende einer LINQ-Abfrage und warum ist das wichtig (Deferred Execution)?
             *
             * Antwort:
             * .ToList() führt die Abfrage sofort aus und speichert die Ergebnisse in einer Liste.
             * Ohne .ToList() wird die Abfrage erst ausgeführt, wenn auf die Daten zugegriffen wird (Deferred Execution).
             * Das ist wichtig, um die Leistung zu optimieren und unerwartete Änderungen an den Daten zu vermeiden.
             * 
             */

            // AUFGABE 2: Projiziere eine Liste, die nur die Namen der Studenten enthält.
            Console.WriteLine("\n--- Aufgabe 2: Nur Namen der Studenten ---");
            // Dein Code hier:
            var studentNames = from s in students
                select s.Name;

            // AUFGABE 3: Sortiere die Studenten nach Alter absteigend.
            Console.WriteLine("\n--- Aufgabe 3: Studenten sortiert nach Alter (absteigend) ---");
            // Dein Code hier:
            var sortedByAgeDesc = students
                .OrderByDescending(s => s.Age)
                .ToList();

            foreach (var student in sortedByAgeDesc)
            {
                Console.WriteLine($"Name: {student.Name}, Age: {student.Age}");
            }

            /*
             * THEORIE FRAGE 3:
             * Wofür wird 'GroupBy' verwendet? Gib ein Beispiel.
             *
             * Antwort:
             * GroupBy wird verwendet, um Elemente basierend auf einem gemeinsamen Schlüssel zu gruppieren.
             * Beispiel: Gruppiere Studenten nach ihrem Alter, um alle Studenten desselben Alters zusammenzufassen.
             *
             */

            // AUFGABE 4: Gruppiere die Studenten nach ihrem Alter.
            Console.WriteLine("\n--- Aufgabe 4: Gruppierung nach Alter ---");
            // Dein Code hier:
            var groupedByAge = from s in students
                group s by s.Age
                into ageGroup
                select ageGroup;
            
            foreach (var ageGroup in groupedByAge)
            {
                Console.WriteLine($"Alter: {ageGroup.Key}");
                foreach (var student in ageGroup)
                {
                    Console.WriteLine($"- {student.Name}");
                }
            }  
            
            /*
             * THEORIE FRAGE 4:
             * Erkläre den Unterschied zwischen .First(), .FirstOrDefault(), .Single() und .SingleOrDefault().
             *
             * Antwort:
             * .First() gibt das erste Element zurück und wirft eine Ausnahme, wenn die Sequenz leer ist.
             * .FirstOrDefault() gibt das erste Element zurück oder den Standardwert (null), wenn die Sequenz leer ist.
             *
             */

            // AUFGABE 5: Finde den ersten Studenten, der "David" heißt.
            Console.WriteLine("\n--- Aufgabe 5: Finde David ---");
            // Dein Code hier:
            var studentDavid = students
                .FirstOrDefault(s => s.Name == "David");
            if (studentDavid != null)
            {
                Console.WriteLine($"Gefundener Student: {studentDavid.Name}, Age: {studentDavid.Age}");
            }
            else
            {
                Console.WriteLine("Student 'David' nicht gefunden.");
            }

            // AUFGABE 6: Join - Verbinde Studenten mit ihren Kursen und gib "StudentName - KursTitel" aus.
            Console.WriteLine("\n--- Aufgabe 6: Join Studenten und Kurse ---");
            // Dein Code hier:
            var studentCourses = from s in students
                join c in courses on s.CourseId equals c.Id
                select new 
                {
                    StudentName = s.Name, 
                    CourseTitle = c.Title
                };
            
            foreach (var sc in studentCourses)
            {
                Console.WriteLine($"{sc.StudentName} - {sc.CourseTitle}");
            }



            /*
             * THEORIE FRAGE 5:
             * Was ist der Unterschied zwischen .Select() und .SelectMany()?
             *
             * Antwort:
             * .Select() projiziert jedes Element in eine neue Form.
             * .SelectMany() projiziert jedes Element in eine Sequenz und flacht die resultierende Sequenz ab.
             */
            
            // AUFGABE 7: Berechne das Durchschnittsalter aller Studenten.
            Console.WriteLine("\n--- Aufgabe 7: Durchschnittsalter ---");
            // Dein Code hier:
            var avgAge = students.Average(s => s.Age);
            
            Console.WriteLine($"Durchschnittsalter der Studenten: {avgAge}");

            // Starte die fortgeschrittenen Übungen
            AdvancedLinqExercises.Run();

            Console.WriteLine("\nDrücke eine Taste zum Beenden...");
            Console.ReadKey();
        }
    }
}
