using program.Views;
using program.Views.Admin;
using program.Views.Manager;
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
        Routing.RegisterRoute(nameof(UserChatPage), typeof(UserChatPage));

        Routing.RegisterRoute(nameof(UserCatalogPage), typeof(UserCatalogPage));
        Routing.RegisterRoute(nameof(UserCatalogDetailPage), typeof(UserCatalogDetailPage));
        Routing.RegisterRoute(nameof(CreateOrderPage), typeof(CreateOrderPage));

        //admin
        Routing.RegisterRoute(nameof(AdminMainPage), typeof(AdminMainPage));

        Routing.RegisterRoute(nameof(AdminManageCompanyPage), typeof(AdminManageCompanyPage));
        Routing.RegisterRoute(nameof(DetailManageCompanyPage), typeof(DetailManageCompanyPage));
        
        Routing.RegisterRoute(nameof(AdminManagerControlPage), typeof(AdminManagerControlPage));
        Routing.RegisterRoute(nameof(DetailControlManagerPage), typeof(DetailControlManagerPage));

        //manager
        Routing.RegisterRoute(nameof(ManagerMainPage), typeof(ManagerMainPage));

        Routing.RegisterRoute(nameof(ManagerCompanyPage), typeof(ManagerCompanyPage));

        Routing.RegisterRoute(nameof(ManagerOrderPage), typeof(ManagerOrderPage));

        Routing.RegisterRoute(nameof(ManagerServicePage), typeof(ManagerServicePage));
        Routing.RegisterRoute(nameof(DetailServicePage), typeof(DetailServicePage));
    }
}
