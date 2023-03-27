using CommunityToolkit.Mvvm.ComponentModel;

namespace MeterApp.Platforms.Windows.Models;

public partial class SerialPortResult
{
    private string? unit;
    private double? weight;
    private string? port;

    public string? Unit { get => unit; set => unit = value; }
    public double? Weight { get => weight; set => weight = value; }
    public string? Port { get => port; set => port = value; }
}

/*public partial class SerialPortResult : ObservableObject
{
    [ObservableProperty]
    public string? unit;
    [ObservableProperty]
    public double? weight;
    [ObservableProperty]
    public string? port;
}*/

