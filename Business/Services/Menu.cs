
namespace Business.Services;

public class Menu
{
    private readonly List<User> Users;
    public Menu(List<User> users)
    {
        Users = users; 
    }
    public void ShowMenu()
    {
        while (true)
        {
            MainMenu();
        }
    }

    static void MainMenu()
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
                User();
                break;
            case "2":
                Edit();
                break;
            case "q":
                Quit();
                break;
            default:
                InvalidMenu();
                break;
        }
    }

    private void EditMenu()
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

    private void ImportMenu()
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
    
   

    static void User()
    {
        Console.Clear();
        Console.WriteLine("Nothing here WIP, press any key to return.");
        Console.ReadKey();
    }

    static void Edit()
    {
        Console.Clear();
        Console.WriteLine("Nothing here WIP, press any key to return.");
        Console.ReadKey();
    }

    static void Delete()
    {
        Console.Clear();
        Console.WriteLine("Nothing here WIP, press any key to return.");
        Console.ReadKey();
    }

    static void Back()
    {
        Console.Clear();
        Console.WriteLine("Nothing here WIP, press any key to return.");
        Console.ReadKey();
    }

    static void EditUser()
    {
        Console.Clear();
        Console.WriteLine("Nothing here WIP, press any key to return.");
        Console.ReadKey();
    }

    static void ImportUser()
    {
        Console.Clear();
        Console.WriteLine("WIP nothing to see here");
        Console.ReadKey();

    }
    static void Quit()
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
    static void InvalidMenu()
    {
        Console.Clear();
        Console.WriteLine("Please choose a valid option.");
        Console.ReadKey();
    }

}