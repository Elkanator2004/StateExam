using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IDb<T, K>
    {
        Task CreateAsync(T item);

        Task<T> ReadAsync(K key, bool useNavigationalProperties = false);

        Task<IEnumerable<T>> ReadAllAsync(bool useNavigationalProperties = false);

        Task UpdateAsync(T item, bool useNavigationalProperties = false);

        Task DeleteAsync(K key);
    }
}
