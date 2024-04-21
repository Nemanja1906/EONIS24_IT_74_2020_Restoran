using Microsoft.AspNetCore.Mvc;
using Restoran.Data;
using Restoran.Data_Transfer_Objects;

namespace Restoran.Controllers
{
   
        [Route("api/[controller]")]
        [ApiController]

        public class AutentifikacijaController : ControllerBase
        {
            private readonly IAutentifikacijaRepository _autentifikacijaRepository;

            public AutentifikacijaController(IAutentifikacijaRepository autentifikacijaRepository)
            {
                _autentifikacijaRepository = autentifikacijaRepository;
            }

            [HttpPost("login")]
            public async Task<ActionResult<object>> Login(LoginDto userLoginDto)
            {
                var result = await _autentifikacijaRepository.Authenticate(userLoginDto.Username, userLoginDto.Password);

                if (result == null)
                    return Unauthorized(new { message = "Invalid username or password" });

                return Ok(result);
            }
        }
    }

