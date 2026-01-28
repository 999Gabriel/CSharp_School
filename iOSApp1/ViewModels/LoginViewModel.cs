namespace iOSApp1.ViewModels;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

public partial class LoginViewModel : ObservableObject
{
    [ObservableProperty] 
    private string _email;

    [ObservableProperty] 
    private string _password;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    [ObservableProperty]
    private bool _isResultVisible;

    [ObservableProperty]
    private Color _statusColor = Colors.Black;

    [RelayCommand]
    private async Task Login()
    {
        // Validierung der Eingaben
        if (string.IsNullOrWhiteSpace(Email))
        {
            StatusMessage = "Bitte geben Sie Ihre E-Mail ein.";
            StatusColor = Colors.Red;
            IsResultVisible = true;
            return;
        }

        if (string.IsNullOrWhiteSpace(Password))
        {
            StatusMessage = "Bitte geben Sie Ihr Passwort ein.";
            StatusColor = Colors.Red;
            IsResultVisible = true;
            return;
        }

        // Hier würde normalerweise der WebAPI Aufruf stattfinden
        // Wir simulieren das Warten auf die Antwort
        await Task.Delay(1000);

        // Erfolg simulieren
        StatusMessage = $"Login erfolgreich!\nWillkommen, {Email}";
        StatusColor = Colors.Green;
        IsResultVisible = true;
        
        // Optional: Navigation
        // await Shell.Current.GoToAsync("//MainPage");
    }

    [RelayCommand]
    private async Task GoToRegister()
    {
        await Shell.Current.GoToAsync("//RegistrationPage");
    }
}

