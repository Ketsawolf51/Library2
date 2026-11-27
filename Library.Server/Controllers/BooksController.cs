using Library.Server.Interfaces;
using Library2.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Server.Controllers;

[ApiController]
[Route("books")]
[Authorize] 
public class BooksController(IMainService main): ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        return await main.GetAllBooks();
    }

    [HttpPost("add")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AppBook([FromQuery] CreateBookDto model)
    {
        return await main.AppBook(model);
    }
}