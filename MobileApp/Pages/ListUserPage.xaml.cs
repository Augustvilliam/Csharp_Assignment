using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using Business.Interfaces;
using Business.Models;
using Business.Services;

namespace MobileApp.Pages;

public partial class ListUserPage : ContentPage
{
    private readonly IFileService _fileService;
    private ObservableCollection<User> _users;
    private User _selectedUser;
    public ListUserPage(IFileService fileService)
	{
		InitializeComponent();
        _fileService = fileService;
        LoadUsersFromFile();
    }

    private async void Button_Back_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("UserMainPage");

    }
    //G OnAppearing Gjord med chatGPT. Anropas när sidan blir aktiv för att alltid ladda in användarlistan.
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ReloadUserFromFile();
    }

    private void LoadUsersFromFile()
    {
        var loadedUsers = _fileService.LoadList();
        _users = new ObservableCollection<User>(loadedUsers);
        UserListView.ItemsSource = _users;
    }
    private void ReloadUserFromFile()
    {
        var loadedUsers = _fileService.LoadList();
        _users.Clear();
        foreach (var user in loadedUsers)
        {
            _users.Add(user);
        }
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

            _users.Add(newUser);
            _fileService.SaveListToFile(_users.ToList());

            await DisplayAlert("Success", $"User{newUser.FirstName} {newUser.LastName} {newUser.UserId} created.", "OK");
        }
        else
        {

            _selectedUser.FirstName = Entry_FirstName.Text;
            _selectedUser.LastName = Entry_LastName.Text;
            _selectedUser.Email = Entry_Email.Text;
            _selectedUser.Adress = Entry_Adress.Text;
            _selectedUser.Postal = Entry_Postal.Text;
            _selectedUser.Locality = Entry_Locality.Text;
            _selectedUser.Phonenmbr = Entry_Phone.Text;

            _fileService.SaveListToFile(_users.ToList());
            await DisplayAlert("Sucess", "Users updated", "ok");

            _selectedUser = null;
            Button_Create.Text = "Create User";
        }
        ClearForm();
    }

    private void Button_Delete_Clicked(object sender, EventArgs e)
    {
        var user = (sender as Button).BindingContext as User;
        if (user != null)
        {
            _users.Remove(user);
            _fileService.SaveListToFile(_users.ToList());
        }
    }

    private void Button_Edit_Clicked(object sender, EventArgs e)
    {
        _selectedUser = (sender as Button).BindingContext as User;
        if (_selectedUser != null)
        {
            Entry_FirstName.Text = _selectedUser.FirstName;
            Entry_LastName.Text = _selectedUser.LastName;
            Entry_Email.Text = _selectedUser.Email;
            Entry_Adress.Text = _selectedUser.Adress;
            Entry_Postal.Text = _selectedUser.Postal;
            Entry_Locality.Text = _selectedUser.Locality;
            Entry_Phone.Text = _selectedUser.Phonenmbr;

            Button_Create.Text = "Update User";

        }
    }

    private void ClearForm() //Megalat Chatgpt genererad. Tömmer textfälten
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
}