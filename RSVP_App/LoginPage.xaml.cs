namespace RSVP_App;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var user = await Db.Instance.LoginAsync(EmailEntry.Text?.Trim() ?? "", PasswordEntry.Text ?? "");
        if (user != null)
        {
            Db.CurrentUserId = user.UserId;
            AppState.IsGuest = false;
            AppState.UserName = user.FirstName;
            AppState.UserEmail = user.Email;
            AppState.UserPhone = user.Phone;
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
        Db.CurrentUserId = 0;
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