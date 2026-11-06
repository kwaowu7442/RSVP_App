using SQLite;
namespace RSVP_App;

public class AppDatabase
{
    private SQLiteAsyncConnection? _db;

    private async Task Init()
    {
        if (_db != null) return;
        var path = Path.Combine(FileSystem.AppDataDirectory, "rsvp.db3");
        _db = new SQLiteAsyncConnection(path);
        await _db.CreateTableAsync<User>();
        await _db.CreateTableAsync<Event>();
        await _db.CreateTableAsync<Rsvp>();
    }

    // Users
    public async Task<bool> AddUserAsync(User u)
    {
        await Init();
        var exists = await _db!.Table<User>().Where(x => x.Email == u.Email).FirstOrDefaultAsync();
        if (exists != null) return false;
        await _db.InsertAsync(u);
        return true;
    }

    public async Task<User?> LoginAsync(string email, string password)
    {
        await Init();
        return await _db!.Table<User>()
            .Where(x => x.Email == email && x.Password == password)
            .FirstOrDefaultAsync();
    }

    // Events
    public async Task AddEventAsync(Event e)
    {
        await Init();
        await _db!.InsertAsync(e);
    }

    public async Task<List<Event>> GetAllEventsAsync()
    {
        await Init();
        return await _db!.Table<Event>().OrderBy(e => e.EventDate).ToListAsync();
    }

    public async Task<List<Event>> GetHostedEventsAsync(int userId)
    {
        await Init();
        return await _db!.Table<Event>().Where(e => e.HostUserId == userId).ToListAsync();
    }

    public async Task<List<Event>> GetAttendingEventsAsync(int userId)
    {
        await Init();
        var rsvps = await _db!.Table<Rsvp>().Where(r => r.UserId == userId).ToListAsync();
        var ids = rsvps.Select(r => r.EventId).ToList();
        var all = await _db.Table<Event>().ToListAsync();
        return all.Where(e => ids.Contains(e.EventId)).ToList();
    }

    public async Task<Event?> GetEventAsync(int id)
    {
        await Init();
        return await _db!.Table<Event>().Where(e => e.EventId == id).FirstOrDefaultAsync();
    }

    // RSVPs
    public async Task AddRsvpAsync(Rsvp r)
    {
        await Init();
        await _db!.InsertAsync(r);
    }
}