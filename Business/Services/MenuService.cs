
using Business.Models;
namespace Business.Services;

public class MenuService
{
    public readonly UserService _userService = new ();
    public void ShowMenu()
    {
        while (true)
        {
            MainMenu();
        }
    }

    public void MainMenu()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the user admin. Please choose one of the options!");
        Console.WriteLine("-------------------------------------------");
        Console.WriteLine("1. Create User");
        Console.WriteLine("2. Show/edit existing users");
        Console.WriteLine("3. Import an existing user from an existing File");
        Console.WriteLine("q. Exist the Admin Tool");
        Console.WriteLine("-------------------------------------------");
        var option = Console.ReadLine();

        switch (option!.ToLower())
        {
            case "1":
                CreateUserDialog();
                break;
            case "2":
                EditMenu();
                break;
            case "3":
                ImportMenu();
                break;
            case "q":
                Quit();
                break;
            default:
                InvalidMenu();
                break;
        }
    }

    public void CreateUserDialog()
    {
        Console.Clear();

        User user = new();

        Console.WriteLine("Please Enter your first name.");
        user.FirstName = Console.ReadLine()!;

        Console.WriteLine("Please Enter your Last name.");
        user.LastName = Console.ReadLine()!;

        Console.WriteLine("Please Enter your Email adress.");
        user.Email = Console.ReadLine()!;

        _userService.Add(user);

    }

    public void ViewAllUsersDialog()
    {
        Console.Clear();

        var users = _userService.GetAll();
        foreach (var user in users)
        {
            Console.WriteLine($"Id. {user.UserId}");
            Console.WriteLine($"Full Name. {user.FirstName} {user.LastName}");
            Console.WriteLine($"Email. {user.Email}");

        }
        Console.ReadKey();
    }



    public void EditMenu()
    {
        Console.Clear();
        Console.WriteLine("Please select an option");
        Console.WriteLine("-------------------------------------------");
        Console.WriteLine("1. Edit User");
        Console.WriteLine("2. Delete User");
        Console.WriteLine("3. Back To Main Menu");
        Console.WriteLine("-------------------------------------------");
        var option = Console.ReadLine();

        switch (option!.ToLower())
        {
            case "1":
                EditUser();
                break;
            case "2":
                Delete();
                break;
            case "q":
                Back();
                break;
            default:
                InvalidMenu();
                break;
        }
    }

    public void ImportMenu()
    {
        Console.Clear();
        Console.WriteLine("Enter or copy a valid filepath to import a usersheet.");
        Console.WriteLine("-------------------------------------------");
        Console.WriteLine("1. Import");
        Console.WriteLine("2. Back To main Menu");
        Console.WriteLine("-------------------------------------------");

        var option = Console.ReadLine();

        switch (option!.ToLower())
        {
            case "1":
                ImportUser();
                break;
            case "2":
                Back();
                break;

            default:
                InvalidMenu();
                break;
        }
    }


    public void Delete()
    {
        Console.Clear();
        Console.WriteLine("Nothing here WIP, press any key to return.");
        Console.ReadKey();
    }

    public void Back()
    {
        Console.Clear();
        Console.WriteLine("Nothing here WIP, press any key to return.");
        Console.ReadKey();
    }

    public void EditUser()
    {
        Console.Clear();
        Console.WriteLine("Nothing here WIP, press any key to return.");
        Console.ReadKey();
    }

    public void ImportUser()
    {
        Console.Clear();
        Console.WriteLine("WIP nothing to see here");
        Console.ReadKey();

    }
    public void Quit()
    {
        Console.Clear();
        Console.WriteLine("Do you wish to exist the 'Choremaker 5000?' (y/n): ");
        var option = Console.ReadLine()!;
        if (option.Equals("y", StringComparison.CurrentCultureIgnoreCase))
        {
            Console.Clear();
            Console.WriteLine("Okey, Goodbye!");
            Environment.Exit(0);
        }

    }
    public void InvalidMenu()
    {
        Console.Clear();
        Console.WriteLine("Please choose a valid option.");
        Console.ReadKey();
    }

}