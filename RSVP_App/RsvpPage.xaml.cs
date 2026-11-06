

namespace RSVP_App;

public partial class RsvpPage : ContentPage
{
    public RsvpPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var ev = await Db.Instance.GetEventAsync(Db.SelectedEventId);
        if (ev != null)
        {
            TitleLabel.Text = ev.Title;
            DetailsLabel.Text = $"{ev.Description}\nWhen: {ev.EventDate:MMM d, yyyy h:mm tt}\nWhere: {ev.Location}";
        }
        if (!AppState.IsGuest && Db.CurrentUserId > 0)
        {
            NameEntry.Text = AppState.UserName;
            EmailEntry.Text = AppState.UserEmail;
        }
    }

    private async void OnRsvpClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameEntry.Text) ||
            string.IsNullOrWhiteSpace(EmailEntry.Text) ||
            !int.TryParse(GuestsEntry.Text, out int guests))
        {
            MessageLabel.Text = "Please fill in all fields";
            return;
        }

        await Db.Instance.AddRsvpAsync(new Rsvp
        {
            EventId = Db.SelectedEventId,
            UserId = Db.CurrentUserId,
            Name = NameEntry.Text.Trim(),
            Email = EmailEntry.Text.Trim(),
            NumberOfGuests = guests
        });

        await DisplayAlert("Thank you", "Your RSVP has been saved.", "OK");
        await Shell.Current.GoToAsync("..");
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}