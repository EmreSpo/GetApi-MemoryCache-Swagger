using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using YourNamespace.Services;

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
        public async Task<ActionResult<List<User>>> Get()
        {
            try
            {
                var users = await _userService.GetUsersAsync();
                return Ok(users);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
