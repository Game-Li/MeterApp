using CommunityToolkit.Mvvm.ComponentModel;

namespace MeterApp.Models;

public partial class SerialPortMessage : ObservableObject
{
    [ObservableProperty]
    public string? unit;
    [ObservableProperty]
    public double? weight;
    [ObservableProperty]
    public string? port;
}
