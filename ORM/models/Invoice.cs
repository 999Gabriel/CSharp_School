namespace ORM.models;

public class Invoice
{
    public int InvoiceId { get; set; }
    public DateTime InvoiceDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    
    public List<InvoiceArticle> InvoiceArticles { get; set; } = new();
}