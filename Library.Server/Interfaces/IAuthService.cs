using Library2.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Library.Server.Interfaces;

public interface IAuthService
{
    public Task<IActionResult> Register(RegisterDto model);
    
    public  Task<IActionResult> Login(LoginDto model);
}