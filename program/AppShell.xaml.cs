using program.Views;
using program.Views.User;

namespace program;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        //log-reg
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
        Routing.RegisterRoute(nameof(AdminPage), typeof(AdminPage));
        Routing.RegisterRoute(nameof(UserPage), typeof(UserPage));

        //user
        Routing.RegisterRoute(nameof(UserAccountPage), typeof(UserAccountPage));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(UserCatalogPage), typeof(UserCatalogPage));
        Routing.RegisterRoute(nameof(UserChatPage), typeof(UserChatPage));
    }
}
