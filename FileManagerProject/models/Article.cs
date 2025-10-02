namespace FileManagerProject.Models;

public class Article
{
    public int Id { get; set; }
    public String Titel { get; set; }
    public String Author { get; set; }
    public decimal Preis { get; set; }
    public String Beschreibung { get; set; }
    private DateTime _publishDate;
    private int _lesezeit;

    public Category Category1 { get; set; }

    public DateTime Veröffentlichungsdatum 
    {
        get { return this._publishDate; }
        set
        {
            if (value <= DateTime.Now)
            {
                this._publishDate = value;
            }
            else
            {
                this._publishDate = DateTime.Now;
            }
        }
    }

    public int? Lesezeit
    {
        get { return this.Category1 == Category.Book ? this._lesezeit : null; }
        set 
        { 
            if (this.Category1 == Category.Book && value.HasValue && value.Value > 0)
            {
                this._lesezeit = value.Value;
            }
            else if (this.Category1 == Category.Book)
            {
                this._lesezeit = 0;
            }
            else
            {
                this._lesezeit = 0; // Nicht relevant für andere Kategorien
            }
        }
    }

    public Article() : this(0, "", "", 0.0m, "", DateTime.Now, 0, Category.NotSpecified) {}

    public Article(int id, string titel, string author, decimal preis, string beschreibung, DateTime publishDate, int lesezeit, Category category)
    {
        this.Id = id;
        this.Titel = titel;
        this.Author = author;
        this.Preis = preis;
        this.Beschreibung = beschreibung;
        this.Veröffentlichungsdatum = publishDate;
        this.Lesezeit = lesezeit;
        this.Category1 = category;
    }

    public override string ToString()
    {
        string lesezeitText = this.Category1 == Category.Book ? $", Lesezeit: {this.Lesezeit} Min" : "";
        return $"ID: {this.Id}, Titel: {this.Titel}, Autor: {this.Author}, Preis: {this.Preis:C}, Beschreibung: {this.Beschreibung}, Datum: {this.Veröffentlichungsdatum:dd.MM.yyyy}{lesezeitText}, Kategorie: {this.Category1}";
    }
}
