using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Library.Client.Services;
using Library2.Dto;

namespace Library.Client.ViewModels;

public partial class LoginPageViewModel: ViewModelBase
{
    
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
        
    }
}