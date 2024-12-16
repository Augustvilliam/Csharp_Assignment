
using Business.Helper;
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
        

        User user = new()
        {
           UserId = IdGenerator.GenerateShortId(5),
        };

    
        Console.WriteLine("Please Enter your first name.");
        user.FirstName = Console.ReadLine()!;

        Console.WriteLine("Please Enter your Last name.");
        user.LastName = Console.ReadLine()!;

        Console.WriteLine("Please Enter your Email adress.");
        user.Email = Console.ReadLine()!;

        Console.WriteLine("Please Enter your Adress adress.");
        user.Adress = Console.ReadLine()!;

        Console.WriteLine("Please Enter your Postal-Code.");
        user.Postal = Console.ReadLine()!;

        Console.WriteLine("Please Enter your locality.");
        user.Locality = Console.ReadLine()!;

        Console.WriteLine("Please Enter your Phone Number.");
        user.Phonenmbr = Console.ReadLine()!;

        if (!UserValidation.ValidateUser(user, out var errorMessage))
        {
            Console.WriteLine($"Validation failed:{errorMessage}");
            Console.WriteLine("Press any key to try again.");
            Console.ReadKey();
            return;
        }

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
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine($"ID:{user.UserId}");
            Console.WriteLine($"Name:{user.FirstName} {user.LastName}");
            Console.WriteLine($"Email:{user.Email}");
            Console.WriteLine($"Adress:{user.Adress}");
            Console.WriteLine($"Postal-Code:{user.Postal}");
            Console.WriteLine($"Locality:{user.Locality}");
            Console.WriteLine($"Phone Number:{user.Phonenmbr}");
            Console.WriteLine("------------------------------------------------------------------------------");
        }

        Console.WriteLine("Press any key to return.");
        Console.ReadKey();
    }
    private void EditUser()
    {
        Console.WriteLine("Entar a valid User ID to edit.");
        string userId = Console.ReadLine()!;

        var user = _userService.GetUserById(userId);
        if (user != null)
        {
            Console.WriteLine("User not found.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Edit user details. Leave blank to keep current information.");

        Console.WriteLine($"Current First Name: {user!.FirstName}");
        string firstName = Console.ReadLine()!;
        user.FirstName = string.IsNullOrEmpty(firstName) ? user.FirstName : firstName;
    

        Console.WriteLine($"Current First Name: {user!.LastName}");
        string lastName = Console.ReadLine()!;
        user.FirstName = string.IsNullOrEmpty(lastName) ? user.LastName : lastName;

        Console.WriteLine($"Current First Name: {user!.Email}");
        string email = Console.ReadLine()!;
        user.Email = string.IsNullOrEmpty(email) ? user.Email : email;

        Console.WriteLine($"Current First Name: {user!.Adress}");
        string adress = Console.ReadLine()!;
        user.Adress = string.IsNullOrEmpty(adress) ? user.Adress : adress;

        Console.WriteLine($"Current First Name: {user!.Postal}");
        string postal = Console.ReadLine()!;
        user.Postal = string.IsNullOrEmpty(postal) ? user.Postal : postal;

        Console.WriteLine($"Current First Name: {user!.Locality}");
        string locality = Console.ReadLine()!;
        user.Locality = string.IsNullOrEmpty(locality) ? user.Locality : firstName;

        Console.WriteLine($"Current First Name: {user!.Phonenmbr}");
        string phoneNumber = Console.ReadLine()!;
        user.Phonenmbr = string.IsNullOrEmpty(phoneNumber) ? user.Phonenmbr : phoneNumber;


        if (!UserValidation.ValidateUser(user, out var errorMessage))
        {
            Console.WriteLine($"Something went wrong. {errorMessage}");
            Console.WriteLine("Press any key to try again.");
            Console.ReadKey();
            return;
        }
    }


}


//uppdelad från en hel "MenuService" 3 mindre services på rekomendation av ChatGPT.

