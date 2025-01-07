using System.Collections.ObjectModel;
using System.Diagnostics;
using Business.Interfaces;
using Business.Models;

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

        LoadUsers();

    }

    private void LoadUsers()
    {
        var loadedUsers = _userService.GetAll();
        _users = new ObservableCollection<User>(loadedUsers);
        UserListView.ItemsSource = _users;
    }

    private void ReloadUsers()
    {
        var loadedUsers = _userService.GetAll().ToList(); // Definiera loadedUsers här
        _users.Clear();
        foreach (var user in loadedUsers)
        {
            _users.Add(user);
        }
        Debug.WriteLine($"Reloading {loadedUsers.Count} users from service.");
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ReloadUsers();
    }

    private void Button_EditConfirm_Clicked(object sender, EventArgs e)
    {
        if (_selectedUser == null)
            return;
        _selectedUser.FirstName = Entry_FirstName.Text;
        _userService.EditUser(_selectedUser.UserId, _selectedUser);
        _fileService.SaveListToFile(_users.ToList());

        ClearForm();
        Button_Create.IsVisible = true;
        Button_EditConfirm.IsVisible = false;
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
        _fileService.SaveListToFile(_users.ToList());


        Entry_FirstName.Text = string.Empty;

    }

    private async void Button_Delete_Clicked(object sender, EventArgs e)
    {
        if ((sender as Button)?.BindingContext is User user)
        {
            bool confirm = await DisplayAlert("Confirm Delete", $"Are you sure you want to delete {user.FirstName}?", "Yes", "No");
            if (confirm)
            {
                _userService.DeleteUser(user.UserId);
                _users.Remove(user);
            }
        }

    }

    private void Button_Edit_Clicked(object sender, EventArgs e)
    {
        if ((sender as Button)?.BindingContext is User user)
        {
            _selectedUser = user;
            Entry_FirstName.Text = _selectedUser.FirstName;
            Button_Create.IsVisible = false;
            Button_EditConfirm.IsVisible = true;
        }
    }

    private void ClearForm()
    {
        Entry_FirstName.Text = string.Empty;
        _selectedUser = null;

    }
}