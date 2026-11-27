using Library2.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Library.Server.Interfaces;

public interface IMainService
{
    public Task<IActionResult> GetAllBooks();
    
    public Task<IActionResult> AppBook(CreateBookDto model);
}