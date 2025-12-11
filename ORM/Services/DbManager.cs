using Microsoft.EntityFrameworkCore;
using ORM.configuration;
using ORM.models;

namespace ORM.Services;

public class DbManager : DbContext
{
    /*
        der DbManager ermöglicht uns den Zugriff auf die komplette Datenbank und 
        aller Tabellen (über DbSet<T> Eigenschaften)
        ermöglicht uns den Zugriff auf die Tabelle Articles
    
    
        CURD Methoden für die Artikel-Tabelle
            C ... Create
            R ... Read
            U ... Update
            D ... Delete
    */
    
    public DbSet<Article> Articles { get; set; } = null!; // initialized to satisfy nullable analysis
    public DbSet<Review> Reviews { get; set; } = null!; // initialized to satisfy nullable analysis
    public DbSet<Invoice> Invoices { get; set; } = null!; // initialized to satisfy nullable analysis
    public DbSet<InvoiceArticle> InvoiceArticles { get; set; } = null!; // initialized to satisfy nullable analysis
    public DbSet<User> Users { get; set; } = null!; // Tabelle: members (via Annotation)
    public DbSet<User2> Users2 { get; set; } = null!; // Tabelle: members2 (via Fluent-API)
    
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    { 
        // für den Pomelo-MySQL-Treiber 
        string connectionString = "Server=localhost;database=ORM_Winkler;user=root;password=Nij43Bq8;"; 
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)); 
    }
    
    // ==================== READ Methods ====================
    
    /// <summary>
    /// Ruft alle Artikel aus der Datenbank ab
    /// </summary>
    public async Task<List<Article>> GetAllArticlesAsync()
    {
        return await Articles.ToListAsync();
    }
    
    /// <summary>
    /// Ruft einen Artikel nach seiner ID ab
    /// </summary>
    public async Task<Article?> GetArticleByIdAsync(int id)
    {
        return await Articles.FirstOrDefaultAsync(a => a.ArticleId == id);
    }
    
    // ==================== CREATE Method ====================
    
    /// <summary>
    /// Fügt einen neuen Artikel hinzu und speichert ihn asynchron
    /// </summary>
    public async Task<bool> CreateArticleAsync(Article article)
    {
        try
        {
            Articles.Add(article);
            return await SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler beim Erstellen des Artikels: {ex.Message}");
            return false;
        }
    }
    
    // ==================== UPDATE Method ====================
    
    /// <summary>
    /// Aktualisiert einen vorhandenen Artikel asynchron
    /// </summary>
    public async Task<bool> UpdateArticleAsync(Article article)
    {
        try
        {
            Articles.Update(article);
            return await SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler beim Aktualisieren des Artikels: {ex.Message}");
            return false;
        }
    }
    
    // ==================== DELETE Method ====================
    
    /// <summary>
    /// Löscht einen Artikel asynchron
    /// </summary>
    public async Task<bool> DeleteArticleAsync(Article article)
    {
        try
        {
            Articles.Remove(article);
            return await SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler beim Löschen des Artikels: {ex.Message}");
            return false;
        }
    }
    
    // ===================== REVIEW Methods ====================
    public async Task<bool> CreateReviewAsync(Review review)
    {
        try
        {
            Reviews.Add(review);
            return await SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler beim Erstellen des Reviews: {ex.Message}");
            return false;
        }
    }
    
    public async Task<bool> GetAllReviewsAsync()   
    {
        try
        {
            var allReviews = await Reviews.Include(r => r.Article).ToListAsync();
            foreach (var review in allReviews)
            {
                Console.WriteLine($"ReviewId: {review.ReviewId}, Rating: {review.Rating}, Article Title: {review.Article.Title}");
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler beim Abrufen der Reviews: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateReviewAsync(Review review)
    {
        try
        {
            Reviews.Update(review);
            return await SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler beim Aktualisieren des Reviews: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteReviewAsync(Review review)
    {
        try
        {
            Reviews.Remove(review);
            return await SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler beim Löschen des Reviews: {ex.Message}");
            return false;
        }
    }

    // ======================= INVOICE Methods =======================
    // ======================= m:n Beziehunge ========================
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Fluent-API Konfiguration aus separater Klasse laden (Variante 2)
        modelBuilder.ApplyConfiguration(new User2Configuration());
        
        modelBuilder.Entity<InvoiceArticle>()
            .HasKey(ia => new { ia.InvoiceId, ia.ArticleId });

        modelBuilder.Entity<InvoiceArticle>()
            .HasOne(ia => ia.Invoice)
            .WithMany(i => i.InvoiceArticles)
            .HasForeignKey(ia => ia.InvoiceId);

        modelBuilder.Entity<InvoiceArticle>()
            .HasOne(ia => ia.Article)
            .WithMany(a => a.InvoiceArticles)
            .HasForeignKey(ia => ia.ArticleId);
    }

    public async Task<bool> CreateInvoiceAsync(Invoice invoice, List<(int articleId, int quantity)> items)
    {
        await Invoices.AddAsync(invoice);
        await SaveChangesAsync();

        foreach (var (articleId, quantity) in items)
        {
            var ia = new InvoiceArticle
            {
                InvoiceId = invoice.InvoiceId,
                ArticleId = articleId,
                Quantity = quantity
            };
            await InvoiceArticles.AddAsync(ia);
        }

        return await SaveChangesAsync() > 0;
    }

    public async Task<Invoice?> GetInvoiceWithArticlesAsync(int invoiceId)
    {
        return await Invoices
            .Include(i => i.InvoiceArticles)
            .ThenInclude(ia => ia.Article)
            .FirstOrDefaultAsync(i => i.InvoiceId == invoiceId);
    }

    public async Task<bool> UpdateInvoiceArticleQuantityAsync(int invoiceId, int articleId, int newQuantity)
    {
        var ia = await InvoiceArticles
            .FirstOrDefaultAsync(x => x.InvoiceId == invoiceId && x.ArticleId == articleId);

        if (ia == null)
            return false;

        ia.Quantity = newQuantity;
        return await SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteInvoiceAsync(int invoiceId)
    {
        var invoice = await Invoices
            .Include(i => i.InvoiceArticles)
            .FirstOrDefaultAsync(i => i.InvoiceId == invoiceId);

        if (invoice == null)
            return false;

        InvoiceArticles.RemoveRange(invoice.InvoiceArticles);
        Invoices.Remove(invoice);

        return await SaveChangesAsync() > 0;
    }
}