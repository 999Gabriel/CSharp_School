namespace Lesson_25_07.models;

public class Person
{
    private int _id;
    private decimal _salary;
    private DateTime _birthday;

    // getter/setter (C#: Properties)
    // normale Properties: werden verwendet, wenn wir
    // eine Überprüfung einbauen wollen
    // automatische Properties: die Variable darf nicht erzeugt werden
    // werden verwendet, wenn keine Überprüfung notwendig ist
    public int Id
    {
        get { return this._id; }
        set 
        {
            if (value >= 0)
            {
                this._id = value;
            }
        }
    }
    
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public DateTime Birthday 
    {
        get { return this._birthday; }
        set {
            if (value <= DateTime.Now)
            {
                this._birthday = value;
            }
        }
    }
    public decimal Salary 
    {
        get { return this._salary; }
        
        set {
            if (value >= 0.0m)
            {
                this._salary = value;
            }
        }
    }

    //ctor
    //Sinn: ctor wird beim Erzeugen einer Instanz/Objekt aufgerufen
    // er soll alle Felder auf sinnvolle Startwerte setzen
    // Standard-Ctor: besitzt keine Parameter, setzt die Parameter auf Null Werte (int 0, double 0.0, string "",...)
    public Person() : this(0, "", "", 0.0m, DateTime.MinValue) {}
    //weiterer Ctor
    public Person(int id, string firstName, string lastName, decimal salary, DateTime birthdate)
    {
        this.Id = id;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Salary = salary;
        this.Birthday = birthdate;
    }
    public override string ToString()
    {
        return $"Id: {this.Id}, FirstName: {this.FirstName}, LastName: {this.LastName}, Salary: {this.Salary}, Birthday: {this.Birthday}";
    }
}