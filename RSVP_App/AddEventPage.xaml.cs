namespace RSVP_App;

public partial class AddEventPage : ContentPage
{
    public AddEventPage()
    {
        InitializeComponent();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TitleEntry.Text) ||
            string.IsNullOrWhiteSpace(DescEntry.Text) ||
            string.IsNullOrWhiteSpace(LocationEntry.Text))
        {
            MessageLabel.Text = "Please fill in all fields";
            return;
        }

        await Db.Instance.AddEventAsync(new Event
        {
            Title = TitleEntry.Text.Trim(),
            Description = DescEntry.Text.Trim(),
            Location = LocationEntry.Text.Trim(),
            EventDate = DatePick.Date + TimePick.Time,
            HostUserId = Db.CurrentUserId
        });

        await Shell.Current.GoToAsync("..");
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}