using DemoMAUIApp.Services;
using Microsoft.Extensions.Logging;

namespace DemoMAUIApp
{
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

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
