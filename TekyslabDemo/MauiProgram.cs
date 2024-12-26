using CommunityToolkit.Maui;
using TekyslabDemo.ViewModels;
using TekyslabDemo.Views;
using TekyslabDemo.Database;
using Maui.BottomSheet;
using Microsoft.Extensions.Logging;
using Refit;
using TekyslabDemo.Services.IService;
using TekyslabDemo.Models;

namespace TekyslabDemo
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
               .UseMauiBottomSheet()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddTransient<ItemDetailsPage>();
            builder.Services.AddTransient<ItemDetailsViewmodel>();
            builder.Services.AddTransient<ItemPicturePage>();
            builder.Services.AddTransient<ItemPictureViewModel>();
            builder.Services.AddTransient<DBConnection>();

            ConfigureRefit(builder.Services);
            return builder.Build();
           
        }

        static void ConfigureRefit(IServiceCollection services)
        {
            services.AddRefitClient<IImageDataService>()
                .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://reqres.in"));


            /* services.AddRefitClient<IImageDataService>(sp =>
             {
                 return new RefitSettings()
                 {
                     AuthorizationHeaderValueGetter = (_, __) => Task.FromResult(Preferences.Default.Get("Bearer_token", string.Empty))
                 };
             })
                 .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri("http://20.68.50.194:1003"));*/
        }
    }
}
