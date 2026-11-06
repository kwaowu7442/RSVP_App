namespace RSVP_App;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(AddUserPage), typeof(AddUserPage));
        Routing.RegisterRoute(nameof(AddEventPage), typeof(AddEventPage));
        Routing.RegisterRoute(nameof(RsvpPage), typeof(RsvpPage));
        Routing.RegisterRoute(nameof(EventsPage), typeof(EventsPage));
    }
}