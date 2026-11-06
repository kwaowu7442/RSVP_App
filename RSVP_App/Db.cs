namespace RSVP_App;

public static class Db
{
    public static AppDatabase Instance { get; } = new AppDatabase();
    public static int CurrentUserId { get; set; } = 0;
    public static int SelectedEventId { get; set; } = 0;
}