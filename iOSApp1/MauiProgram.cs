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
                     
 */

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                // fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                // fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        return builder.Build();
    }
}

