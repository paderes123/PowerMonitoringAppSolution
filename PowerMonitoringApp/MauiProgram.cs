using Microsoft.Extensions.Logging;
using PowerMonitoringApp.Services;
using PowerMonitoringApp.Services.Interfaces;
using PowerMonitoringApp.ViewModels;
using Syncfusion.Maui.Core.Hosting;
using Syncfusion.Maui.Toolkit.Hosting;

namespace PowerMonitoringApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
            .ConfigureSyncfusionToolkit()
            .ConfigureSyncfusionCore()
            .UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
        // Register other Services
        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        builder.Services.AddSingleton<IPowerMeterService, FirebasePowerMeterService>();

        // Register the viewmodels
        builder.Services.AddTransient<PowerMeterViewModel>();
        builder.Services.AddTransient<VoltageAndCurrentMeterViewModel>();

        return builder.Build();
	}
}
