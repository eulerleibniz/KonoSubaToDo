using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KonoAndroid.Model
{
    public class TodoItemDatabase
    {
        private static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
           {
               return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
           });

        static SQLiteAsyncConnection SqlAsyncConnection => lazyInitializer.Value;
        private static bool initialized = false;

        public TodoItemDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        private async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!SqlAsyncConnection.TableMappings.Any(m => m.MappedType.Name == typeof(TodoItem).Name))
                {
                    await SqlAsyncConnection.CreateTablesAsync(CreateFlags.None, typeof(TodoItem)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<List<TodoItem>> GetItemsAsync()
        {
            return SqlAsyncConnection.Table<TodoItem>().ToListAsync();
        }

        public Task<List<TodoItem>> GetItemsNotDoneAsync()
        {
            return SqlAsyncConnection.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Completed] = 0");
        }

        public Task<TodoItem> GetItemAsync(int id)
        {
            return SqlAsyncConnection.Table<TodoItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(TodoItem item)
        {
            if (item.ID != 0)
            {
                return SqlAsyncConnection.UpdateAsync(item);
            }
            else
            {
                return SqlAsyncConnection.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(TodoItem item)
        {
            return SqlAsyncConnection.DeleteAsync(item);
        }
    }
}