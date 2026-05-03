using Microsoft.AspNetCore.Mvc;
using NeedToDo.APIs.Models;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;

    public AuthController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        // Replace with real user lookup + password hash verification
        if (request.Username == "admin" && request.Password == "password")
        {
            var token = _tokenService.GenerateToken(request.Username);
            return Ok(new { token });
        }

        return Unauthorized();
    }
}