//Aufgabe 2: Person Klasse
 using System;

namespace TestLearningByDoing.models
{
    // Repräsentiert eine Person mit Vor- und Nachnamen.
    public class Person
    {
        // Private Felder (nur falls du später Logik ergänzen willst)
        private string _firstName;
        private string _lastName;

        // Öffentliche Properties (einzige Datenquelle)
        public string FirstName
        {
            get => _firstName;
            set => _firstName = ValidateName(value, nameof(FirstName));
        }

        public string LastName
        {
            get => _lastName;
            set => _lastName = ValidateName(value, nameof(LastName));
        }

        // Konstruktor (setzt Properties, nicht direkt Felder)
        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName  = lastName;
        }

        // ToString: "Lastname, Firstname"
        public override string ToString()
        {
            return $"{LastName}, {FirstName}";
        }

        // Kleine Validierungsmethode (einheitlich)
        private static string ValidateName(string? name, string paramName)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Darf nicht leer sein.", paramName);

            return name.Trim();
        }
    }
}


/*
 * //Aufgabe 3
 --------------------------------------
 */
 
 