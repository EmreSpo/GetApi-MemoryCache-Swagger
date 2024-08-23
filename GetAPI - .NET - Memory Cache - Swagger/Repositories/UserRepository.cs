using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YourNamespace.Data; // `AppDbContext`'i tanımak için gerekli
using YourNamespace.Models; // `User` sınıfını tanımak için gerekli

namespace YourNamespace.Repositories
{
    /// <summary>
    /// Kullanıcılarla ilgili veritabanı işlemlerini gerçekleştiren repository sınıfı.
    /// </summary>
    public class UserRepository : IRepository<User>
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// UserRepository sınıfının bir örneğini oluşturur.
        /// </summary>
        /// <param name="context">Veritabanı bağlamı</param>
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Tüm kullanıcıları asenkron olarak getirir.
        /// </summary>
        /// <returns>Kullanıcıların listesi</returns>
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        /// <summary>
        /// Belirli bir ID'ye sahip kullanıcıyı asenkron olarak getirir.
        /// </summary>
        /// <param name="id">Kullanıcı ID'si</param>
        /// <returns>Bulunan kullanıcı veya null</returns>
        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        /// <summary>
        /// Yeni bir kullanıcıyı asenkron olarak ekler.
        /// </summary>
        /// <param name="entity">Eklenecek kullanıcı</param>
        public async Task AddAsync(User entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Bir kullanıcıyı asenkron olarak günceller.
        /// </summary>
        /// <param name="entity">Güncellenecek kullanıcı</param>
        public async Task UpdateAsync(User entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Belirli bir ID'ye sahip kullanıcıyı asenkron olarak siler.
        /// </summary>
        /// <param name="id">Silinecek kullanıcı ID'si</param>
        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                throw new ArgumentException("User not found", nameof(id));

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}




