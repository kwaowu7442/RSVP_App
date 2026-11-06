namespace RSVP_App;

public partial class EventsPage : ContentPage
{
    public EventsPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        bool loggedIn = !AppState.IsGuest && Db.CurrentUserId > 0;
        AttendingBtn.IsVisible = loggedIn;
        HostingBtn.IsVisible = loggedIn;
        AddEventBtn.IsVisible = loggedIn;
        WelcomeLabel.Text = loggedIn ? $"Welcome, {AppState.UserName}" : "Welcome, Guest";
        await LoadEvents("all");
    }

    private async Task LoadEvents(string mode)
    {
        List<Event> list;
        if (mode == "attending")
        {
            list = await Db.Instance.GetAttendingEventsAsync(Db.CurrentUserId);
            ListLabel.Text = "Events I'm attending";
        }
        else if (mode == "hosting")
        {
            list = await Db.Instance.GetHostedEventsAsync(Db.CurrentUserId);
            ListLabel.Text = "Events I'm hosting";
        }
        else
        {
            list = await Db.Instance.GetAllEventsAsync();
            ListLabel.Text = "All events";
        }
        EventsView.ItemsSource = list;
        EmptyLabel.Text = list.Count == 0 ? "No events to show." : "";
    }

    private async void OnAllClicked(object sender, EventArgs e) => await LoadEvents("all");
    private async void OnAttendingClicked(object sender, EventArgs e) => await LoadEvents("attending");
    private async void OnHostingClicked(object sender, EventArgs e) => await LoadEvents("hosting");

    private async void OnAddEventClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddEventPage));
    }

    private async void OnEventSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Event ev)
        {
            await DisplayAlert(ev.Title,
                $"{ev.Description}\n\nWhen: {ev.EventDate:MMM d, yyyy h:mm tt}\nWhere: {ev.Location}", "OK");
            EventsView.SelectedItem = null;
        }
    }

    private async void OnLogOutClicked(object sender, EventArgs e)
    {
        AppState.IsGuest = true;
        Db.CurrentUserId = 0;
        await Shell.Current.GoToAsync("//LoginPage");
    }
}