using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using Business.Interfaces;
using Business.Models;
using Business.Services;

namespace MobileApp.Pages;

public partial class ListUserPage : ContentPage
{
    private readonly IUserService _userService;
    private ObservableCollection<User> _users;
    private User _selectedUser;
    public ListUserPage(IUserService userService)
	{
		InitializeComponent();
        _userService = userService;
        _users = new ObservableCollection<User>(_userService.GetAll());
        UserListView.ItemsSource = _users;
    }

    private async void Button_Back_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("UserMainPage");

    }

    private async void Button_Create_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Entry_FirstName.Text) && !string.IsNullOrEmpty(Entry_LastName.Text))
        {
            var newUser = new User
            {
                UserId = Guid.NewGuid().ToString(),
                FirstName = Entry_FirstName.Text,
                LastName = Entry_LastName.Text,
                Email = Entry_Email.Text,
                Adress = Entry_Adress.Text,
                Postal = Entry_Postal.Text, 
                Locality = Entry_Locality.Text,
                Phonenmbr = Entry_Phone.Text
            };
           
            _userService.Add(newUser);
            _users.Add(newUser);
            await DisplayAlert("Success", $"User{newUser.FirstName} {newUser.LastName} {newUser.UserId} created.", "OK");
        }
        else
        {
            await DisplayAlert("Error", "Please fill in all the fields", "OK");
        }
    }

    private void Button_Delete_Clicked(object sender, EventArgs e)
    {
        var user = (sender as Button).BindingContext as User;
        if (user != null)
        {
            _userService.DeleteUser(user.UserId);
            _users.Remove(user);
        }
    }

    private void Button_Edit_Clicked(object sender, EventArgs e)
    {
        _selectedUser = (sender as Button).BindingContext as User;
        if (_selectedUser != null)
        {
            Entry_FirstName.Text = _selectedUser.FirstName;
            Entry_LastName.Text = _selectedUser.LastName;
            Entry_Adress.Text = _selectedUser.FirstName;
            Entry_Postal.Text = _selectedUser.FirstName;
            Entry_Locality.Text = _selectedUser.FirstName;
            Entry_Phone.Text = _selectedUser.FirstName;

        }
    }
}