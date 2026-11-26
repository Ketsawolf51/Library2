using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Library.Client.Services;
using Library2.Dto;

namespace Library.Client.ViewModels;

public partial class RegisterViewModel: ViewModelBase
{
    private readonly AuthApiService api = new  AuthApiService();
    
    [ObservableProperty]
    private string? _login =  string.Empty;
    [ObservableProperty]
    private string? _password =  string.Empty;
    [ObservableProperty]
    private string? _email =  string.Empty;
    [ObservableProperty]
    private string? _errorMessage =  string.Empty;

    [RelayCommand]
    private async Task RegisterAsync()
    {
        ErrorMessage = null;
        
        if(string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Email))
        {
            ErrorMessage = "Заполните все поля.";
            return;
        }
        var dto = new RegisterDto
        {
            Login = Login,
            Password = Password,
            Email = Email
        };
        
        var response = await api.RegisterAsync(dto);

        if (response.Status)
        {
           NavigationService.Instance.NavigateTo<BooksViewModel>();
        }
        else
        {
            
            ErrorMessage = response.Data;

        }
        
    }
    [RelayCommand]
    private void GoToLogin()
    {
        NavigationService.Instance.NavigateTo<LoginPageViewModel>();
    }
    
}