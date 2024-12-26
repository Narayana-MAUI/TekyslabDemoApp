using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekyslabDemo.Models;

namespace TekyslabDemo.Database
{
    public class DBConnection
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(DBConstants.DatabasePath, DBConstants.Flags);
        });

        private static SQLiteAsyncConnection Database => lazyInitializer.Value;
        private static bool initialized = false;

        

        public DBConnection()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        private async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(SaveImage).Name))
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(SaveImage)).ConfigureAwait(false);

                initialized = true;
            }

            
        }

        public Task<List<SaveImage>> GetImagesData()
        {
            return Database.Table<SaveImage>().ToListAsync();
        }

        

        public Task<int> SetImagesData(SaveImage saveImage)
        {
            if (saveImage.Id == 0)
                return Database.InsertAsync(saveImage);
            else
                return Database.UpdateAsync(saveImage);
        }

        public Task<int> DeleteImageData(SaveImage saveImage)
        {
            return Database.DeleteAsync(saveImage);
        }

       
    }
}
