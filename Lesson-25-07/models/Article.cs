namespace Lesson_25_07.models;

public class Article
{
    private string Titel { get; set; }
    private string Author { get; set; }

    // Backing fields to avoid name collisions and recursion
    private DateTime _publishDate;
    private int _lesezeit;

    public Category Category1 { get; set; }

    public DateTime Veröffentlichungsdatum
    {
        get { return _publishDate; }
        set
        {
            if (value <= DateTime.Now)
            {
                _publishDate = value;
            }
            else
            {
                _publishDate = DateTime.Now;
            }
        }
    }

    public int Lesezeit
    {
        get { return _lesezeit; }
        set
        {
            if (value > 0)
            {
                _lesezeit = value;
            }
            else
            {
                _lesezeit = 0;
            }
        }
    }

    public Article() : this("", "", DateTime.Now, 0, Category.NotSpecified) { }

    public Article(string titel, string author, DateTime publishDate, int lesezeit, Category category)
    {
        Titel = titel;
        Author = author;
        _publishDate = publishDate;
        _lesezeit = lesezeit;
        Category1 = category;
    }

    public override string ToString()
    {
        return $"Titel: {Titel}, Author: {Author}, PublishDate: {_publishDate}, Lesezeit: {_lesezeit}, Category: {Category1}";
    }
}