using Business.Interfaces;
using Business.Models;
using Business.Services;

namespace MobileApp.Pages;

public partial class ListUserPage : ContentPage
{
    private readonly IUserService _userService;

    public ListUserPage(IUserService userService)
	{
		InitializeComponent();
        _userService = userService;
        DisplayUsers();
	}

    private void DisplayUsers()
    {
        var users = _userService.GetAll();
        string userList = string.Join("\n", users.Select(u => $"{u.FirstName} {u.LastName}"));

        DisplayAlert("User List", userList, "OK");
    }
    
    private async void Button_Create_Clicked(object sender, EventArgs e)
    {
        string firstName = await DisplayPromptAsync("Create User", "Enter first name:");
        string lastName = await DisplayPromptAsync("Create User", "Enter Last name:");

        if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
        {
            var newUser = new User
            {
                UserId = Guid.NewGuid().ToString(),
                FirstName = firstName,
                LastName = lastName,

            };
            _userService.Add(newUser);
            await DisplayAlert("Sucess", "User created sucessfully.", "OK");
            DisplayUsers();
        }
    
    }

    private void Button_Export_Clicked(object sender, EventArgs e)
    {

    }

    private void Button_Back_Clicked(object sender, EventArgs e)
    {

    }

    private async void Button_Delete_Clicked(object sender, EventArgs e)
    {
        string userId = await DisplayPromptAsync("Dlete User", "Enter User ID to delete", "OK");

        if (!string.IsNullOrEmpty(userId))
        {
            var user = _userService.GetAll().FirstOrDefault(u => u.UserId == userId);

            if (user != null)
            {
                var userList = _userService.GetAll().ToList();
                userList.Remove(user);

                await DisplayAlert("Delted", "User has been Removed", "OK");
                DisplayUsers();

            }
            else
            {
                await DisplayAlert("Error", "User Not found", "OK");
            }
        }
    }

}
