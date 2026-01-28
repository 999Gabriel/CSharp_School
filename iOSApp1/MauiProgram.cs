using CommunityToolkit.Maui;
using iOSApp1.Views;
using iOSApp1.ViewModels;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

namespace iOSApp1;

// MAUI ... Multi-Platform App UI
/*
 * --> wir schreiben den code mit C#, die Oberfläche wird mit XAML erzeut
 *
 * der Compilor erzeugt nativen Plattformcode
 * (App sieht auf Android wie eine Android App aus, auf IOS sieht es aus wie IOS eben, usw)
 *
 *
 * Alternativen zu MAUI:
 *      #Flutter ... eigene Rendering-Engine (App sieht überall gleich aus)
 *      #React Native
 *
 *
 * wir verwenden MAUI mit MVVM
 *      MVVM ... Model-View-ViewModel
 *        --> Architekturmuster
 *      Models ... hier befinden sich die Datenklassen (z.B.: Artikel, User, ...)
 *          --> C#
 *      Views ... hier befinden sich die Oberflächenklassen (z.B.: LoginView, ArtikelView, ...)
 *          --> XAML
 *
 *      ViewModels ... hier befinden sich die Logikklassen (z.B.: LoginViewModel, ArtikelViewModel, ...)
 *          --> C#
 *
 *
 *
 *     UI:
 *          wird mit XAML erzeugt
 *          besteht aus Komponenten (Button, Label, DatePicker, ...)
 *
 *          WICHTIG:
 *              damit das Layout auf unterschiedlichen Geräten (Handy, Laptop, ...)
 *              gut aussieht, müssen LayoutContainer verwendet werden
 *                  1. VerticalStackLayout -- die Komponenten werden vertikal angeordnet
 *                  2. HorizontalStackLayout -- die Komponenten werden horizontal angeordnet
 *                  3. Grid -- ist ein tabellenartiger Container also besteht aus Zeilen und Spalten
 *                  ... 
 *                  
                    LayoutContainer werden häufig verschaltelt bzw geschachtelt
       
       ViewModel
        notwendige Pakete
         CommunityToolkit.Mvvm ... erleichtert das Arbeiten mit MVVM
         CommunityToolkit.Maui ... weiteres Hilfpaket für MAUI
         
        Sinn (Vor-/Nachteile)
        
        - z.B.: könnte ein Design die UI in XAML erstellen (es sind hierfür keine Programmierkenntnisse nötig)
        - die Logik (C#-Code) ist komplett vom UI getrennt
        - die ViewModels können gut getestet werden (Unit-Tests)
        - ist komplexer (relativiert sich nach einigen Projekten)
        
        Bindings                 
        
            DataBindings ... verbindet die Eingabefelder (UI) mit den Eigenschaften (ViewModels)
            CommandBindings ... verbindet UI-Elemente (bsp.: Buttons) mit Methoden in den ViewModels
        
        ViewModel - ist eine ganz normale Klasse
        
        View mit Model verbinen
            
            ...xaml.cs
            
                im ctor die Verbindung herstellen
        
        View und das ViewModel im DI-Container registrieren (MauiProgram.cs)
        
        ===============================================================================================================
        MVVM-Schritte
        
            1. View inklusive Komponenten + Layout erzeugen
                    
            2. ViewModel erzeugen (erbt von ObservableObject) und beinhaltet 
                    bestimmte Annotations (--> Compilor erzeugt daraus zusätzlichen Code)
            3. View und ViewModel verbinden (in ...xaml.cs)
            4. DI-Container registrieren (Verbindung zwischen View und ViewModel) --> (in MauiProgram.cs)
            5. DataBinding und CommandBinding (View und ViewModel)
        ===============================================================================================================
 */

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                // fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                // fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("TimesNewRoman.ttf", "TNR");
            });

        // DI-Container registrieren
        // DI ... Dependency Injection --> Abhängigkeiten injektieren
        // DI Container ist ein Software Teil, der bereits vorhanden ist
        //      wir müssen dem DI-Container nur den Namen der Klasse
        //      (RegistrationPage und RegistrationViewModel) bekanntgeben -- er erzeugt die Instanz für uns
        // (auch die Freigabe wird von ihm ausgeführt)
        builder.Services.AddTransient<RegistrationPage>();
        builder.Services.AddTransient<RegistrationViewModel>();

        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<LoginViewModel>();
        
        return builder.Build();
    }
}
