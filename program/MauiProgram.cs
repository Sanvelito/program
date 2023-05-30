using CommunityToolkit.Maui;
using program.Helpers;
using program.Services;
using program.ViewModels;
using program.ViewModels.User;
using program.Views.User;
using Refit;

namespace program;

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
			})
            .UseMauiCommunityToolkit();

        builder.Services.AddTransient<AuthHeaderHandler>();

        builder.Services
    .AddRefitClient<IApiAuthService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://10.0.2.2:5269"));

        builder.Services
    .AddRefitClient<IApiService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://10.0.2.2:5269"))
    .AddHttpMessageHandler<AuthHeaderHandler>();

        builder.Services.AddSingleton<IConnectivity>((e) => Connectivity.Current);
        

        builder.Services.AddSingleton<LoadingViewModel>();
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<RegistrationViewModel>();
        builder.Services.AddSingleton<UserViewModel>();
        builder.Services.AddSingleton<AdminViewModel>();

        //user catalog page
        builder.Services.AddSingleton<UserCatalogViewModel>();
        builder.Services.AddTransient<UserCatalogPage>();
        //user catalog - detail page
        builder.Services.AddTransient<UserCatalogDetailViewModel>();
        builder.Services.AddTransient<UserCatalogDetailPage>();
        //user catalog - detail page - create order page
        builder.Services.AddTransient<UserCreateOrderViewModel>();
        builder.Services.AddTransient<CreateOrderPage>();
        

        //user acc page
        builder.Services.AddTransient<UserAccountPage>();
        builder.Services.AddSingleton<UserAccountViewModel>();



        var app = builder.Build();
		return app;
	}
}
