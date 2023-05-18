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
			});
        builder.Services
    .AddRefitClient<IApiService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://10.0.2.2:5269"));

        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);

        builder.Services.AddSingleton<LoadingViewModel>();
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<RegistrationViewModel>();
        builder.Services.AddSingleton<UserViewModel>();
        builder.Services.AddSingleton<AdminViewModel>();

        //user
        builder.Services.AddSingleton<UserAccountViewModel>();
        builder.Services.AddSingleton<UserCatalogViewModel>();

        //builder.Services.AddSingleton<UserCatalogDetailViewModel>();


        builder.Services.AddSingleton<UserCatalogPage>();
        var app = builder.Build();
		return app;
	}
}
