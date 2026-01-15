using iOSApp1.Models;

namespace iOSApp1.Views;

public partial class RegistrationPage : ContentPage
{
    public RegistrationPage()
    {
        InitializeComponent();
    }

    private void OnRegisterClicked(object sender, EventArgs e)
    {
        var user = new User
        {
            Firstname = FirstNameEntry.Text,
            Lastname = LastNameEntry.Text,
            Email = EmailEntry.Text,
            Password = PasswordEntry.Text,
            DateOfBirth = DobPicker.Date
        };

        ResultLabel.Text = $"Registered User:\n\n{user}";
        ResultFrame.IsVisible = true;
    }
}