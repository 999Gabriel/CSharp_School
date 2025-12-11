namespace ORM.models;

public class InvoiceArticle
{
    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; } = null!;
    public int ArticleId { get; set; }
    public Article Article { get; set; } = null!;
    
    public int Quantity { get; set; }
}