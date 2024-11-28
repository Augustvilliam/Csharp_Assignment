
namespace Business.Services;

public class Menu
{
    public void ShowMenu()
    {
        while (true)
        {
            MainMenu();
        }
    }

    private void MainMenu()
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
                user();
                break;
            case "2":
                edit();
                break;
            case "q":
                Quit();
                break;
            default:
                InvalidMenu();
                break;

        }
    }


    private void user()
    {
        Console.Clear();
        Console.WriteLine("Nothing here WIP, press any key to return.");
        Console.ReadKey();
    }

    private void edit()
    {
        Console.Clear();
        Console.WriteLine("Nothing here WIP, press any key to return.");
        Console.ReadKey();
    }


    private void Quit()
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
    private void InvalidMenu()
    {
        Console.Clear();
        Console.WriteLine("Please choose a valid option.");
        Console.ReadKey();
    }
}
