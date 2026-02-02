using Microsoft.AspNetCore.Mvc;
using TaskBoardAPI.Models;
using TaskBoardAPI.Services;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

   /* IMPLEMENTACIONDE DE REST UTILIZANDO POST */
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
    {
        var token = await _authService.AuthenticateAsync(dto);
        if (token == null)
            return Unauthorized(new { message = "Credenciales inválidas" });

        return Ok(new { token, message = "Credenciales correctas"  });
    }

}
