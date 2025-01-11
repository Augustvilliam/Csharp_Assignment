using Business.Interfaces;
using Business.Models;

namespace MobileApp.Pages;

public partial class UserImportPage : ContentPage
{
    private readonly IImportExportService _importExportService;
    private readonly IUserService _userService;

    public UserImportPage(IImportExportService importExportService, IUserService userService)
    {
        InitializeComponent();
        _importExportService = importExportService;
        _userService = userService;
    }

    private async void Button_Back_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///UserMainPage");
    }

    private void Button_Export_Clicked(object sender, EventArgs e)
    {
        try
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fileName = Path.Combine(desktopPath, "users.json");

            var users = _userService.GetAll().ToList(); // Konverterar till List<User>
            _importExportService.SaveListToFile<User>(users, fileName);

            DisplayAlert("Success", $"Users exported to {fileName}", "OK");
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", $"Failed to export users: {ex.Message}", "OK");
        }
    }

    private void Button_Import_Clicked(object sender, EventArgs e)
    {
        try
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fileName = Path.Combine(desktopPath, "users.json");

            var importedUsers = _importExportService.LoadListFromFile<User>(fileName);

            foreach (var user in importedUsers)
            {
                _userService.Add(user);
            }

            DisplayAlert("Success", $"Imported {importedUsers.Count} users from {fileName}.", "OK");
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", $"Failed to import users: {ex.Message}", "OK");
        }
    }
}
