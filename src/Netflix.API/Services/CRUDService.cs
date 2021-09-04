using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Netflix.API.Repositories;

namespace Netflix.API.Services
{
    public class CRUDService<T> where T : DbContext
    {
        private readonly T _context;

        public CRUDService(T context)
        {
            _context = context;
        }

        public async Task<D> GetObjectById<D>(int id)
        {

            return (D)await _context.FindAsync(typeof(D), id);
        }

        public async Task<int> Create(object obj)
        {
            await _context.AddAsync(obj);
            return await SaveChangesAsync();
        }

        public async Task<int> Update(object obj)
        {
            _context.Update(obj);
            return await SaveChangesAsync();
        }

        public async Task<int> Delete(object obj)
        {
            _context.Remove(obj);
            return await SaveChangesAsync();
        }

        private async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}