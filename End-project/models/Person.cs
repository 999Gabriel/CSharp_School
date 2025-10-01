namespace End_project.Models;

public class Person
{
    private int _id;
    private decimal _salary;
    private DateTime _birthday;

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

    public Person() : this(0, "", "", 0.0m, DateTime.MinValue) {}

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
        return $"Id: {this.Id}, Name: {this.FirstName} {this.LastName}, Gehalt: {this.Salary:C}, Geburtstag: {this.Birthday:dd.MM.yyyy}";
    }
}
