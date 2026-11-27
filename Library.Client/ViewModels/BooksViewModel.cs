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
    
    
    private List<BookCatalogRowDto> _allBooks = [];

    
    [ObservableProperty]
    private List<BookCatalogRowDto> _books = [];
    
    [ObservableProperty]
    private BookCatalogRowDto? _selectedBook;

    
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

    
    
    [RelayCommand]
    private void OpenAddBook()
    {
        NavigationService.Instance.NavigateTo<AddBookViewModel>();
    }
    
}