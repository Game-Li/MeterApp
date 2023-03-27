using CommunityToolkit.Mvvm.ComponentModel;

namespace MeterApp.Platforms.Windows.Models;

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

