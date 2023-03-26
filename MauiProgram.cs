using MeterApp.Pages;
using MeterApp.Services;
using MeterApp.Services.Impl;
using MeterApp.ViewModels;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
#if WINDOWS
using MeterApp.Platforms.Windows;
using MeterApp.Platforms.Windows.Services;
using MeterApp.Platforms.Windows.Services.Impl;
#endif

namespace MeterApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-SemiBold.ttf", "OpenSansSemiBold");
            });


#if DEBUG
        builder.Logging.AddDebug();
#endif

#if WINDOWS
		builder.Services.AddSingleton<SerialPortService>();
		builder.Services.AddSingleton<IProtocolService, Protocol_0X7E_ServiceImpl>();
		builder.Services.AddSingleton<ConfigureService>();
#endif


		builder.Services.AddSingleton<LoginPage>();
		builder.Services.AddSingleton<HomePage>();

        builder.Services.AddSingleton<LoginViewModel>();
		builder.Services.AddSingleton<HomeViewModel>();

		builder.Services.AddSingleton<ILoginService, LoginServiceImpl>();


        return builder.Build();
	}
}
