using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library2.Dto;

namespace Library.Client.Services;

public class BookApiService :BaseApiService
{
    public async Task<List<BookCatalogRowDto>> GetAllBooks()
    {
        try
        {
            return [];
        }
        catch (Exception e)
        {
            Console.WriteLine("Ошибка. Список книг не найден");
            return[];
        }
        
    }
}