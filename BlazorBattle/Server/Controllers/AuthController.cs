using System.Threading.Tasks;
using BlazorBattle.Server.data;
using BlazorBattle.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BlazorBattle.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegister request)
        {
            var response = await _authRepository.Register(new User()
            {
                UserName = request.UserName,
                Email = request.Email,
                Bananas = request.Bananas,
                DateOfBirth = request.DateOfBirth,
                IsConfirmed = request.IsConfirmed
            }, request.Password,
                int.Parse(request.StartUnitId));

            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLogin request)
        {
            var response = await _authRepository.Login(request.Email, request.Password);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }
    }
}