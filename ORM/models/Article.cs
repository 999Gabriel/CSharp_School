namespace ORM.models;

public class Article
{
    // wir verwenden vereinfachte Klassen, keine Ctors, kein ToString() usw
    // der ORM erzeugt aufgrund dieser Klasse eine SQL Tabelle
    // und damit er erkennt welches Feld das Primärschlüsselfeld ist, müssen wir dieses Feld entweder
    // Id oder KlassennameId bezeichnen --> convention over configuration
    public int ArticleId { get; set; }
    public string Title { get; set; } = string.Empty; // initialized to avoid CS8618
    public String Name { get; set; } = string.Empty;  // initialized to avoid CS8618
    public Decimal Price { get; set; }
    public DateTime ReleaseDate { get; set; }
    
}