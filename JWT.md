# JWT Authentication Implementation Steps

## 1. Install NuGet Packages

```bash
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package System.IdentityModel.Tokens.Jwt
```

---

## 2. Configure JWT Settings in `appsettings.json`

```json
"Jwt": {
  "Key": "your-super-secret-key-at-least-32-characters",
  "Issuer": "your-app-issuer",
  "Audience": "your-app-audience",
  "ExpiresInMinutes": 60
}
```

---

## 3. Create `User` Model

```csharp
public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
}
```

---

## 4. Create Login Request DTO

```csharp
public class LoginRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
```

---

## 5. Create `TokenService`

```csharp
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class TokenService
{
    private readonly IConfiguration _config;

    public TokenService(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateToken(string username)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(_config["Jwt:ExpiresInMinutes"]!)),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
```

---

## 6. Create `AuthController`

```csharp
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
```

---

## 7. Register Services and Configure JWT Middleware in `Program.cs`

```csharp
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

// Register TokenService
builder.Services.AddScoped<TokenService>();

// Configure JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        };
    });

// Add after builder.Build() calls:
app.UseAuthentication();
app.UseAuthorization();
```

---

## 8. Protect Endpoints with `[Authorize]`

```csharp
using Microsoft.AspNetCore.Authorization;

[Authorize]
[HttpGet(Name = "GetToDo")]
public IEnumerable<ToDo> Get()
{
    return _dbContext.ToDos.ToList();
}
```

---

## 9. Test the Flow

1. **POST** `/Auth/login` with `{ "username": "admin", "password": "password" }`
2. Copy the returned `token`
3. Add header `Authorization: Bearer <token>` to protected requests

---

## 10. (Optional) Expose JWT Config in Swagger

```csharp
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});
```

---

## Summary Checklist

- [ ] Install `Microsoft.AspNetCore.Authentication.JwtBearer`
- [ ] Add JWT config to `appsettings.json`
- [ ] Create `TokenService` to generate tokens
- [ ] Register `TokenService` in DI: `builder.Services.AddScoped<TokenService>()`
- [ ] Create `AuthController` with `/login` endpoint
- [ ] Register authentication + JWT bearer in `Program.cs`
- [ ] Add `app.UseAuthentication()` before `app.UseAuthorization()`
- [ ] Decorate protected controllers/actions with `[Authorize]`
- [ ] (Optional) Configure Swagger to accept Bearer tokens
