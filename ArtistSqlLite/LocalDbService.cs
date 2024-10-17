using SQLite;

namespace ArtistSqlLite
{
    public class LocalDbService
    {
        private const string DB_NAME = "artistDB.db3";
        private readonly SQLiteAsyncConnection _connection;
        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection.CreateTableAsync<Artist>();
        }

        public async Task<List<Artist>> GetArtists()
        {
            return await _connection.Table<Artist>().ToListAsync();
        }

        public async Task<Artist> GetById(int id)
        {
            return await _connection.Table<Artist>().Where(x=>x.Id== id).FirstOrDefaultAsync();
        }

        public async Task Create(Artist artist)
        {
            await _connection.InsertAsync(artist);
        }

        public async Task Update(Artist artist)
        {
            await _connection.UpdateAsync(artist);
        }

        public async Task Delete(Artist artist)
        {
            await _connection.DeleteAsync(artist);
        }
    }
}
