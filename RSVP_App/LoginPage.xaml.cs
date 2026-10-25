using System.Net.NetworkInformation;

namespace RSVP_App;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        if (EmailEntry.Text == "user@rsvp.com" && PasswordEntry.Text == "Password1")
        {
            AppState.IsGuest = false;
            AppState.UserName = "Kwadwo Owusu";
            AppState.UserEmail = "user@rsvp.com";
            AppState.UserPhone = "555-0100";
            MessageLabel.Text = "";
            await Shell.Current.GoToAsync(nameof(EventsPage));
        }
        else
        {
            MessageLabel.Text = "Invalid email or password";
        }
    }

    private async void OnGuestClicked(object sender, EventArgs e)
    {
        AppState.IsGuest = true;
        AppState.UserName = "";
        AppState.UserEmail = "";
        AppState.UserPhone = "";
        await Shell.Current.GoToAsync(nameof(EventsPage));
    }

    private async void OnCreateClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddUserPage));
    }
}
