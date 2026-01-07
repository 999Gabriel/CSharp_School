using System.Text.Json.Serialization;

namespace ORM.models;

public class Review
{
    public int ReviewId { get; set; }
    public string ReviewText { get; set; } = string.Empty;
    public int Rating { get; set; }
    
    // Foreign Key (Fremdschlüssel) - verweist auf den Artikel
    public int ArticleId { get; set; }
    
    // Navigation Property (da zwischen Artikel und Review eine 1:n Beziehung besteht)
    // --> jeder Review gehört genau einem Artikel
    [JsonIgnore]
    public virtual Article? Article { get; set; } 
}