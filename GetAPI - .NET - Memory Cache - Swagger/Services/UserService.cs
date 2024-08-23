using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using YourNamespace.Models; // User sınıfını burada tanımlayın

namespace YourNamespace.Services
{
    /// <summary>
    /// Kullanıcı bilgilerini yönetmek için kullanılan servis sınıfı.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _memoryCache;
        private readonly string _cacheKey = "usersCache";
        private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(5);

        /// <summary>
        /// UserService sınıfının bir örneğini oluşturur.
        /// </summary>
        /// <param name="httpClient">HttpClient nesnesi</param>
        /// <param name="memoryCache">Bellek önbelleği</param>
        public UserService(HttpClient httpClient, IMemoryCache memoryCache)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        /// <summary>
        /// Kullanıcı listesini asenkron olarak alır.
        /// </summary>
        /// <returns>Kullanıcıların listesi</returns>
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            // Cache'de kullanıcıları arıyoruz
            if (!_memoryCache.TryGetValue(_cacheKey, out IEnumerable<User>? users))
            {
                // Cache'de yoksa, JSONPlaceholder'dan alıyoruz
                var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    users = JsonSerializer.Deserialize<IEnumerable<User>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                else
                {
                    throw new Exception("Failed to retrieve data from JSONPlaceholder.");
                }

                // Null kontrolü yapıyoruz
                if (users == null)
                {
                    users = Enumerable.Empty<User>(); // Boş bir liste döndürüyoruz
                    Console.WriteLine("No users found in JSONPlaceholder.");
                }
                else if (!users.Any())
                {
                    Console.WriteLine("No users found in JSONPlaceholder.");
                }

                // Cache'e ekliyoruz
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = _cacheExpiration,
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };
                _memoryCache.Set(_cacheKey, users, cacheEntryOptions);

                Console.WriteLine("Cache miss - Veri JSONPlaceholder'dan alındı ve cache'e eklendi.");
            }
            else
            {
                Console.WriteLine("Cache hit - Veri cache'den alındı.");
            }

            return users ?? Enumerable.Empty<User>(); // Null ise boş liste döndür
        }

        /// <summary>
        /// Belirli bir kullanıcıyı ID'ye göre alır.
        /// </summary>
        /// <param name="id">Kullanıcı ID'si</param>
        /// <returns>Bulunan kullanıcı</returns>
        public async Task<User> GetUserByIdAsync(int id)
        {
            var users = await GetUsersAsync();
            var user = users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with id {id} not found.");
            }

            return user;
        }

        /// <summary>
        /// Yeni bir kullanıcı oluşturur.
        /// </summary>
        /// <param name="user">Oluşturulacak kullanıcı</param>
        public Task AddUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            // JSONPlaceholder API üzerinde veri ekleme işlemi yapamayacağınız için bu metodda boş bir işlem yapıyoruz.
            // Gerçek projelerde burada API'ye istek yaparak kullanıcı ekleme işlemi gerçekleştirilir.

            _memoryCache.Remove(_cacheKey); // Cache'i temizliyoruz

            return Task.CompletedTask; // Boş bir Task döndür
        }

        /// <summary>
        /// Bir kullanıcıyı günceller.
        /// </summary>
        /// <param name="user">Güncellenecek kullanıcı</param>
        public Task UpdateUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            // JSONPlaceholder API üzerinde veri güncelleme işlemi yapamayacağınız için bu metodda boş bir işlem yapıyoruz.
            // Gerçek projelerde burada API'ye istek yaparak kullanıcı güncelleme işlemi gerçekleştirilir.

            _memoryCache.Remove(_cacheKey); // Cache'i temizliyoruz

            return Task.CompletedTask; // Boş bir Task döndür
        }

        /// <summary>
        /// Bir kullanıcıyı siler.
        /// </summary>
        /// <param name="id">Silinecek kullanıcı ID'si</param>
        public Task DeleteUserAsync(int id)
        {
            // Senkron GetUsersAsync çağrısının yerine async bir yöntem kullanmak daha iyi olabilir
            var users = GetUsersAsync().Result;
            var user = users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with id {id} not found.");
            }

            // JSONPlaceholder API üzerinde veri silme işlemi yapamayacağınız için bu metodda boş bir işlem yapıyoruz.
            // Gerçek projelerde burada API'ye istek yaparak kullanıcı silme işlemi gerçekleştirilir.

            _memoryCache.Remove(_cacheKey); // Cache'i temizliyoruz

            return Task.CompletedTask; // Boş bir Task döndür
        }
    }
}




