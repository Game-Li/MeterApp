using System.Configuration;
using Windows.Services.Maps;
using System.Collections.Specialized;
//using Microsoft.Extensions.Configuration;

namespace MeterApp.Platforms.Windows.Services;


public class ConfigureService : System.Configuration.Provider.ProviderBase
{
    public SerialPortSetting SerialPortSetting { get; set; }


    public ConfigureService()
    {
        SerialPortSetting = new SerialPortSetting();
        SerialPortSetting.Baudrate = int.Parse(ConfigurationManager.AppSettings.Get("BaudRate") ?? "0");
        SerialPortSetting.PortName = ConfigurationManager.AppSettings.Get("Port") ?? "COM0";
        SerialPortSetting.DataBits = double.Parse(ConfigurationManager.AppSettings.Get("DataBits") ?? "0");
        SerialPortSetting.Parity = double.Parse(ConfigurationManager.AppSettings.Get("Parity") ?? "0");
        SerialPortSetting.StopBits = double.Parse(ConfigurationManager.AppSettings.Get("StopBits") ?? "0");
    }
}
