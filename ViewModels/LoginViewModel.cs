using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MeterApp.Models;
using MeterApp.Services;

namespace MeterApp.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        [ObservableProperty]
        UserInfo user;

        public ILoginService LoginService { get; set;}




        public LoginViewModel(ILoginService loginService)
        {
            LoginService = loginService;
            User = new() { Username = "", Password = "" };
        }

        [RelayCommand]
        void Login()
        {
            if (LoginService.Login(User.Username, User.Password))
            {
            }
            else
            {
            }
        }
    }
}
