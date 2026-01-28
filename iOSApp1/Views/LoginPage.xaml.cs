
using iOSApp1.ViewModels;

namespace iOSApp1.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel lvm)
    {
        InitializeComponent();
        this.BindingContext = lvm;
    }
}
