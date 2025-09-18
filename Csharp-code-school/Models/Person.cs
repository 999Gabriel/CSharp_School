namespace Csharp_code_school.Models;
/*
   Aufgabe für 11.9.2025

        schreibe eine Klasse Article
        wähle selber sinnvolle Attribute (möglichst viele unterschiedliche Datentypen)
        und verwende nicht Copilot oder andere Tools
*/


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
 */
public class Person
{
    // Attribute
    private string _name;
    private int _age;
    private string _city;
    private int _id;
    private decimal _salary;

    //getter und setter --> properties
    //es gibt
    // - normale properties
    //          es muss das Field und das Property (get/set) programmiert werden
    // - automatische properties
    //          es muss nur das Property in einer verkürzten Schreibweise programmiert werden
    public int Id
    {
        get { return this._id; }
        set
        {
            // - value wird vom Compiler automatisch bereitgestellt
            //      value entspricht dem Parameter der setMethode in Java (also in der Klammer oben)
            if (value >= 0)
            {
                this._id = value;
            }
        }
    }

    public decimal Salary
    {
        get { return this._salary; }
        set
        {
            if (value >= 0.0m) // 0.0m ist der default Wert --> m = decimal weil value ein decimal ist, 0.0 ist ein double
            {                  // deswegen ein m hinter 0.0 bzw hinter dem double
                this._salary = value;
            }
        }
    }

    public DateTime Birthday { get; set; }

    // Konstruktor --> ctor
    //   Sinn: beim Erzeugen eines Objekts werden die Felder mit Werten initialisiert
    //        Standart-Ctor: hat keine Parameter und initialisiert die Felder mit Standartwerten
    //          (int = 0, string = null, bool = false/true, double = 0.0, char = '')
    //    Benutzerdefinierter Ctor: hat Parameter, es werden die Felder mit den übergebenen Werten initialisiert
    // über : this(...) wird der zweite Ctor aufgerufen
    public Person(string name, int age, string city, int id, DateTime birthDate)
    {
        //in Java: this.setId(id);
        this.Id = id;
        this._name = name;
        this._age = age;
        this._city = city;
        this.Birthday = birthDate;
    }
    public Person() : this("", 0, "", 0, DateTime.MinValue)
    {
    }

    //automatisches Property
    public string FirstName { get; set; } // keine Variable definieren weil der Compile das für uns macht
    public string LastName { get; set; }

    // other methodes
    // Methoden werden in C# mit einem Großbuchstaben begonnen
    public override string ToString()
    {
        // $ bedeutet "String interpolation" --> innerhalb der "" können Variablen direkt eingebunden werden
        //                                                        und zwar mit {this.Id}
        return $"Id: {this.Id}, Name: {this.FirstName} {this.LastName}, " +
               $"Salary: {this.Salary}, Birthday: {this.Birthday.ToShortDateString()}";
    }
}
