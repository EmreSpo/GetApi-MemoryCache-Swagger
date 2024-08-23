using System.Collections.Generic;
using System.Threading.Tasks;
using YourNamespace.Models; // User sınıfını burada tanımlayın

namespace YourNamespace.Services
{
    /// <summary>
    /// Kullanıcı hizmeti arayüzü.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Kullanıcı listesini asenkron olarak alır.
        /// </summary>
        /// <returns>Kullanıcıların listesi</returns>
        Task<IEnumerable<User>> GetUsersAsync();

        /// <summary>
        /// ID'ye göre kullanıcıyı asenkron olarak alır.
        /// </summary>
        /// <param name="id">Kullanıcı ID'si</param>
        /// <returns>Bulunan kullanıcı</returns>
        Task<User> GetUserByIdAsync(int id);

        /// <summary>
        /// Yeni bir kullanıcıyı asenkron olarak oluşturur.
        /// </summary>
        /// <param name="user">Oluşturulacak kullanıcı</param>
        Task AddUserAsync(User user);

        /// <summary>
        /// Var olan bir kullanıcıyı asenkron olarak günceller.
        /// </summary>
        /// <param name="user">Güncellenecek kullanıcı</param>
        Task UpdateUserAsync(User user);

        /// <summary>
        /// ID'ye göre bir kullanıcıyı asenkron olarak siler.
        /// </summary>
        /// <param name="id">Silinecek kullanıcı ID'si</param>
        Task DeleteUserAsync(int id);
    }
}

