using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterApp.Platforms.Windows;

public class SerialPortSetting
{
    private string portName;
    private int baudrate;
    private double dataBits;
    private double parity;
    private double stopBits;

    public string PortName { get => portName; set => portName = value; }
    public int Baudrate { get => baudrate; set => baudrate = value; }
    public double DataBits { get => dataBits; set => dataBits = value; }
    public double Parity { get => parity; set => parity = value; }
    public double StopBits { get => stopBits; set => stopBits = value; }
}

public partial class SerialPortResult : ObservableObject
{
    [ObservableProperty]
    public string? unit;
    [ObservableProperty]
    public double? weight;
    [ObservableProperty]
    public string? port;
}
