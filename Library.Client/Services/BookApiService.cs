using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Library2;
using Library2.Dto;

namespace Library.Client.Services;

public class BookApiService :BaseApiService
{
    public async Task<List<BookCatalogRowDto>> GetAllBooks()
    {
        try
        {
            var response = await Http.GetAsync($"books");
            if (!response.IsSuccessStatusCode)
            {
                return [];
            }
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<List<BookCatalogRowDto>>>();
            return result.Data;
        }
        catch (Exception e)
        {
            Console.WriteLine("Ошибка. Список не быль получин, ошибка сервера ");
            return[];
        }
        
    }
    public async Task<ApiResponse<string>> AddBookAsync(CreateBookDto model)
    {
        try
        {
            var response = await Http.PostAsJsonAsync("books/add", model);

            if (response.StatusCode == HttpStatusCode.Forbidden)
                return new ApiResponse<string> { Status = false, Message = "Недостаточно прав (требуется роль Admin)" };

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                return new ApiResponse<string> { Status = false, Message = "Сессия истекла, войдите заново" };

            // Читаем ответ как строку, чтобы не упасть, если сервер вернул не JSON
            var rawText = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponse<string>
                {
                    Status = false,
                    Message = $"Ошибка {(int)response.StatusCode}: {rawText}"
                };
            }

            var result = System.Text.Json.JsonSerializer.Deserialize<ApiResponse<string>>(
                rawText,
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result ?? new ApiResponse<string> { Status = false, Message = "Пустой ответ сервера" };
        }
        catch (Exception ex)
        {
            return new ApiResponse<string>
            {
                Status = false,
                Message = $"Ошибка подключения: {ex.Message}"
            };
        }
    }
}