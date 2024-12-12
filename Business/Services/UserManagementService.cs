
using Business.Interfaces;
using Business.Models;

namespace Business.Services;

public class UserManagementService
{
    private readonly IUserService _userService;
    public UserManagementService(IUserService userService)
    {
        _userService = userService;
    }

    public void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the user admin. Please choose one of the options!");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("1. Create User");
            Console.WriteLine("2. Show Existing Users");
            Console.WriteLine("3. Edit/Delete User");
            Console.WriteLine("b. Back to Main Menu");
            var option = Console.ReadLine();

            switch (option!.ToLower())
            {
                case "1":
                    CreateUser();
                    break;
                case "2":
                    ViewAllUsers();
                    break;
                case "3":
                    EditUser();
                    break;
                case "b":
                    return;
                default:
                    Console.WriteLine("Please choose a valid option.");
                    Console.ReadKey();
                    break;
            }
        }
    }
    
    private void CreateUser()
    {
        Console.Clear();
        Console.WriteLine("Enter User details:");
        Console.Clear();

        User user = new();

        Console.WriteLine("Please Enter your first name.");
        user.FirstName = Console.ReadLine()!;

        Console.WriteLine("Please Enter your Last name.");
        user.LastName = Console.ReadLine()!;

        Console.WriteLine("Please Enter your Email adress.");
        user.Email = Console.ReadLine()!;
        
        _userService.Add(user);
        
        Console.WriteLine("User created successfully. Press any key to continue.");
        Console.ReadKey();
    }
    private void ViewAllUsers()
    {
        Console.Clear();
        var users = _userService.GetAll();
        foreach(var user in users)
        {
            Console.WriteLine($"ID:{user.UserId} Name:{user.FirstName}{user.LastName} Email:{user.Email}");
        }

        Console.WriteLine("Press any ket to return.");
        Console.ReadKey();
    }
    private void EditUser()
    {
        Console.Clear();
        Console.WriteLine("Nothing here WIP, press any key to return.");
        Console.ReadKey();
    }


}


//uppdelad från en hel "MenuService" 3 mindre services på rekomendation av ChatGPT.

