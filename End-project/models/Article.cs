namespace End_project.Models;

public class Article
{
    public String Titel { get; set; }
    public String Author { get; set; }
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

    public int Lesezeit
    {
        get { return this._lesezeit; }
        set 
        { 
            if (value > 0)
            {
                this._lesezeit = value;
            }
            else
            {
                this._lesezeit = 0;
            }
        }
    }

    public Article() : this("", "", DateTime.Now, 0, Category.NotSpecified) {}

    public Article(string titel, string author, DateTime publishDate, int lesezeit, Category category)
    {
        this.Titel = titel;
        this.Author = author;
        this.Veröffentlichungsdatum = publishDate;
        this.Lesezeit = lesezeit;
        this.Category1 = category;
    }

    public override string ToString()
    {
        return $"Titel: {this.Titel}, Autor: {this.Author}, Datum: {this.Veröffentlichungsdatum:dd.MM.yyyy}, Lesezeit: {this.Lesezeit} Min, Kategorie: {this.Category1}";
    }
}
