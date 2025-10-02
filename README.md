<div align="center">

# 🎓 C# Code School Repository

*A comprehensive collection of C# projects, exercises, and learning materials*

[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![.NET](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)](https://dotnet.microsoft.com/)
[![JetBrains Rider](https://img.shields.io/badge/Rider-000000.svg?style=for-the-badge&logo=Rider&logoColor=white&color=black&labelColor=crimson)](https://www.jetbrains.com/rider/)

---

*"The best way to learn programming is by writing programs"* 💻

</div>

---

## 📚 About This Repository

Welcome to my C# learning journey! This repository contains all my school projects, exercises, assignments, and experimental code as I master the fundamentals and advanced concepts of C# programming.

---

## 🗂️ Repository Structure

```
📁 Csharp-code-school/
 ├── 📂 Csharp-code-school/     # Grundlagen-Projekt mit Person/Article Klassen
 ├── 📂 End-project/            # Artikelverwaltungssystem mit Filemanagement
 ├── 📂 FileManagerProject/     # Erweiterte Artikelverwaltung mit erweiterten Features
 ├── 📂 Lesson-25-07/           # Lektion vom 25.07 mit Klassen-Beispielen
 └── 📄 README.md               # Diese Dokumentation
```

---

## 🎯 Learning Objectives

<table>
<tr>
<td width="50%">

### Core Concepts
- ✅ Object-Oriented Programming (OOP)
- ✅ Classes, Objects, and Inheritance
- ✅ Properties and Methods
- ✅ Constructors and Destructors
- ✅ Data Types and Variables
- ✅ Control Flow and Loops

</td>
<td width="50%">

### Advanced Topics
- 🔄 LINQ and Lambda Expressions
- 🔄 Exception Handling
- 🔄 File I/O Operations
- 🔄 Collections and Generics
- 🔄 Delegates and Events
- 🔄 Async/Await Programming

</td>
</tr>
</table>

---

## 🔧 Technologies & Tools

<div align="center">

| Technology          | Purpose           | Status      |
|---------------------|------------------|-------------|
| **C# (.NET 9.0)**   | Primary Language | ✅ Active   |
| **JetBrains Rider** | IDE              | ✅ Active   |
| **Git & GitHub**    | Version Control  | ✅ Active   |
| **File I/O**        | Data Persistence | ✅ Implemented |
| **Collections**     | List, Dictionary | ✅ Implemented |
| **Exception Handling** | Error Management | ✅ Implemented |
| **Console Applications** | User Interface | ✅ Implemented |
| **Visual Studio**   | Alternative IDE  | 📋 Available|
| **Cursor AI IDE**   | Alternative IDE  | 📋 Available|

</div>

---

## 📋 Current Projects

### ✅ Completed Projects

#### 🎓 **Csharp-code-school** - Grundlagen-Projekt
- **Person Class** - Vollständige OOP-Implementierung mit Properties, Konstruktoren und Validierung
- **Article Class** - Erweiterte Artikel-Klasse mit verschiedenen Datentypen und Geschäftslogik
- **Dictionary/List Management** - Kunden-Artikel-Zuordnung mit Dictionary und List-Operationen
- **Console I/O** - Benutzerinteraktion mit Eingabevalidierung und Fehlerbehandlung

#### 🏪 **End-project** - Artikelverwaltungssystem
- **FileManager Class** - Vollständiges Dateimanagement für Artikel-Persistierung
- **Menu-driven Interface** - Benutzerfreundliche Konsolenanwendung mit Hauptmenü
- **Article Management** - CRUD-Operationen für Artikel (Create, Read, Update, Delete)
- **Category Filtering** - Artikel nach Kategorien filtern und durchsuchen
- **Author Search** - Suche nach Artikeln basierend auf Autor
- **Reading Time Filter** - Filterung nach Lesezeit für Bücher

#### 🔧 **FileManagerProject** - Erweiterte Artikelverwaltung
- **Enhanced FileManager** - Verbesserte Dateioperationen mit absoluten Pfaden
- **Advanced Article Features** - Erweiterte Artikel-Eigenschaften (Preis, Beschreibung, ID-Management)
- **Automatic ID Generation** - Automatische ID-Generierung für neue Artikel
- **Book-specific Features** - Spezielle Lesezeit-Features nur für Bücher
- **Data Persistence** - Vollständige Datenpersistierung in Textdateien

#### 📚 **Lesson-25-07** - Klassen-Beispiele
- **Basic Class Examples** - Einfache Implementierung von Person und Article Klassen
- **Enum Usage** - Verwendung von Category-Enums
- **Constructor Examples** - Verschiedene Konstruktor-Patterns
- **ToString Override** - Überschreibung der ToString-Methode

### 🔄 Currently Learning
- File I/O Operations (✅ Implementiert)
- Exception Handling (✅ Implementiert)
- Collections and Generics (✅ Implementiert)
- LINQ and Lambda Expressions (🔄 In Progress)  

---

## 🎨 Code Style Guidelines

```csharp
// Following C# naming conventions:
public class MyClass          // PascalCase for classes
{
    private string _myField;  // _camelCase for private fields
    
    public string MyProperty { get; set; }  // PascalCase for properties
    
    public void MyMethod()    // PascalCase for methods
    {
        var localVariable = ""; // camelCase for local variables
    }
}
```

---

## 🔤 Data Types Mastered

| Data Type | Purpose              | Example Usage                    |
|-----------|----------------------|----------------------------------|
| `int`     | Whole numbers        | `int age = 25;`                  |
| `string`  | Text data            | `string name = "Gabriel";`       |
| `char`    | Single character     | `char initial = 'G';`            |
| `double`/`float` | Floating-point | `double price = 19.99;`          |
| `decimal` | Currency             | `decimal salary = 50000.00m;`    |
| `bool`    | True/false values    | `bool isActive = true;`          |
| `DateTime`| Date and time        | `DateTime birthday = DateTime.Now;` |

---

## 📊 Learning Progress

<div>
![Overall Progress](assets/progress-35.svg)
</div>

---

## ✅ Completed Topics

- [x] Basic Syntax and Data Types  
- [x] Classes and Objects  
- [x] Properties (Auto & Custom)  
- [x] Constructors and Method Overloading  
- [x] String Interpolation (`$"Hello {name}"`)  
- [x] Access Modifiers (private, public)  
- [x] Method Overriding (`ToString()`)  
- [x] Collections (List, Array, Dictionary)  
- [x] Exception Handling (try-catch)  
- [x] File I/O Operations (File.ReadAllLines, File.WriteAllLines)  
- [x] Enum Usage (Category enum)  
- [x] Input Validation and Error Handling  
- [x] Menu-driven Console Applications  
- [x] Data Persistence (Text File Storage)  
- [x] CRUD Operations (Create, Read, Update, Delete)  
- [x] Search and Filter Functionality  
- [x] Constructor Chaining (`: this(...)`)  
- [x] Property Validation and Business Logic  
- [x] Dictionary and List Management  
- [x] String Manipulation and Parsing

---

## 🔄 Currently Learning

- [ ] Inheritance and Polymorphism  
- [ ] Abstract Classes and Interfaces  
- [ ] LINQ and Lambda Expressions  
- [ ] Async/Await Programming  
- [ ] Database Integration (SQL Server/Entity Framework)

---

## 📅 Upcoming Assignments

- **Advanced OOP Concepts** – Inheritance and Polymorphism  
- **Database Integration** – Entity Framework und SQL Server  
- **Web API Development** – ASP.NET Core Web API  
- **Unit Testing** – NUnit oder xUnit Framework  
- **Advanced Collections** – LINQ und Lambda Expressions  

---

## 🎓 School Information

- **Course**: C# Programming Fundamentals  
- **Institution**: Code School  
- **Duration**: Academic Year 2025/2026  
- **Current Focus**: Object-Oriented Programming  

---

## 🌟 Key Learning Highlights

- **Properties vs Fields** – Difference between private fields (`_name`) and public properties (`Name { get; set; }`)  
- **Constructor Chaining** – Using `: this(...)` to reuse code across constructors  
- **String Interpolation** – Modern C# syntax using `$"..."` for embedded expressions  
- **File I/O Operations** – Reading and writing data to text files with error handling  
- **Menu-driven Applications** – Creating user-friendly console interfaces  
- **Data Validation** – Implementing business logic in property setters  
- **Collection Management** – Using Lists and Dictionaries for data organization  
- **Exception Handling** – Proper error management with try-catch blocks  
- **CRUD Operations** – Complete Create, Read, Update, Delete functionality  
- **Search and Filter** – Implementing search functionality across different criteria  

---

## 📈 Code Quality Metrics

- **Naming Conventions**: ✅ Following C# standards  
- **Documentation**: ✅ Comprehensive comments and XML documentation  
- **Error Handling**: ✅ Implemented with try-catch blocks  
- **Input Validation**: ✅ User input validation throughout applications  
- **Code Organization**: ✅ Well-structured classes and methods  
- **Business Logic**: ✅ Proper validation in property setters  
- **File Management**: ✅ Robust file I/O with error handling  
- **Unit Testing**: 📋 Planned for future projects  

---

## 📫 Connect & Collaborate

<div>
<img src="https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white" alt="GitHub"> 
<img src="https://img.shields.io/badge/Email-D14836?style=for-the-badge&logo=gmail&logoColor=white" alt="Email">
</div>

Feel free to explore the code, suggest improvements, or ask questions!  

---

📅 **Last Updated**: Januar 2025  
🎯 **Next Update**: After advanced OOP concepts implementation  
⭐ **Star this repo if you find it helpful!**  

Happy Coding! 🚀
