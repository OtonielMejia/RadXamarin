using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using RADXamarin.Models;
using System.Threading.Tasks;

namespace RADXamarin.Data
{
    public class SQLiteHelper
    {
        SQLiteAsyncConnection db;
        public SQLiteHelper(string dbPath) { 
            
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Contactos>().Wait();
        
        }

        public Task <int> SaveContactoAsync(Contactos contacto) { 
        
            if (contacto.IdContacto != 0)
            {
                return db.UpdateAsync(contacto);
            }
            else
            {
                return db.InsertAsync(contacto);
            }
        }

        public Task<List<Contactos>> GetContactoAsync()
        {
            return db.Table<Contactos>().ToListAsync();
        }

        public Task<Contactos> GetContactoByIdAsync(int id)
        {
            return db.Table<Contactos>().Where(a => a.IdContacto == id).FirstOrDefaultAsync();
        }

        public Task<int> DeleteContactoAsync(Contactos contacto)
        {
            return db.DeleteAsync(contacto);
        }
    }
}
