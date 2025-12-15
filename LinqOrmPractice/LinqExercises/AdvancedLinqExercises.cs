using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqExercises
{
    public class AdvancedLinqExercises
    {
        public static void Run()
        {
            var students = Data.GetStudents();
            var courses = Data.GetCourses();

            // Wir fügen noch ein paar komplexe Daten hinzu für die Übungen
            var studentGrades = new List<StudentGrade>
            {
                new StudentGrade { StudentId = 1, CourseId = 101, Grade = 1.3 }, // Alice, Math
                new StudentGrade { StudentId = 1, CourseId = 102, Grade = 2.0 }, // Alice, History
                new StudentGrade { StudentId = 2, CourseId = 101, Grade = 3.7 }, // Bob, Math
                new StudentGrade { StudentId = 3, CourseId = 103, Grade = 1.0 }, // Charlie, Physics
                new StudentGrade { StudentId = 4, CourseId = 102, Grade = 4.0 }, // David, History
                new StudentGrade { StudentId = 4, CourseId = 103, Grade = 5.0 }, // David, Physics (Failed)
            };

            Console.WriteLine("\n========== FORTGESCHRITTENE LINQ ÜBUNGEN ==========");

            /*
             * AUFGABE 1: GroupJoin (Left Outer Join)
             * Aufgabe: Liste ALLE Studenten auf und ihre Kurse.
             * Wenn ein Student keinen Kurs belegt hat (simuliert durch fehlenden Eintrag in studentGrades),
             * soll er trotzdem angezeigt werden mit dem Hinweis "Keine Kurse".
             *
             * Hinweis: Nutze GroupJoin zwischen students und studentGrades.
             */
            Console.WriteLine("\n--- Aufgabe 1: GroupJoin (Left Join) ---");
            // Dein Code hier:
            var studentCourseInfo = from s in students
                join g in courses on s.CourseId equals g.Id into sg
                from course in sg.DefaultIfEmpty()
                select new
                {
                    StudentName = s.Name,
                    CourseName = course != null ? course.Title : "Kein Kurs"
                };
            foreach (var info in studentCourseInfo)
            {
                Console.WriteLine($"Student: {info.StudentName}, Kurs: {info.CourseName}");
            }


            /*
             * AUFGABE 2: Komplexe Gruppierung & Aggregation
             * Aufgabe: Gruppiere die Noten (studentGrades) nach KursId.
             * Berechne für jeden Kurs:
             * - Den Durchschnitt (Average)
             * - Die beste Note (Min, da 1.0 besser ist als 5.0)
             * - Die schlechteste Note (Max)
             *
             * Gib den Kursnamen (aus 'courses' Liste holen!) und die Statistiken aus.
             */
            Console.WriteLine("\n--- Aufgabe 2: Advanced Grouping ---");
            // Dein Code hier:
            // FEEDBACK:
            // 1. Logikfehler: Du gruppierst nach StudentId (g.StudentId), aber die Aufgabe war nach KursId.
            // 2. Join-Fehler: Du joinst StudentId (gradeGroup.Key) mit CourseId (c.Id). Das passt inhaltlich nicht.
            //
            // KORREKTUR:
            /*
            var gradeStats = from g in studentGrades
                 group g by g.CourseId into gradeGroup // Gruppiere nach KURS
                 join c in courses on gradeGroup.Key equals c.Id
                 select new
                 {
                     CourseName = c.Title,
                     AverageGrade = gradeGroup.Average(x => x.Grade),
                     BestGrade = gradeGroup.Min(x => x.Grade),
                     WorstGrade = gradeGroup.Max(x => x.Grade)
                 };
            */
            var gradeStats = from g in studentGrades
                group g by g.StudentId
                into gradeGroup
                join c in courses on gradeGroup.Key equals c.Id
                select new
                {
                    CourseName = c.Title,
                    AverageGrade = gradeGroup.Average(x => x.Grade),
                    BestGrade = gradeGroup.Min(x => x.Grade),
                    WorstGrade = gradeGroup.Max(x => x.Grade)
                };
            foreach (var info in gradeStats)
            {
                Console.WriteLine($"Kurs: {info.CourseName}, Durchschnitt: {info.AverageGrade:F2}, " +
                                  $"Beste Note: {info.BestGrade:F2}, Schlechteste Note: {info.WorstGrade:F2}");
            }
            

            /*
             * AUFGABE 3: Set Operations (Except, Intersect, Union)
             * Szenario:
             * Liste A: Studenten, die Mathe belegt haben (CourseId 101).
             * Liste B: Studenten, die Physik belegt haben (CourseId 103).
             *
             * Aufgabe: Finde alle Studenten, die BEIDE Kurse belegt haben (Intersect).
             * Finde alle Studenten, die NUR Mathe aber NICHT Physik belegt haben (Except).
             */
            Console.WriteLine("\n--- Aufgabe 3: Mengenoperationen ---");
            // Dein Code hier:
            var mathStudents = from g in studentGrades
                where g.CourseId == 101
                join s in students on g.StudentId equals s.Id
                select s;
            
            var physicsStudents = from g in studentGrades
                where g.CourseId == 103
                join s in students on g.StudentId equals s.Id
                select s;
            
            var studentsInBothCourses = mathStudents.Intersect(physicsStudents);
            Console.WriteLine("Studenten, die sowohl Mathe als auch Physik belegt haben:");
            foreach (var student in studentsInBothCourses)
            {
                Console.WriteLine($"- {student.Name}");
            }
            // FEEDBACK:
            // Intersect ist korrekt! Aber der zweite Teil der Aufgabe (Except) fehlt.
            //
            // KORREKTUR (Teil 2):
            /*
            var onlyMathStudents = mathStudents.Except(physicsStudents);
            Console.WriteLine("Studenten nur in Mathe:");
            foreach (var s in onlyMathStudents) Console.WriteLine($"- {s.Name}");
            */

            /*
             * AUFGABE 4: Custom Comparer (Distinct)
             * Aufgabe: Die Liste 'students' enthält möglicherweise Duplikate (simulieren wir kurz).
             * Erstelle eine neue Liste mit einem Duplikat von "Alice".
             * Nutze .Distinct() mit einem Custom IEqualityComparer, um Studenten anhand ihrer ID zu unterscheiden,
             * nicht anhand der Referenz.
             */
            Console.WriteLine("\n--- Aufgabe 4: Distinct mit IEqualityComparer ---");
            // Dein Code hier:
            var studentsWithDuplicate = students.ToList();
            studentsWithDuplicate.Add(new Student { Id = 1, Name = "Alice (Clone)", Age = 20, CourseId = 101 });
            
            // Implementiere hier den Comparer oder nutze DistinctBy (wenn .NET 6+)
            // FEEDBACK:
            // Das Ergebnis ist korrekt, aber du hast die Aufgabe "umgangen".
            // Gefragt war .Distinct() mit einem IEqualityComparer.
            // Deine Lösung ist ein Workaround (DistinctBy-Logik via GroupBy).
            //
            // KORREKTUR (mit DistinctBy in .NET 6+):
            // var distinctStudents = studentsWithDuplicate.DistinctBy(s => s.Id).ToList();
            var distinctStudents = studentsWithDuplicate
                .GroupBy(s => s.Id)
                .Select(g => g.First())
                .ToList();
            Console.WriteLine("Eindeutige Studenten basierend auf ID:");
            foreach (var student in distinctStudents)
            {                
                Console.WriteLine($"- {student.Name} (ID: {student.Id})");
            }
        }
    }

    // Hilfsklasse für Aufgabe
    public class StudentGrade
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public double Grade { get; set; }
    }
}
