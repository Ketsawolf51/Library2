using System.Security.Claims;
using Library.Server.Interfaces;
using Library2;
using Library2.Data;
using Library2.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Server.Services;

public class AuthService:IAuthService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPasswordHasher _passwordHasher;
    private readonly Dbcontext _db;
    public AuthService(IHttpContextAccessor httpContextAccessor, IPasswordHasher passwordHasher, Dbcontext db)
    {
        _httpContextAccessor = httpContextAccessor;
        _passwordHasher = passwordHasher;
        _db = db;
    }
    
    public async Task<IActionResult> Register(RegisterDto model)
    {
        if (await _db.Users.AnyAsync(u => u.Username == model.Login || u.Email == model.Email))
        {
            return new BadRequestObjectResult(new ApiResponse<string> 
            { 
                Status = false, 
                Data = "Пользователь с таким логином или Email уже существует" 
            });
        }
        
        var user = new User
        {
            Username = model.Login,
            Email = model.Email,
            Password = _passwordHasher.Hash(model.Password)
        };
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        await SignInUserAsync(user);
        
        return new OkObjectResult(new ApiResponse<string> { Status = true, Data = "Регистрация успешна" });
    }
    public async Task<IActionResult> Login(LoginDto model)
    {
        
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == model.Login);
        if (user == null || !_passwordHasher.Verify(model.Password, user.Password))
        {
            return new UnauthorizedObjectResult(new ApiResponse<string> 
            { 
                Status = false, 
                Data = "Неверный логин или пароль" 
            });
        }
        await SignInUserAsync(user);
        
        return new OkObjectResult(new ApiResponse<string> { Status = true, Data = "Вход выполнен успешно." });
    }

    private async Task SignInUserAsync(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role ?? "User") 
        };
        
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        if (_httpContextAccessor.HttpContext != null)
        {
            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                principal);
        }
    }
}