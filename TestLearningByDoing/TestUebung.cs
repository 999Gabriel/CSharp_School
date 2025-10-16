using TestLearningByDoing.models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace TestLearningByDoing
{
/*
 * C# Kurztest – OOP & Basics (≈20 Minuten)
   Gesamt: 20 Punkte | Hilfsmittel: Editor/Compiler erlaubt (kein Internet)
   Tipp: Antworte kurz & präzise. Bei Code: kompilierbar & minimal.
   Teil A – Theoriefragen (12 Punkte, ~12 Min)
   1) Feld vs. Property (2P)
   Erkläre den Unterschied und warum Properties in Domänenklassen zu bevorzugen sind.
   Felder speicher die Daten direkt und geben die interne Logik dar, Properties geben einem die Macht Inputs zu validieren und zu berechnen.
   2) Abgeleitete Werte nicht speichern (2P)
   Begründe an IsAvailable (abhängig von StockQuantity), warum Speichern problematisch ist und Berechnung besser.
   weil wenn wir StockQuantity ändern, müssen wir auch IsAvailable ändern, was zu Fehlern führen kann. Das passiert bei berechnung nicht.
   3) switch vs. if/else-Kaskade (2P)
   Nenne ein konkretes Beispiel, wann switch die bessere Wahl ist und warum.
   es wäre am besten zu nutzen, wenn wir viele if/else-Kaskaden haben.
   4) decimal vs. double für Geld (2P)
   Welchen Typ nimmst du für Preise – und warum? Nenne den Hauptgrund technisch korrekt.
   immer deciamal, weil es immer genau 2 Nachkommastellen hat, double nur wenn es 18 Nachkommastellen hat.
   5) Sicheres Enum-Parsing (2P)
   Wie parst du einen vom Nutzer eingegebenen Kategorienamen (z. B. „book“) zu Category fallunabhängig und ohne Exception? Beschreibe das Muster/API knapp (kein kompletter Code nötig).
   ich paare einen user eingegebenen Kategorienamen zu Category fallunabhängig und ohne Exception mit Enum.TryParse<Category>(string, bool ignoreCase, out Category result)
   6) TryParse vs. Convert (2P)
   Unterschied zwischen int.TryParse und Convert.ToInt32(string) bzgl. Fehlerverhalten. Wann nutzt du welches?
   der Unterschied zwischen int.TryParse und Convert.ToInt32(string) ist, dass TryParse keinen Fehler wirft, sondern
   false zurückgibt, wenn die Konvertierung fehlschlägt. Convert.ToInt32(string) wirft eine FormatException, wenn
   die Konvertierung fehlschlägt. Ich nutze TryParse, wenn ich unsichere Eingaben habe und keine Exception werfen
   möchte. Ich nutze Convert.ToInt32, wenn ich sichere Eingaben habe und eine Exception werfen möchte, wenn die
   Eingabe ungültig ist.
   Teil B – Code-Review (4 Punkte, ~4 Min)
   Lies den Ausschnitt und markiere zwei konkrete Design-/Fehlerstellen. Begründe jeweils kurz.
   */
  public class Article
  {
    public string ArticleName;
    public decimal Price;
    public int StockQuantity;
    public bool IsAvailable; // wird gesetzt

    public Article(string name, decimal price, int stock)
    {
      ArticleName = name;
      Price = price;
      StockQuantity = stock;
      IsAvailable = StockQuantity > 0;
    }

    public override string ToString()
    {
      return "Name: " + ArticleName + ", Preis: " + Price;
      /*--> die ToString Methode ist fehlerheaft, sollte mehr so aussehen*/
      return $"Artikelname: {ArticleName}, Preis: {Price}";
    }
  }
//den zweiten Fehler habe ich nicht gefunden 
/*
   Schreibe jeweils: [Stelle] → [Problem] → [kurze Begründung / Verbesserung]
   Teil C – Kleine Code-Aufgaben (4 Punkte, ~4 Min)
   C1) Robuste Eingabe nicht-leerer Strings (2P)
   Schreibe eine kleine Methode ReadNonEmpty(string prompt), die in einer Schleife nach Eingabe fragt, Trimming vornimmt und nur bei nicht-leer zurückkehrt.
   */

  public static string ReadNonEmpty(string prompt)
  {
    string? input;
    do
    {
      Console.Write(prompt);
      input = Console.ReadLine()?.Trim();
    } while (string.IsNullOrEmpty(input));

    return input;
  }
/*
   C2) Artikel-Konstruktor mit Fail-Fast (2P)
   Schreibe einen Vollkonstruktor für Article mit Parametern (string articleName, Category category, decimal price, int stockQuantity), der:
   Namen trimmt und auf nicht-leer prüft (leerer Name → ArgumentException)
   price >= 0m und stockQuantity >= 0 erzwingt (sonst ArgumentOutOfRangeException)
   IsAvailable nicht speichert, sondern als berechnete Property => StockQuantity > 0 vorsieht
   (Hinweis: Du darfst die Property-Signaturen skizzieren; Fokus ist auf dem Konstruktor und dem Prinzip.)
   Bonus (keine Pflicht, 0–2P Extra)
   */
  public class ArticleWithConstructor
  {
    string ArticleName;
    Category Category;
    decimal Price;
    int StockQuantity;
    bool IsAvailable; // wird gesetzt

    public ArticleWithConstructor(string articleName, Category category, decimal price, int stockQuantity)
    {
      ArticleName = articleName.Trim();
      Category = category;
      Price = price >= 0m
        ? price
        : throw new ArgumentOutOfRangeException(nameof(price), ">= 0 erwartet.");
      StockQuantity = stockQuantity >= 0
        ? stockQuantity
        : throw new ArgumentOutOfRangeException(nameof(stockQuantity), ">= 0 erwartet.");
      IsAvailable = StockQuantity > 0;
    }
  }
  /*
   B1) decimal.TryParse so nutzen, dass sowohl 12,99 als auch 12.99 akzeptiert werden. Skizziere das Parsing-Muster (Kulturhinweis reicht, kein Vollprogramm).
   Abgabe-Hinweis
   Theorie: Stichpunkte reichen, wenn sie technisch korrekt sind.
   Code: Kurz & kompilierbar (keine überflüssigen Klassen).
   Benenne bei Teil B die Stellen präzise (z. B. „public string ArticleName;“).
 */
  public class TestUebung
  {
    public static void Main(string[] args)
    {
      // Beispiel für decimal.TryParse mit Kulturinfo
      string input = "12,99"; // oder "12.99"
      if (decimal.TryParse(input, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.GetCultureInfo("de-DE"), out decimal resultDe))
      {
        Console.WriteLine($"Erfolgreich geparst (de-DE): {resultDe}");
      }
      else if (decimal.TryParse(input, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.GetCultureInfo("en-US"), out decimal resultUs))
      {
        Console.WriteLine($"Erfolgreich geparst (en-US): {resultUs}");
      }
      else
      {
        Console.WriteLine("Ungültiges Format.");
      }
    }
  }

// ============================================================================
// TestLearningByDoing – Konsolidierte Vergleichsdatei
// Inhalt: Theoriefragen (mit korrigierten Musterantworten), Code-Review,
//         korrigierte Klassen (Article), Hilfsmethoden (ReadNonEmpty),
//         Parsing-Demo für decimal (12,99 / 12.99), kleines Demo-Programm.
//
// Zweck: 1) Direkter Vergleich mit deiner Lösung
//        2) „Perfekte“ Referenz gemäß euren OOP-Guidelines (eine Datenquelle,
//           Fail-Fast, berechnete Properties, ToString nur über Properties)
//
// Hinweis: Nur EIN Main-Einstiegspunkt (Program.Main). Alle Demos sind
//          in eigene Methoden ausgelagert, damit der Code kompakt bleibt.
// ============================================================================



  // ==========================================================================
  // TEIL A – THEORIE (korrigierte Musterantworten, kurz & präzise)
  // ==========================================================================

  /*
   1) Feld vs. Property
      - Feld: roher Datenspeicher ohne Kapselung/Validierung.
      - Property: kapselt Zugriff (Getter/Setter), erlaubt Validierung,
        Änderungslogik, Datenbindung/Serialisierung. In Domänenklassen
        sind Properties zu bevorzugen (eine Quelle der Wahrheit, robust).

   2) Abgeleitete Werte nicht speichern (z. B. IsAvailable)
      - Speichern führt zu „Drift“ (veraltete Zustände), wenn Basiswerte
        (StockQuantity) sich ändern. Berechnen (=>) hält es immer konsistent.

   3) switch vs. if/else-Kaskade
      - switch ist besser bei Gleichheitsvergleichen auf diskrete Werte
        (z. B. Enum Category). Ergebnis: klarer, wartbarer, weniger Fehler.

   4) decimal vs. double für Geld
      - decimal ist dezimal-basiert (28–29 Stellen) → vermeidet binäre
        Rundungsfehler bei Geld. double ist binär-Gleitkomma → unpräzise.

   5) Sicheres Enum-Parsing
      - Enum.TryParse<Category>(input, ignoreCase: true, out var cat)
        → kein Throw, case-insensitive.

   6) TryParse vs. Convert
      - int.TryParse: gibt false bei Fehler (keine Exception), ideal für unsichere Eingaben.
      - Convert.ToInt32(string): wirft FormatException bei ungültiger Eingabe;
        sinnvoll, wenn Eingabe garantiert korrekt sein sollte.
        (Sonderfall: Convert.ToInt32(null) → 0)
  */

  // ==========================================================================
  // TEIL B – CODE-REVIEW (Originalausschnitt + Markierung der Probleme)
  // ==========================================================================

  /*
   Original (vereinfachter Problem-Code):

   public class Article
   {
     public string ArticleName;
     public decimal Price;
     public int StockQuantity;
     public bool IsAvailable; // wird gesetzt

     public Article(string name, decimal price, int stock)
     {
       ArticleName = name;
       Price = price;
       StockQuantity = stock;
       IsAvailable = StockQuantity > 0;
     }

     public override string ToString()
     {
       return "Name: " + ArticleName + ", Preis: " + Price;
       return $"Artikelname: {ArticleName}, Preis: {Price}";
     }
   }

   Probleme (mind. zwei nennen):
   1) Öffentliche Felder (ArticleName/Price/StockQuantity/IsAvailable)
      -> Keine Kapselung/Validierung möglich. Verbesserung: Properties mit
         privaten Settern + Validierung im Konstruktor.

   2) Abgeleiteter Wert IsAvailable wird gespeichert
      -> Drift-Gefahr. Verbesserung: berechnete Property
         public bool IsAvailable => StockQuantity > 0;

   3) Keine Validierung im Konstruktor
      -> Name darf nicht leer sein; Price/Stock müssen >= 0. Fail-Fast einbauen.

   4) ToString mit doppeltem return (zweiter unreachable)
      -> Genau EIN return; außerdem Währungsformatierung {Price:C} verwenden.
  */

  // ==========================================================================
  // ENUMS – Kategorien für Artikel
  // ==========================================================================

  /// <summary>
  /// Vordefinierte Kategorien für Artikel.
  /// </summary>
  public enum Category
  {
    NotSpecified,
    Book,
    Shoes,
    Computer,
    Handy,
    Laptop
  }

  // ==========================================================================
  // TEIL C – KORRIGIERTE DOMÄNENKLASSE „Article“
  // Prinzipien:
  // - Eine Quelle der Wahrheit (nur Properties, keine doppelten Felder)
  // - Fail-Fast-Validierung im Vollkonstruktor
  // - Abgeleitete Werte nicht speichern
  // - ToString nutzt nur Properties und formatiert Kultur-bewusst
  // ==========================================================================

  /// <summary>
  /// Repräsentiert einen Artikel inkl. Preis, Lagerstand und Kategorie.
  /// </summary>
  public class Article
  {
    // Properties (eine Datenquelle; von außen nicht frei setzbar)
    public string ArticleName { get; private set; }
    public Category Category { get; private set; }
    public decimal Price { get; private set; }
    public int StockQuantity { get; private set; }

    // Berechnete Eigenschaft (nicht speichern!)
    public bool IsAvailable => StockQuantity > 0;

    /// <summary>
    /// Vollkonstruktor mit Fail-Fast-Validierung.
    /// </summary>
    public Article(string articleName, Category category, decimal price, int stockQuantity)
    {
      if (string.IsNullOrWhiteSpace(articleName))
      {
        throw new ArgumentException("Darf nicht leer sein.", nameof(articleName));
      }

      if (price < 0m)
      {
        throw new ArgumentOutOfRangeException(nameof(price), ">= 0 erwartet.");
      }

      if (stockQuantity < 0)
      {
        throw new ArgumentOutOfRangeException(nameof(stockQuantity), ">= 0 erwartet.");
      }

      ArticleName = articleName.Trim();
      Category = category;
      Price = price;
      StockQuantity = stockQuantity;
    }

    /// <summary>
    /// Kettenkonstruktor mit Defaults (Preis=0, Stock=0).
    /// </summary>
    public Article(string articleName, Category category) : this(articleName, category, 0m, 0) 
    { }

    /// <summary>
    /// Parameterloser Konstruktor (alle Defaults).
    /// </summary>
    public Article() : this("", Category.NotSpecified, 0m, 0) { }

    /// <summary>
    /// Lesbare, kulturabhängige Ausgabe für Debug/Logs.
    /// </summary>
    public override string ToString()
    {
      return $"Name: {ArticleName} | Category: {Category} | " +
             $"Price: {Price:C} | Stock: {StockQuantity} | " +
             $"Avail: {(IsAvailable ? "Yes" : "No")}";
    }

    // Beispielhafte Änder-APIs mit Validierung (optional nützlich)
    public void IncreaseStock(int amount)
    {
      if (amount <= 0)
      {
        throw new ArgumentOutOfRangeException(nameof(amount), "> 0 erwartet.");
      }
      StockQuantity += amount;
    }

    public void DecreaseStock(int amount)
    {
      if (amount <= 0)
      {
        throw new ArgumentOutOfRangeException(nameof(amount), "> 0 erwartet.");
      }
      if (amount > StockQuantity)
      {
        throw new InvalidOperationException("Nicht genug Lagerbestand.");
      }
      StockQuantity -= amount;
    }
  }

  // ==========================================================================
  // HILFSMETHODEN – Konsoleingaben
  // ==========================================================================

  /// <summary>
  /// InputHelpers: Sammlung kleiner, fokussierter Konsolen-Reader.
  /// </summary>
  public static class InputHelpers
  {
    /// <summary>
    /// Liest eine nicht-leere, getrimmte Eingabe; fragt bei Fehler erneut.
    /// </summary>
    public static string ReadNonEmpty(string prompt)
    {
      while (true)
      {
        Console.Write(prompt);
        string? input = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(input))
          return input.Trim();

        Console.WriteLine("Fehler: Eingabe darf nicht leer sein.");
      }
    }

    /// <summary>
    /// Parst decimal robust (akzeptiert 12,99 und 12.99) über Kulturen.
    /// </summary>
    public static bool TryParseDecimalFlexible(string input, out decimal value)
    {
      // Erst "de-DE", dann "en-US" probieren (beide gängig im Alltag).
      if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.GetCultureInfo("de-DE"), out value))
        return true;

      if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out value))
        return true;
      
      return false;
    }
  }

  // ==========================================================================
  // DEMO-HILFSKLASSE – zeigt decimal-Parsing mit Kultur-Fallback
  // ==========================================================================

  /// <summary>
  /// Kleine Demo ohne eigenen Main – wird aus Program.Main aufgerufen.
  /// </summary>
  public static class ParsingDemo
  {
    public static void ShowDecimalParsing(string input)
    {
      if (InputHelpers.TryParseDecimalFlexible(input, out var val))
      {
        Console.WriteLine($"Parsing OK: {val}");
      }
      else
      {
        Console.WriteLine("Parsing fehlgeschlagen.");
      }
    }
  }

  // ==========================================================================
  // PROGRAM – Einstiegspunkt mit kurzer Demo (ersetzt mehrere Mains)
  // ==========================================================================

  public class Program
  {
    public static void Main(string[] args)
    {
      Console.WriteLine("=== Kurztest-Referenz – Demo ===");

      // 1) ReadNonEmpty (C1)
      string first = InputHelpers.ReadNonEmpty("Vorname: ");
      string last  = InputHelpers.ReadNonEmpty("Nachname: ");
      Console.WriteLine($"Person: {last}, {first}");

      // 2) Article-Konstruktion (C2 – Fail-Fast & berechnete Property)
      try
      {
        var a = new Article("Clean Code", Category.Book, 29.90m, 3);
        Console.WriteLine(a.ToString());
      }
      catch (Exception ex)
      {
        Console.WriteLine("Fehler bei Article: " + ex.Message);
      }

      // 3) Decimal-Parsing für „12,99“ oder „12.99“ (Bonus)
      Console.Write("Preis eingeben (z. B. 12,99 / 12.99): ");
      string? priceInput = Console.ReadLine() ?? "";
      ParsingDemo.ShowDecimalParsing(priceInput);

      Console.WriteLine("=== Ende Demo ===");
    }
  }
}