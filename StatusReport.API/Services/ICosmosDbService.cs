using System.Collections.Generic;
namespace StatusReport.API.Services
{
    public interface ICosmosDbService<T>
    {
        Task<T> GetAsync(string id);
        Task<IEnumerable<T>> GetMultipleAsync(string query);
        Task AddAsync(T item);
        Task UpdateAsync(string id, T item);
        Task DeleteAsync(string id);
    }
}
