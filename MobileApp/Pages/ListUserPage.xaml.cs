namespace MobileApp.Pages;

public partial class ListUserPage : ContentPage
{
	public ListUserPage()
	{
		InitializeComponent();
	}

    private async void Button_Back_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("UserMainPage");

    }
}