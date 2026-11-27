using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Library.Client.Services;
using Library2.Dto;

namespace Library.Client.ViewModels;

public partial class AddBookViewModel:ViewModelBase
{
    [ObservableProperty] 
    private string _isbn;

    [ObservableProperty] 
    private string _name;

    [ObservableProperty] 
    private string _authorFirstName;
    
    [ObservableProperty] 
    private string _authorLastName;
    
    [ObservableProperty] 
    private int _year ;

    [ObservableProperty] 
    private string _genre;
    
    [ObservableProperty] 
    private int _count ;
    
    [ObservableProperty] 
    private string? _errorMessage;
    
    private readonly BookApiService  _bookApiService = new BookApiService();

    [RelayCommand]
    private async Task SaveBookAsync()
    {
        ErrorMessage = null;

        if (string.IsNullOrWhiteSpace(_name) || string.IsNullOrWhiteSpace(_authorFirstName) ||
            string.IsNullOrWhiteSpace(_authorLastName)||string.IsNullOrWhiteSpace(_isbn))
        {
            ErrorMessage = "напиши номер книги, Имя и Фамилию автора , название не забуть ";
            return;
        }

        var book = new CreateBookDto
        {
            Isbn = Isbn,
            Name = Name,
            AuthorFirstName = AuthorFirstName,
            AuthorLastName = AuthorLastName,
            Year = Year,
            Genre = Genre,
            Count = Count
        };
        
        var res = await _bookApiService.AddBookAsync(book);

        if (res.Status)
        {
            NavigationService.Instance.NavigateTo<BooksViewModel>();
        }
        else
        {
            ErrorMessage= res.Message;
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        NavigationService.Instance.NavigateTo<BooksViewModel>();
    } 
}