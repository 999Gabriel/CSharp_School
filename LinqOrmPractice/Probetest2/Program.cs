using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Probetest2
{
    // ==========================================
    // MODEL KLASSEN (Vorgabe)
    // ==========================================

    public enum Gender
    {
        Male,
        Female,
        None
    }

    public class BankAccount
    {
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }
        public decimal Balance { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========== PROBETEST 2 ==========");

            /*
             * AUFGABE 1: THEORIE
             *
             * Erkläre alles, was in einer Klasse enthalten ist, möglichst genau.
             *
             * ANTWORT:
             * in einer Klasse sind folgende Elemente enthalten:
             * wir haben Eigenschaften (Properties), die den Zustand der Klasse beschreiben.
             * wir haben Methoden (Funktionen), die das Verhalten der Klasse definieren.
             * und wir haben Konstruktoren, die verwendet werden, um Objekte der Klasse zu erstellen und zu initialisieren.
             * aber auch getter und setter Methoden, die den Zugriff auf die Eigenschaften der Klasse kontrollieren.
             *
             */


            /*
             * AUFGABE 2: DATEI-HANDLING
             *
             * Gegeben sind folgende Daten:
             * Server=https://localhost:3306
             * Database=MyDb
             * User=Pedro
             * Password=123Geheim
             *
             * a) Schreib eine Methode (SaveToFile), welche diese Daten in einer Datei abspeichert.
             *    - Der Methode sollen diese Daten als List<string> übergeben werden.
             *    - Die Methode soll Exceptionhandling (Klasse Exception) beinhalten.
             *    - Die Methode soll zurückliefern (bool), ob es funktioniert hat oder nicht.
             *
             * b) Verwende die Methode aus a) im Hauptprogramm und gib eine entsprechende Meldung aus.
             */
            Console.WriteLine("\n--- Aufgabe 2: Datei-Handling ---");

            // Daten vorbereiten
            List<string> configData = new List<string>
            {
                "Server=https://localhost:3306",
                "Database=MyDb",
                "User=Pedro",
                "Password=123Geheim"
            };

            string filePath = "config.txt";

            // Dein Code für b) hier:
            bool success = SaveToFile(configData, filePath);
            if (success)
            {
                Console.WriteLine("Daten erfolgreich in der Datei gespeichert.");
            }
            else
            {
                Console.WriteLine("Es gab einen Fehler beim Speichern der Datei.");
            }

            /*
             * AUFGABE 3: LINQ
             *
             * Es existiert die Klasse Bankaccount (AccountId, Vorname, Nachname, Geburtstag, Geschlecht, Kontobetrag)
             * und eine Liste von Bankaccounts.
             */

            // Dummy-Daten erstellen
            var accounts = new List<BankAccount>
            {
                new BankAccount
                {
                    AccountId = 1, FirstName = "Max", LastName = "Mustermann", Birthday = new DateTime(1990, 1, 1),
                    Gender = Gender.Male, Balance = -1500m
                },
                new BankAccount
                {
                    AccountId = 2, FirstName = "Anna", LastName = "Schmidt", Birthday = new DateTime(1995, 5, 10),
                    Gender = Gender.Female, Balance = 2000m
                },
                new BankAccount
                {
                    AccountId = 3, FirstName = "John", LastName = "Doe", Birthday = new DateTime(1985, 3, 15),
                    Gender = Gender.Male, Balance = -500m
                },
                new BankAccount
                {
                    AccountId = 4, FirstName = "Maria", LastName = "Muster", Birthday = new DateTime(1992, 7, 20),
                    Gender = Gender.Female, Balance = -1200m
                },
                new BankAccount
                {
                    AccountId = 5, FirstName = "Peter", LastName = "Pan", Birthday = new DateTime(2000, 12, 1),
                    Gender = Gender.Male, Balance = -2000m
                }
            };

            /*
             * a) Erstelle folgende LINQ-Abfrage - in der ABFRAGE-SYNTAX (Query Syntax)!
             *    Ermittle alle Accounts von männlichen Kunden, welche mindestens einen negativen Betrag von 1000€ am Konto haben
             *    (also Balance <= -1000).
             *    Wähle nur die Felder AccountId, Nachname, Kontobetrag und Gender (Projektion) aus.
             *    Sortiere die Accounts nach dem Nachnamen.
             *    Keine Ausgabe notwendig (nur die Query erstellen).
             */
            Console.WriteLine("\n--- Aufgabe 3a: LINQ Query Syntax ---");
            // Dein Code hier:
            var filteredAccounts = from account in accounts
                where account.Gender == Gender.Male && account.Balance <= -1000m
                select account;

            foreach (var acc in filteredAccounts)
            {
                Console.WriteLine(
                    $"AccountId: {acc.AccountId}, Nachname: {acc.LastName}, Kontobetrag: {acc.Balance}, " +
                    $"Geschlecht: {acc.Gender}");
            }


            /*
             * b) Ermittle alle Accounts, gruppiert nach dem Geschlecht.
             *    Gib die Gruppierung (Key), und darunter alle Accounts pro Gruppierung, am Bildschirm aus.
             */
            Console.WriteLine("\n--- Aufgabe 3b: LINQ Grouping ---");
            // Dein Code hier:
            var groupedAccounts = from account in accounts
                group account by account.Gender;

            foreach (var groupedAccount in groupedAccounts)
            {
                Console.WriteLine($"Geschlecht: {groupedAccount.Key}");
                foreach (var acc in groupedAccount)
                {
                    Console.WriteLine(
                        $"  AccountId: {acc.AccountId}, Nachname: {acc.LastName}, Kontobetrag: {acc.Balance}");
                }
            }


            /*
             * c) Erkläre Lambda-Ausdrücke genau - auch anhand eines Beispiels.
             *
             * ANTWORT:
             * Lambda-Ausdrücke sind anonyme Funktionen, die verwendet werden, um kurze Codeblöcke zu definieren.
             * Sie werden oft in LINQ-Abfragen verwendet, um Operationen wie Filtern, Sortieren und Transformieren von Daten durchzuführen.
             * Ein Lambda-Ausdruck besteht aus Eingabeparametern, dem Lambda-Operator "=>" und einem Ausdruck oder Block von Anweisungen.
             * Beispiel:
             * var evenNumbers = numbers.Where(n => n % 2 == 0).ToList();
             * In diesem Beispiel filtert der Lambda-Ausdruck "n => n % 2 == 0" die geraden Zahlen aus der Liste "numbers".
             *
             */

            Console.WriteLine("\nDrücke eine Taste zum Beenden...");
            Console.ReadKey();
        }

        // Hier die Methode für Aufgabe 2a implementieren
        public static bool SaveToFile(List<string> data, string filePath)
        {
            try
            {
                File.WriteAllLines(filePath, data);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Speichern der Datei: {ex.Message}");
                return false;
            }
        }
    }
}


