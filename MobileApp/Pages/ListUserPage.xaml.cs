using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using Business.Interfaces;
using Business.Models;
using Business.Services;

namespace MobileApp.Pages;

public partial class ListUserPage : ContentPage
{
    private readonly IUserService _userService;
    private readonly IFileService _fileService;
    private ObservableCollection<User> _users;
    private User _selectedUser;
    public ListUserPage(IUserService userService, IFileService fileService)
    {
        InitializeComponent();
        _userService = userService;
        _fileService = fileService;
        _users = new ObservableCollection<User>(_userService.GetAll());

    }


    private async void Button_Create_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Entry_FirstName.Text))
        {
            await DisplayAlert("Error", "FirsName is Requ", "ok");
            return;
        }

        var newUser = new User
        {
            UserId = Guid.NewGuid().ToString(),
            FirstName = Entry_FirstName.Text
        };
        _userService.Add(newUser);
        _users.Add(newUser);
        Entry_FirstName.Text = string.Empty;

    }

    private void Button_Delete_Clicked(object sender, EventArgs e)
    {

    }

    private void Button_Edit_Clicked(object sender, EventArgs e)
    {

    }
}