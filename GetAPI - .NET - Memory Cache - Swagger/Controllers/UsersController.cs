using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using YourNamespace.Services;
using YourNamespace.Data; // AppDbContext sınıfının bulunduğu ad alanı
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace YourNamespace.Controllers
{
    /// <summary>
    /// API'yi kullanıcı bilgileri ile yönet
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AppDbContext _context;

        /// <summary>
        /// UsersController sınıfının bir örneğini oluştur
        /// </summary>
        /// <param name="userService">Kullanıcı hizmeti</param>
        /// <param name="context">Veritabanı bağlamı</param>
        public UsersController(IUserService userService, AppDbContext context)
        {
            _userService = userService;
            _context = context;
        }

        /// <summary>
        /// Kullanıcı listesini al
        /// </summary>
        /// <returns>Kullanıcıların listesi</returns>
        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            try
            {
                var users = await _userService.GetUsersAsync();
                return Ok(users);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred: " + ex.Message);
            }
        }

        /// <summary>
        /// Yeni bir kullanıcı oluşturur ve veritabanına kaydet.
        /// </summary>
        /// <param name="user">Oluşturulacak kullanıcı bilgileri</param>
        /// <returns>Oluşturulan kullanıcı</returns>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User is null");
            }

            try
            {
                // User nesnesinin Id property'sinin sıfır olup olmadığını kontrol et
                if (user.Id != 0)
                {
                    return BadRequest("ID should not be provided. It is auto-generated.");
                }

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                // CreatedAtAction metodunu güncelle, user.Id kullanarak doğru endpointi belirt
                return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database update failed: " + ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred: " + ex.Message);
            }
        }
    }
}
