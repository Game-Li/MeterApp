using CommunityToolkit.Mvvm.ComponentModel;

namespace MeterApp.Models;

public partial class UserInfo : ObservableObject
{
    [ObservableProperty]
    public string? username;
    [ObservableProperty]
    public string? password;
}
