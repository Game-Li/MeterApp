using CommunityToolkit.Mvvm.ComponentModel;

namespace MeterApp.Models;

public partial class HomeOneModel : ObservableObject
{
    [ObservableProperty]
    public int? inttype;
    [ObservableProperty]
    public bool? booltype;
    [ObservableProperty]
    public string? stringtype;
    [ObservableProperty]
    public DateTime? dateTimetype;
}
