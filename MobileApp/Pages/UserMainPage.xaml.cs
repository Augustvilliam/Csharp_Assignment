namespace MobileApp.Pages;

public partial class UserMainPage : ContentPage
{
	public UserMainPage()
	{
        InitializeComponent();
	}

    private void Button_Quit_Clicked(object sender, EventArgs e) //d�dar appen. klart man ska ha en egen exit knapp. Men ordinarie halvfabrikat f�r vara kvar. 
    {
        Application.Current.Quit();
    }

    private async void Button_Import_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("UserImportPage");
    }

    private async void Button_List_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("ListUserPage");
    }
}