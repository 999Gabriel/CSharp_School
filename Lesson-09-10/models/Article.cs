namespace Lesson_09_10.models;

// WICHTIG: wir schreiben ab nun vereinfachte Klassen
//      nur automatische Properties, keine Ctors, keine ToString() methode
public class Article
{
    public String Titel { get; set; }
    public String Description { get; set; }
    public int Id { get; set; }
    public Decimal Price { get; set; }
    public DateTime ReleaseDate { get; set; }
    public Category category { get; set; }
}