namespace RSVP_App;

public partial class EventsPage : ContentPage
{
    public EventsPage()
    {
        InitializeComponent();
        WelcomeLabel.Text = AppState.IsGuest ? "Welcome, Guest" : $"Welcome, {AppState.UserName}";
    }

    private async void OnLogOutClicked(object sender, EventArgs e)
    {
        AppState.IsGuest = true;
        await Shell.Current.GoToAsync("//LoginPage");
    }
}