using Library.Server.Interfaces;
using Library2;
using Library2.Data;
using Library2.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;

namespace Library.Server.Services;

public class MainService:IMainService
{
    private readonly Dbcontext  _context;
    public MainService(Dbcontext context)
    {
        _context = context;
    }

    public async Task<IActionResult> GetAllBooks()
    {
        List<BookCatalogRowDto> books = await _context.Books.Select(x => new BookCatalogRowDto
        {
            Isbn = x.ISBN,
            Title = x.Name,
            Author = x.Author.FirstName + " " + x.Author.LastName,
            Year = x.Year_Publication,
            Genre = x.Genre,
            Count = x.Count,
            Rating = x.Feedbacks.Any()
                ? Math.Round(x.Feedbacks.Average(f => f.Rating), 1)
                : 0


        }).ToListAsync();
        
        return new OkObjectResult(new ApiResponse<List<BookCatalogRowDto>>
        {
            Status = true,
            Message = "Books retrieved successfully",
            Data = books
        });
    }

    public async Task<IActionResult> AppBook(CreateBookDto model)
    {
        if (await _context.Books.AnyAsync(x => x.ISBN == model.Isbn))
        {
            return new BadRequestObjectResult(new ApiResponse<string>
            {
                Status = false,
                Message = "Книга с таким ISBN уже существует"
            });
        }

        var author = await _context.Authors.FirstOrDefaultAsync(x =>
            x.FirstName == model.AuthorFirstName && x.LastName == model.AuthorLastName);
        if (author==null)
        {
            author = new Author
            {
                FirstName = model.AuthorFirstName,
                LastName = model.AuthorLastName
            };
            _context.Authors.Add(author);
        }

        var book = new Book
        {
            ISBN = model.Isbn,
            Name = model.Name,
            Author = author,
            Year_Publication = model.Year,
            Genre = model.Genre,
            Count = model.Count,
        };
        
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        
        return new OkObjectResult(new ApiResponse<string>
        {
            Status = true,
            Message = "Книга успешно добавлена"
        });
    }
}