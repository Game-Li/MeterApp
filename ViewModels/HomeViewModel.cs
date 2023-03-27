using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MeterApp.Models;
using MeterApp.Services;
using System.Collections.ObjectModel;
#if WINDOWS
using MeterApp.Platforms.Windows;
using MeterApp.Platforms.Windows.Models;
using MeterApp.Platforms.Windows.Services;
#endif


namespace MeterApp.ViewModels;

public partial class HomeViewModel : BaseViewModel
{
    [ObservableProperty]
    HomeOneModel homeOne;

    [ObservableProperty]
    int counters = 0;

    int wht = 0;

    [ObservableProperty]
    SerialPortMessage message;

    public ObservableCollection<MeterDataModel> MeterDatas { get; } = new();

#if WINDOWS
/*    [ObservableProperty]
    SerialPortResult serialPortResult;*/

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
            HomeOne = new(){Stringtype = ""};
            _serialPortService = serialPortService;
            _serialPortService.ComWeighParser += ReceiveSerialPortResult;
            _serialPortService.initCom();
            Message = new SerialPortMessage(){Weight = 0, Unit = "", Port = ""};

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
        /*        SerialPortMessage.Unit = e.Unit;
                SerialPortMessage.Weight = double.Parse(e.Weight);
                SerialPortMessage.Port = e.PortName;*/

        wht = wht + 1;

        Message.Unit = "kg";
        Message.Weight = double.Parse(e.Weight);
        Message.Port = "";
    }
#endif

#if WINDOWS
    [RelayCommand]
    void Test()
    {
        Counters++;
        Message.Weight = Message.Weight + 1;
        HomeOne.Stringtype = "hello"+ Message.Weight.ToString();
    }
#endif
}
