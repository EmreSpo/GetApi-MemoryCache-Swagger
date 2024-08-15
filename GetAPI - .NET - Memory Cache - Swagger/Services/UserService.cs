using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace YourNamespace.Services
{
    /// <summary>
    /// Kullanıcı hizmeti, kullanıcı bilgilerini yönetmek için kullanılır.
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
        /// <param name="httpClient">HTTP istemcisi</param>
        /// <param name="memoryCache">Bellek önbelleği</param>
        public UserService(HttpClient httpClient, IMemoryCache memoryCache)
        {
            _httpClient = httpClient;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Kullanıcı listesini asenkron olarak alır.
        /// </summary>
        /// <returns>Kullanıcıların listesi</returns>
        /// <exception cref="HttpRequestException">API isteği başarısız olduğunda oluşur.</exception>
        /// <exception cref="KeyNotFoundException">Kullanıcı bulunamadığında oluşur.</exception>
        public async Task<List<User>> GetUsersAsync()
        {
            // Cache'de veri var mı kontrol et
            if (!_memoryCache.TryGetValue(_cacheKey, out List<User>? users))
            {
                var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException("API request failed.");
                }

                var json = await response.Content.ReadAsStringAsync();
                // JSON deserialization işlemi için güvenli kontrol ekle
                users = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();

                if (users.Count == 0)
                {
                    throw new KeyNotFoundException("No users found.");
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = _cacheExpiration,
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };
                _memoryCache.Set(_cacheKey, users, cacheEntryOptions);

                Console.WriteLine("Cache miss - Veri API'den alındı ve cache'e eklendi.");
            }
            else
            {
                Console.WriteLine("Cache hit - Veri cache'den alındı.");
            }

            // Kullanıcı listesinin null olup olmadığını kontrol et
            if (users == null)
            {
                users = new List<User>();
            }

            return users;
        }
    }

    /// <summary>
    /// Kullanıcı hizmeti arayüzü.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Kullanıcı listesini asenkron olarak alır.
        /// </summary>
        /// <returns>Kullanıcıların listesi</returns>
        Task<List<User>> GetUsersAsync();
    }
}