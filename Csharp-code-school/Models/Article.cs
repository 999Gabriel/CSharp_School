namespace Csharp_code_school.Models;

public class Article
{
    // Private Felder (Attribute)
    // Sie sind privat gemäß dem Kapselungsprinzip – externer Code kann nicht direkt darauf zugreifen
    private int _id;                
    private string _articleName;    
    private decimal _price;         
    private int _stockQuantity;     
    private bool _isAvailable;      
    
    // Primärer Konstruktor – nimmt alle notwendigen Parameter entgegen, um ein gültiges Article-Objekt zu erzeugen
    // Alle Felder werden hier initialisiert, damit das Objekt von Anfang an in einem konsistenten Zustand ist
    public Article(int id, string name, decimal price, int stock, Category category, bool available)
    {
        this._id = id;
        this._articleName = name;
        this._price = price;
        this._stockQuantity = stock;
        this.Category = category;       // Nutzung der (automatischen) Property statt eines Feldes
        this._isAvailable = available;
    }
    
    // Parameterloser Standardkonstruktor – erzeugt ein Article mit sicheren Standardwerten
    // Verwendet Constructor Chaining (: this(...)), um den primären Konstruktor aufzurufen
    // Vorteil: Keine Code-Duplikation und zentrale Initialisierungslogik
    public Article() : this(0, "", 0.0m, 0, Category.NotSpecified, false)
    {
    }

    // Property für Id mit Validierung
    // Warum Properties statt direkter Feldzugriffe? Sie ermöglichen Validierung und kontrollierten Zugriff
    public int Id
    {
        get { return this._id; }    
        set
        {
            // Validierung: Id darf nicht negativ sein
            // Verhindert das Speichern ungültiger Daten
            if (value >= 0)
            {
                this._id = value;
            }
            // Bei ungültigem Wert wird ignoriert (Alternative: Exception werfen)
        }
    }

    // Automatische Property für den Artikelnamen
    // Keine spezielle Validierung nötig – daher automatische Property
    public string Articlename { get; set; }

    // Property für den Preis mit Geschäftslogik-Validierung
    public decimal Price
    {
        get { return this._price; }
        set
        {
            // Regel: Preis darf nicht negativ sein
            if (value >= 0)
            {
                this._price = value;
            }
            else
            {
                // Wenn negativer Preis übergeben wird: auf 0 setzen als sicherer Standard
                this._price = 0;
            }
        }
    }
    
    // Verfügbarkeit hängt von zwei Faktoren ab: manuelles Flag UND Lagerbestand
    public bool IsAvailable
    {
        get { 
            // Ein Artikel ist nur verfügbar wenn:
            // 1. Er manuell als verfügbar markiert ist
            // 2. UND Lagerbestand vorhanden ist (> 0)
            return this._isAvailable && this._stockQuantity > 0; 
        }
        set
        {
            // Setzen auf false immer erlaubt
            // Setzen auf true nur erlaubt wenn Lagerbestand vorhanden
            if (!value || this._stockQuantity > 0)
            {
                this._isAvailable = value;
            }
            // Falls true gesetzt werden soll ohne Bestand -> ignorieren
        }
    }

    // Automatische Property für die Kategorie (Enum)
    // Kein weiterer Prüfbedarf – Enum gewährleistet bereits Typensicherheit
    public Category Category { get; set; } 

    // Property für Lagerbestand mit Validierung
    // Negative Bestände sind nicht erlaubt – würde bedeuten wir schulden Bestand
    public int StockQuantity
    {
        get { return this._stockQuantity; }    // Aktuellen Lagerstand zurückgeben
        set
        {
            // Regel: Lagerbestand darf nicht negativ sein
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