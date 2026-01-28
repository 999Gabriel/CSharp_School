using iOSApp1.Models;
using iOSApp1.ViewModels;
using iOSApp1.Views;

namespace iOSApp1.Views;

public partial class RegistrationPage : ContentPage
{
    // hier wird vom DI-Container die von ihm erzeugte Instanz des ViewModels an den ctor übergeben
    // DI: hier wird die abhängige Klasse injeziert
    public RegistrationPage(RegistrationViewModel vm)
    {
        InitializeComponent();
        // die View (RegistrationPage.xaml) wird mit dem
        // ViewModel (RegistrationPageViewModel.) verbunden
        this.BindingContext = vm;
    }
}