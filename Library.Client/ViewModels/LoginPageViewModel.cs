using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Library.Client.Services;
using Library2.Dto;

namespace Library.Client.ViewModels;

public partial class LoginPageViewModel: ViewModelBase
{
    private readonly AuthApiService api = new AuthApiService();
    [ObservableProperty]
    private string login;
    [ObservableProperty]
    private string password;
    
    [ObservableProperty] private string? _errorMessage;
    
    [RelayCommand]
    private async Task LoginAsync()
    {
        
        ErrorMessage = string.Empty;
        
        if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
        {
            return;
        }

        var dto = new LoginDto
        {
            Login = Login,
            Password = Password
        };
        var response = await api.LoginAsync(dto);

        if (response.Status)
        {
            UserSession.Role = response.Data ?? "User";
            NavigationService.Instance.NavigateTo<BooksViewModel>();

        }
        else
        {
            ErrorMessage = "Неправильный пароль.";
        }
    }

    [RelayCommand]
    private async Task GoToRegister()
    {
        NavigationService.Instance.NavigateTo<RegisterViewModel>();
    }
   
    
        
    
}