using program.ViewModels;
using program.ViewModels.User;
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
		builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);

        builder.Services.AddSingleton<LoadingViewModel>();
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<RegistrationViewModel>();
        builder.Services.AddSingleton<UserViewModel>();
        builder.Services.AddSingleton<AdminViewModel>();

        //user
        builder.Services.AddSingleton<UserAccountViewModel>();

        var app = builder.Build();
		return app;
	}
}
