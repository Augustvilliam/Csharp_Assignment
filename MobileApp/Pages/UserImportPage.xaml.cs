namespace MobileApp.Pages;

public partial class UserImportPage : ContentPage
{
	public UserImportPage()
	{
		InitializeComponent();
	}

    private async void Button_Back_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("UserMainPage");
    }
}