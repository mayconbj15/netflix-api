using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Netflix.API.Services
{
    public interface ICRUDService<T> where T : DbContext
    {
        Task<D> GetObjectById<D>(int id);

        Task<int> Create(object obj);

        Task<int> Update(object obj);

        Task<int> Delete(object obj);
    }
}