using MobileApp.Pages;

namespace MobileApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(UserMainPage), typeof(UserMainPage));
        Routing.RegisterRoute(nameof(ListUserPage), typeof(ListUserPage));

    }
}
