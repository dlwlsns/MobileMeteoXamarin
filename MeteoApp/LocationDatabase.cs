using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MeteoApp.Models;
using SQLite;

namespace MeteoApp
{
    public class LocationDatabase
    {
        private readonly SQLiteAsyncConnection database;

        public LocationDatabase()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "LocationSQLite.db3");
            database = new SQLiteAsyncConnection(dbPath);

            // crea la tabella se non esiste
            database.CreateTableAsync<Location>().Wait();
        }

        /*
         * Ritorna una lista con tutti gli items.
         */
        public Task<List<Location>> GetItemsAsync()
        {
            return database.Table<Location>().ToListAsync();
        }

        /*
         * Query con statement SQL.
         */
        public Task<List<Location>> GetItemsWithWhere(int id)
        {
            return database.QueryAsync<Location>("SELECT * FROM location WHERE id =?", id);
        }

        /*
         * Query con LINQ.
         */
        public Task<Location> GetItemAsync(int id)
        {
            return database.Table<Location>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        /*
         * Salvataggio o update.
         */
        public Task<int> SaveItemAsync(Location item)
        {
            if (item.ID == 0) // esempio
                return database.UpdateAsync(item);

            return database.InsertAsync(item);
        }

        /*
         * Cancellazione di un elemento.
         */
        public Task<int> DeleteItemAsync(Location item)
        {
            return database.DeleteAsync(item);
        }
    }
}