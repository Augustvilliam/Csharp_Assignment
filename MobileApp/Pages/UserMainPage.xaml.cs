namespace MobileApp.Pages;

public partial class UserMainPage : ContentPage
{
	public UserMainPage()
	{
        InitializeComponent();
	}

    private async void Button_List_Clicked(object sender, EventArgs e)
    {
        var listUserPage = App.Current.Handler.MauiContext.Services.GetService<ListUserPage>();
        await Navigation.PushAsync(listUserPage);
    }
}