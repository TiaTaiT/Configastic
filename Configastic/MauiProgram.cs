using Configastic.Components.Features.GoogleProjects.Store;
using Configastic.Services.Interfaces;
using Configastic.Services.Services;
using Fluxor;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace Configastic
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
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		//builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddFluxor(o => o
            .ScanAssemblies(typeof(MauiProgram).Assembly));

            builder.Services.AddScoped<IDeviceScanningService,DeviceScanningService>();
            builder.Services.AddScoped<IDeviceSearcher, BolidDeviceSearcher>();
            builder.Services.AddSingleton<ProjectHeaderState>();

            builder.Services.AddMudServices();
            builder.Services.AddMudBlazorDialog();

            builder.Logging.AddDebug();

            return builder.Build();
        }
    }
}
