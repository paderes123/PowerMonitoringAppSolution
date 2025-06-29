﻿using CommunityToolkit.Maui;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Microsoft.Extensions.Logging;
using PowerMonitoringApp.Services;
using PowerMonitoringApp.Services.Interfaces;
using PowerMonitoringApp.ViewModels;
using PowerMonitoringApp.Views;
using Syncfusion.Maui.Core.Hosting;
using Syncfusion.Maui.Toolkit.Hosting;

namespace PowerMonitoringApp;

public static class MauiProgram
{
    private static IServiceProvider? _services;

    public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
            .UseMauiCommunityToolkit()
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
        builder.Services.AddSingleton<AppShell>();

        var firebaseAuthConfig = new FirebaseAuthConfig
        {
            ApiKey = "AIzaSyAgBmw4yMR8y7cu8WJgXSSa5j4UP1Q_Xr8",
            AuthDomain = "scenic-scholar-380010.firebaseapp.com",
            Providers = new FirebaseAuthProvider[]
                {
                    // Add and configure individual providers
                    new GoogleProvider().AddScopes("paderesbryan08@gmail.com"),
                    new EmailProvider()
                    // ...
                }
        };
        var firebaseAuthClient = new FirebaseAuthClient(firebaseAuthConfig);


        // Register other Services  
        builder.Services.AddSingleton<FirebaseAuthClient>(firebaseAuthClient);
        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        builder.Services.AddSingleton<IPowerMeterService, FirebasePowerMeterService>();
        builder.Services.AddTransient<IAuthService, FirebaseAuthService>();

        // Register the viewmodels
        builder.Services.AddTransient<PowerMeterViewModel>();
        builder.Services.AddSingleton<MonthlyPowerUsageViewModel>();
        builder.Services.AddTransient<TodayPowerUsageViewModel>();
        builder.Services.AddTransient<VoltageAndCurrentMeterViewModel>();
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<SignUpViewModel>();
        builder.Services.AddTransient<ShellViewModel>();

        //Register the pages
        //builder.Services.AddTransient<LoginPage>();
        //builder.Services.AddTransient<FrequencyMeterPage>();
        //builder.Services.AddTransient<PowerMeterPage>();
        //builder.Services.AddSingleton<SignUpPage>();

        var app = builder.Build();
        _services = app.Services;

        return app;
	}

    // Expose the service provider so we can resolve dependencies elsewhere
    public static IServiceProvider Services => _services ?? throw new InvalidOperationException("Services not initialized.");
}
