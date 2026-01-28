namespace iOSApp1.ViewModels;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


// partial ... wir schreiben nur einen geringen Teil der Klasse
//          den Rest wird vom Compiler erzeugt
//          deshalb muss die Klasse mit "partial" gekennzeichnet werden
public partial class RegistrationViewModel : ObservableObject
{
    // --> Compilor erzeugt für uns zusätzlichen Code
    [NotifyCanExecuteChangedFor(nameof(RegisterUserCommand))]
    [ObservableProperty] private string _firstname; 

    [NotifyCanExecuteChangedFor(nameof(RegisterUserCommand))]
    [ObservableProperty] private string _lastname;

    [NotifyCanExecuteChangedFor(nameof(RegisterUserCommand))]
    [ObservableProperty] private string _email;

    [NotifyCanExecuteChangedFor(nameof(RegisterUserCommand))]
    [ObservableProperty] private string _password;
    [ObservableProperty]
    private string _statusMessage = string.Empty;
    [ObservableProperty]
    private bool _isResultVisible;

    [ObservableProperty]
    private Color _statusColor = Colors.Black;

    public RegistrationViewModel()
    {
        // nurmehr das vom Compiler erzeugte Property verwenden
        //      --> ansonsten funktioniert DataBindung NICHT
        this.Firstname = "Gabi";
        this.Lastname = "Mustermann";
        this.Email = "999gabriel.winkler@gmail.com";
        this.Password = "12345678";
    }

    // registerUserAsynch
    [RelayCommand(CanExecute = nameof(FormOk))] // --> Compilor erzeugt für uns zusätzlichen Code
    public async Task RegisterUser()
    {
        // hier kommt der Code für die Registrierung
        // Was soll passieren
        // 1. Eingabedaten validieren
        
        // 2. Daten speichern -- WebAPI verwenden
        
        // 3. Meldung über Erfolg anzeigen
        await Task.Delay(1000);

        // Erfolg simulieren
        StatusMessage = $"Registrierung erfolgreich!\nViel Spaß mit der App {Firstname}";
        StatusColor = Colors.Green;
        IsResultVisible = true;
    }

    private bool FormOk()
    {
        // hier kommt die Validierung hin
        if (this.Firstname.Trim().Length < 1)
        {
            return false;
        }
        // usw für die restlichen Eingabefelder
        if (this.Lastname.Trim().Length < 1)
        {
            return false;
        }
        if (this.Email.Trim().Length < 8 || !this.Email.Contains("@"))
        {
            return false;
        }
        if (this.Password.Trim().Length < 8)
        {
            return false;
        }
        return true;
    }
}