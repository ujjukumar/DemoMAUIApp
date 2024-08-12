using DemoMAUIApp.Services;
using Microsoft.Extensions.Logging;

namespace DemoMAUIApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            MauiAppBuilder builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Adding MAUI Services
            builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
            builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
            builder.Services.AddSingleton<IMap>(Map.Default);

            // Adding Services
            builder.Services.AddSingleton<MonkeyService>();

            // Adding ViewModels
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddTransient<MonkeyDetailsViewModel>();

            // Adding Views
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<MonkeyDetailsPage>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
