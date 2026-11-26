using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Library.Client.Services;
using Library2.Dto;

namespace Library.Client.ViewModels;

public partial class BooksViewModel : ViewModelBase
{
    private readonly BookApiService _bookApiService = new();
    
    // Полный кэш книг с сервера, чтобы не терять данные при фильтрации
    private List<BookCatalogRowDto> _allBooks = [];

    // Отфильтрованный список, который отображается в DataGrid
    [ObservableProperty]
    private List<BookCatalogRowDto> _books = [];
    
    [ObservableProperty]
    private BookCatalogRowDto? _selectedBook;

    // Свойства поиска и фильтров (обязательно [ObservableProperty]!)
    [ObservableProperty] 
    private string _searchTb = string.Empty;

    [ObservableProperty]
    private List<string> _authorFilter = ["Все авторы"];

    [ObservableProperty]
    private string _selectedAuthor = "Все авторы";

    [ObservableProperty]
    private List<string> _genreFilter = ["Все жанры"];

    [ObservableProperty]
    private string _selectedGenre = "Все жанры";

    public bool IsAdmin => UserSession.IsAdmin;

    

    
}