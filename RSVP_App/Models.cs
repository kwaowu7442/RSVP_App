using SQLite;
namespace RSVP_App;

public class User
{
    [PrimaryKey, AutoIncrement]
    public int UserId { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    [Unique]
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Password { get; set; } = "";
}

public class Event
{
    [PrimaryKey, AutoIncrement]
    public int EventId { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Location { get; set; } = "";
    public DateTime EventDate { get; set; }
    public int HostUserId { get; set; }
}

public class Rsvp
{
    [PrimaryKey, AutoIncrement]
    public int RsvpId { get; set; }
    public int EventId { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public int NumberOfGuests { get; set; }
}