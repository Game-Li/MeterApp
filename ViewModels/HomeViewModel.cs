using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MeterApp.Models;
using MeterApp.Services;
using System.Collections.ObjectModel;
#if WINDOWS
using MeterApp.Platforms.Windows;
using MeterApp.Platforms.Windows.Services;
#endif


namespace MeterApp.ViewModels;

public partial class HomeViewModel : BaseViewModel
{
    [ObservableProperty]
    HomeOneModel homeOneModel;

    [ObservableProperty]
    int counters = 0;

    public ObservableCollection<MeterDataModel> MeterDatas { get; } = new();
    
#if WINDOWS
    [ObservableProperty]
    SerialPortResult serialPortResult;

    private SerialPortService _serialPortService;
#endif

#if ANDROID
    public HomeViewModel()
    {

    }
#endif

#if WINDOWS
    public HomeViewModel(SerialPortService serialPortService = null)
    {
        if (serialPortService is not null)
        {
            HomeOneModel = new(){Stringtype = ""};
            _serialPortService = serialPortService;
            _serialPortService.ComWeighParser += ReceiveSerialPortResult;
            _serialPortService.initCom();
            SerialPortResult = new(){Weight = 0, Unit = "", Port = ""};

            for (int i = 0; i < 10; i++)
            {
                MeterDatas.Add(new MeterDataModel() { 
                    Property1 = "Property1" + i + 1,
                    Property2 = "Property2" + i + 1,
                    Property3 = "Property3" + i + 1,
                    Property4 = "Property4" + i + 1,
                    Property5 = "Property5" + i + 1,
                    Property6 = "Property6" + i + 1,
                    Property7 = "Property7" + i + 1,
                });
            }
        }
    }
#endif


#if WINDOWS
    public void ReceiveSerialPortResult(ComWeighEventArgs e)
    {
        SerialPortResult.Unit = e.Unit;
        SerialPortResult.Weight = double.Parse(e.Weight);
        SerialPortResult.Port = e.PortName;
    }
#endif

#if WINDOWS
    [RelayCommand]
    void Test()
    {
        Counters++;
        SerialPortResult.Weight = SerialPortResult.Weight + 1;
        HomeOneModel.Stringtype = "hello"+SerialPortResult.Weight.ToString();
    }
#endif
}
