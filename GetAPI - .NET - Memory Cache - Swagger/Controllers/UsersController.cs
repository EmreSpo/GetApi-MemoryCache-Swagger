using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using YourNamespace.Services;
using YourNamespace.Models;

namespace YourNamespace.Controllers
{
    /// <summary>
    /// API'yi kullanıcı bilgileri ile yönetir.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// UsersController sınıfının bir örneğini oluşturur.
        /// </summary>
        /// <param name="userService">Kullanıcı hizmeti</param>
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Kullanıcı listesini alır.
        /// </summary>
        /// <returns>Kullanıcıların listesi</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
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
        /// Belirli bir kullanıcıyı ID'ye göre getirir.
        /// </summary>
        /// <param name="id">Kullanıcı ID'si</param>
        /// <returns>Belirli bir kullanıcı</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred: " + ex.Message);
            }
        }

        /// <summary>
        /// Yeni bir kullanıcı oluşturur.
        /// </summary>
        /// <param name="user">Oluşturulacak kullanıcı bilgileri</param>
        /// <returns>Oluşturulan kullanıcı</returns>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User is null.");
            }

            try
            {
                await _userService.AddUserAsync(user);
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred: " + ex.Message);
            }
        }

        /// <summary>
        /// Belirli bir kullanıcıyı günceller.
        /// </summary>
        /// <param name="id">Güncellenecek kullanıcı ID'si</param>
        /// <param name="user">Güncellenmiş kullanıcı bilgileri</param>
        /// <returns>Güncellenen kullanıcı</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (id != user.Id)
            {
                return BadRequest("User ID mismatch.");
            }

            try
            {
                await _userService.UpdateUserAsync(user);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data: " + ex.Message);
            }
        }

        /// <summary>
        /// Belirli bir kullanıcıyı siler.
        /// </summary>
        /// <param name="id">Silinecek kullanıcı ID'si</param>
        /// <returns>Silme işlemi sonucu</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data: " + ex.Message);
            }
        }
    }
}
