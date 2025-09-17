namespace Csharp_code_school;

public class Article
{
    // Private fields (attributes) 
    // We make them private to follow encapsulation principle - external code can't directly access them
    private int _id;                
    private string _articleName;    
    private decimal _price;         
    private int _stockQuantity;     
    private bool _isAvailable;      
    
    // Primary constructor - accepts all required parameters to create a valid Article object
    // We initialize all fields here to ensure object is in a valid state from creation
    public Article(int id, string name, decimal price, int stock, Category category, bool available)
    {
        this._id = id;
        this._articleName = name;
        this._price = price;
        this._stockQuantity = stock;
        this.Category = category;       // Using the property (automatic) instead of a field
        this._isAvailable = available;
    }
    
    // Default constructor - creates an Article with safe default values
    // This uses constructor chaining (:this(...)) to call the primary constructor
    // Why? Avoids code duplication and ensures consistent initialization logic
    public Article() : this(0, "", 0.0m, 0, Category.NotSpecified, false)
    {
    }

    // Property for ID with validation
    // Why properties instead of direct field access? They allow us to add validation and control access
    public int Id
    {
        get { return this._id; }    
        set
        {
            // Validation: ID should never be negative
            // This prevents invalid data from being stored
            if (value >= 0)
            {
                this._id = value;
            }
            // If invalid value is provided, we ignore it (could also throw exception)
        }
    }

    // Automatic property for article name
    // Why automatic? No special validation needed, so we let C# handle the backing field automatically
    public string Articlename { get; set; }

    // Property for Price with business logic validation
    public decimal Price
    {
        get { return this._price; }
        set
        {
            // Business rule: Price cannot be negative
            if (value >= 0)
            {
                this._price = value;
            }
            else
            {
                // If negative price is provided, set to 0 as safe default
                this._price = 0;
            }
        }
    }
    
    // Availability depends on two factors: manual flag AND stock quantity
    public bool IsAvailable
    {
        get { 
            // An item is only available if:
            // 1. It's manually marked as available
            // 2. AND there's stock available (quantity > 0)
            return this._isAvailable && this._stockQuantity > 0; 
        }
        set
        {
            // Allow setting to false always
            // Only allow setting to true if there's actually stock available
            if (!value || this._stockQuantity > 0)
            {
                this._isAvailable = value;
            }
            // If trying to set true when no stock, ignore the request
        }
    }

    // Automatic property for Category enum
    // Why automatic? The enum itself provides type safety, so no additional validation needed
    public Category Category { get; set; } 

    // Property for stock quantity with validation
    // Why validation? Stock can't be negative - that would mean we owe inventory
    public int StockQuantity
    {
        get { return this._stockQuantity; }    // Return the current stock level
        set
        {
            // Business rule: Stock quantity cannot be negative
            // If someone tries to set negative stock (data entry error, calculation bug, etc.)
            // we set it to 0 as a safe default instead of allowing invalid data
            if (value < 0)
            {
                this._stockQuantity = 0;    
            }
            else
            {
                this._stockQuantity = value;    
            }
        }
    }
    
    public override string ToString()
    {
        return $"Id: {this.Id}, Article Name: {this.Articlename}, " +
               $"Price in €: {this.Price}, Quantity: {this.StockQuantity}, Category: {this.Category}, " +
               $"Available: {this.IsAvailable}";
    }
}