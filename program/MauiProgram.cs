﻿using CommunityToolkit.Maui;
using program.Helpers;
using program.Services;
using program.ViewModels;
using program.ViewModels.Admin;
using program.ViewModels.Manager;
using program.ViewModels.User;
using program.Views;
using program.Views.Admin;
using program.Views.Manager;
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
        builder.Services.AddTransient<ImageResizer>();

        builder.Services
    .AddRefitClient<IApiAuthService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://10.0.2.2:5000"));

        builder.Services
    .AddRefitClient<IApiService>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://10.0.2.2:5000"))
    .AddHttpMessageHandler<AuthHeaderHandler>();

        builder.Services.AddSingleton<IConnectivity>((e) => Connectivity.Current);

        //loading 
        builder.Services.AddTransient<LoadingPage>();
        builder.Services.AddSingleton<LoadingViewModel>();

        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<RegistrationViewModel>();

        //user catalog page
        builder.Services.AddSingleton<UserCatalogViewModel>();
        builder.Services.AddTransient<UserCatalogPage>();
        //user catalog - detail page
        builder.Services.AddTransient<UserCatalogDetailViewModel>();
        builder.Services.AddTransient<UserCatalogDetailPage>();
        //user catalog - detail page - create order page
        builder.Services.AddTransient<UserCreateOrderViewModel>();
        builder.Services.AddTransient<CreateOrderPage>();

        //user chat page
        builder.Services.AddTransient<UserChatViewModel>();
        builder.Services.AddTransient<UserChatPage>();

        //user acc page
        builder.Services.AddTransient<UserAccountPage>();
        builder.Services.AddSingleton<UserAccountViewModel>();

        //admin
        builder.Services.AddTransient<AdminMainPage>();
        builder.Services.AddSingleton<AdminMainViewModel>();

        builder.Services.AddTransient<AdminManageCompanyPage>();
        builder.Services.AddSingleton<AdminManageCompanyViewModel>();

        builder.Services.AddTransient<DetailManageCompanyPage>();
        builder.Services.AddSingleton<DetailManageCompanyViewModel>();

        //admin manager
        builder.Services.AddTransient<AdminManagerControlPage>();
        builder.Services.AddSingleton<AdminManagerControlViewModel>();

        builder.Services.AddTransient<DetailControlManagerPage>();
        builder.Services.AddSingleton<DetailControlManagerViewModel>();


        //manager
        builder.Services.AddTransient<ManagerMainPage>();
        builder.Services.AddSingleton<ManagerMainViewModel>();

        //service
        builder.Services.AddTransient<ManagerServicePage>();
        builder.Services.AddSingleton<ManagerServiceViewModel>();

        builder.Services.AddTransient<DetailServicePage>();
        builder.Services.AddSingleton<DetailServiceViewModel>();

        builder.Services.AddTransient<ManagerOrderPage>();
        builder.Services.AddSingleton<ManagerOrderViewModel>();

        builder.Services.AddTransient<ManagerCompanyPage>();
        builder.Services.AddSingleton<ManagerCompanyViewModel>();

        var app = builder.Build();
		return app;
	}
}
