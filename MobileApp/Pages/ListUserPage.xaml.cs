using System.Collections.ObjectModel;
using System.Diagnostics;
using Business.Factory;
using Business.Helper;
using Business.Interfaces;
using Business.Models;

namespace MobileApp.Pages;

public partial class ListUserPage : ContentPage
{
    private readonly IUserService _userService;
    private readonly IFileService _fileService;
    private readonly IUserFactory _userFactory;
    private ObservableCollection<User> _users;
    private User _selectedUser;
    public ListUserPage(IUserService userService, IFileService fileService, IUserFactory userFactory)
    {
        InitializeComponent();
        _userService = userService;
        _fileService = fileService;
        _userFactory = userFactory;
        _users = new ObservableCollection<User>(_userService.GetAll());

        LoadUsers();

    }

    private void LoadUsers()
    {
        try
        {
            var loadedUsers = _userService.GetAll();
            _users = new ObservableCollection<User>(loadedUsers);
            UserListView.ItemsSource = _users;
        }
        catch (Exception ex) 
        {
            Debug.WriteLine($"Error loading users: {ex.Message}");
            DisplayAlert("Error", "Failed to load users.", "OK");
             
        }
    }

    private void ReloadUsers()
    {
        try
        {
            var loadedUsers = _userService.GetAll().ToList(); // Definiera loadedUsers här
            _users.Clear();
            foreach (var user in loadedUsers)
            {
                _users.Add(user);
            }
            Debug.WriteLine($"Reloading {loadedUsers.Count} users from service.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error reloading users: {ex.Message}");
            DisplayAlert("Error", "Failed to reload users.", "OK");
        }

 
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ReloadUsers();
    }

    private async void Button_EditConfirm_Clicked(object sender, EventArgs e)
    {
        if (_selectedUser == null)
            return;

        _selectedUser.FirstName = Entry_FirstName.Text;
        _selectedUser.LastName = Entry_LastName.Text;
        _selectedUser.Email = Entry_Email.Text;
        _selectedUser.Adress = Entry_Adress.Text;
        _selectedUser.Postal = Entry_Postal.Text; 
        _selectedUser.Locality = Entry_Locality.Text;
        _selectedUser.Phonenmbr = Entry_Phone.Text;

        if (!UserValidation.ValidateUser(_selectedUser, out string errorMessage))
        {
            await DisplayAlert("Validation Error", errorMessage, "OK");
            return;
        }

        _userService.EditUser(_selectedUser.UserId, _selectedUser);
        _fileService.SaveListToFile(_users.ToList());

        await DisplayAlert("Sucess!", $"User: {_selectedUser.UserId} was uppdated", "ok");
        ClearForm();
        Button_Create.IsVisible = true;
        Button_EditConfirm.IsVisible = false;
    }


    private async void Button_Create_Clicked(object sender, EventArgs e)
    {

        var newUser = _userFactory.CreateDefultUser();

        newUser.FirstName = Entry_FirstName.Text;
        newUser.LastName = Entry_LastName.Text;
        newUser.Email = Entry_Email.Text;
        newUser.Adress = Entry_Adress.Text;
        newUser.Postal = Entry_Postal.Text;
        newUser.Locality = Entry_Locality.Text;
        newUser.Phonenmbr = Entry_Phone.Text;



        if (!UserValidation.ValidateUser(newUser, out string errorMessage))
        {
            await DisplayAlert(" Validation Error", errorMessage, "ok");
            return;
        }



        _userService.Add(newUser);
        _users.Add(newUser);

        await DisplayAlert("Sucess", "User was created", "ok");
        ClearForm();

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

    private  void Button_Edit_Clicked(object sender, EventArgs e)
    {
        if ((sender as Button)?.BindingContext is User user)
        {
            _selectedUser = user;
            Entry_FirstName.Text = _selectedUser.FirstName;
            Entry_LastName.Text = _selectedUser.LastName;
            Entry_Email.Text = _selectedUser.Email;
            Entry_Adress.Text = _selectedUser.Adress;
            Entry_Postal.Text = _selectedUser.Postal;
            Entry_Locality.Text = _selectedUser.Locality;
            Entry_Phone.Text = _selectedUser.Phonenmbr;

            Button_Create.IsVisible = false;
            Button_EditConfirm.IsVisible = true;
        }

    }

    private void UsertListView_selectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is User selectedUser)
        {
            Label_UserId.Text = selectedUser.UserId;
            Label_UserName.Text = $"{selectedUser.FirstName} {selectedUser.LastName}";
            Label_Email.Text = selectedUser.Email;
            Label_Adress.Text = selectedUser.Adress;
            Label_Postal.Text = selectedUser.Postal;
            Label_Locality.Text = selectedUser.Locality;
            Label_Phone.Text = selectedUser.Phonenmbr;
        }
        else
        {
            ClearUserDetails();
        }
    }
    private void ClearForm()
    {
        Entry_FirstName.Text = string.Empty;
        Entry_LastName.Text = string.Empty;
        Entry_Email.Text = string.Empty;
        Entry_Adress.Text = string.Empty;
        Entry_Postal.Text = string.Empty;
        Entry_Locality.Text = string.Empty;
        Entry_Phone.Text = string.Empty;
        _selectedUser = null;

    }
    private void ClearUserDetails()
    {
        Label_UserId.Text = string.Empty;   
        Label_UserName.Text = string.Empty;
        Label_Email.Text = string.Empty;
        Label_Adress.Text = string.Empty;
        Label_Postal.Text = string.Empty;
        Label_Locality.Text = string.Empty;
        Label_Phone.Text = string.Empty;
    }
}