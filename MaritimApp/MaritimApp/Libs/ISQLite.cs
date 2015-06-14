using SQLite;

namespace MaritimApp.Libs
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
