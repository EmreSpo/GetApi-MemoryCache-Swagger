using System.Collections.Generic;
using System.Threading.Tasks;

namespace YourNamespace.Repositories
{
    /// <summary>
    /// Generic bir repository arayüzü. 
    /// </summary>
    /// <typeparam name="T">İşlem yapılacak entity tipi</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Tüm entity'leri asenkron olarak getirir.
        /// </summary>
        /// <returns>Entity'lerin listesi</returns>
        Task<IEnumerable<T>> GetAllAsync();   

        /// <summary>
        /// Belirli bir ID'ye sahip entity'yi asenkron olarak getirir.
        /// </summary>
        /// <param name="id">Entity'nin ID'si</param>
        /// <returns>Bulunan entity veya null</returns>
        Task<T?> GetByIdAsync(int id);        

        /// <summary>
        /// Yeni bir entity'yi asenkron olarak ekler.
        /// </summary>
        /// <param name="entity">Eklenecek entity</param>
        Task AddAsync(T entity);              

        /// <summary>
        /// Bir entity'yi asenkron olarak günceller.
        /// </summary>
        /// <param name="entity">Güncellenecek entity</param>
        Task UpdateAsync(T entity);           

        /// <summary>
        /// Belirli bir ID'ye sahip entity'yi asenkron olarak siler.
        /// </summary>
        /// <param name="id">Silinecek entity'nin ID'si</param>
        Task DeleteAsync(int id);             
    }
}
