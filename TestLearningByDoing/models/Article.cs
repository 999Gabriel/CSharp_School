//4) Article mit Validierung
// Aufgabe: Erstelle Article mit Properties: ArticleName (string), Category (Enum), Price (decimal), StockQuantity (int).
// Ziel: Fail-fast in Settern/Konstruktor.
// Akzeptanzkriterien:
// Price < 0 → ArgumentOutOfRangeException
// StockQuantity < 0 → ArgumentOutOfRangeException
// IsAvailable ist berechnete Property: StockQuantity > 0 (nicht speichern!)
// ToString() nutzt nur Properties und formatiert Price als Währung ({Price:C}).
/*
 namespace TestLearningByDoing.models;

    public class Article
    {
        private string _articleName;
        private Category _category;
        private decimal _price;
        private int _stockQuantity;
    
    public Article(string articleName, Category category, decimal price, int stockQuantity)
    {
        _articleName = articleName;
        _category = category;
        _price = price;
        _stockQuantity = stockQuantity;
    }
    public string ArticleName { get; set; }
    public Category Category { get; set; }

    public decimal Price
    {
        get => _price;
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Price must be greater than 0");
            }
            _price = value;
        }
    }

    public int StockQuantity
    {
        get => _stockQuantity;
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "StockQuantity is 0 oder negative (WE'RE OUT MAN!");
            }
            _stockQuantity = value;
        }
    }
    
    
    public override string ToString()
    {
        return $"{_articleName} ({_category}): {_price:C} ({_stockQuantity} in Stock)";
    }
    */
    
    // Aufgabe 5
    // Parameterloser Constructor
    using System;

    namespace TestLearningByDoing.models
    {
        public class Article
        {
        // Auto-Properties (eine Datenquelle)
        public string ArticleName { get; private set; }
        public Category Category { get; private set; }
        public decimal Price { get; private set; }
        public int StockQuantity { get; private set; }

        // Abgeleitet, nicht speichern
        public bool IsAvailable => StockQuantity > 0;

        // (1) Parameterloser Konstruktor -> sinnvolle Defaults
        public Article() : this("", Category.NOTSPECIFIED, 0m, 0) { }

        // (2) 3-Parameter -> StockQuantity = 0
        public Article(string articleName, Category category, decimal price)
            : this(articleName, category, price, 0) { }

        // (3) Vollkonstruktor (eine Quelle der Wahrheit + Validierung)
        public Article(
            string articleName,
            Category category,
            decimal price,
            int stockQuantity)
        {
            ArticleName   = ValidateName(articleName, nameof(articleName));
            Category      = category;
            Price         = price >= 0m
                ? price
                : throw new ArgumentOutOfRangeException(nameof(price), ">= 0 erwartet.");
            StockQuantity = stockQuantity >= 0
                ? stockQuantity
                : throw new ArgumentOutOfRangeException(nameof(stockQuantity), ">= 0 erwartet.");
        }

        public override string ToString()
        {
            return $"ID: (n/a) | Name: {ArticleName} | Cat: {Category} | " +
                   $"Price: {Price:C} | Stock: {StockQuantity} | Avail: " +
                   (IsAvailable ? "Yes" : "No");
        }

        private static string ValidateName(string? value, string param)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Darf nicht leer sein.", param);
            return value.Trim();
        }
    }
}
