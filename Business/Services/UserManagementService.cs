
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
            Console.WriteLine("3. Edit User");
            Console.WriteLine("4. Delete User");
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
                case "4":
                    DeleteUser();
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

        string errorMessage;

        //Förnamn
        do
        {
            Console.WriteLine("Please Enter your first name.");
            user.FirstName = Console.ReadLine()!;
            if (!UserValidation.ValidateName(user.FirstName))
            {
                Console.WriteLine("First name must contain at least 2 characters.");
            }
        } while (!UserValidation.ValidateName(user.FirstName));

        //Efternamn

        do
        {
            Console.WriteLine("Please Enter your Last name.");
            user.LastName = Console.ReadLine()!;
            if (!UserValidation.ValidateName(user.LastName))
            {
                Console.WriteLine("Last name must contain at least 2 characters.");
            }
        } while (!UserValidation.ValidateName(user.LastName));


        //Email
        do
        {
            Console.WriteLine("Please Enter your Email.");
            user.Email = Console.ReadLine()!;
            if (!UserValidation.ValidateEmail(user.Email))
            {
                Console.WriteLine("Your email adress is invalid. Please enter a valid one using the following format e.g Example@example.ex ");
            }
        } while (!UserValidation.ValidateEmail(user.Email));
  
        //Adress
        do
        {
            Console.WriteLine("Please Enter your Adress.");
            user.Adress = Console.ReadLine()!;
            if (!UserValidation.ValidateAdress(user.Adress))
            {
                Console.WriteLine("Your adress is not valid.");
            }
        } while (!UserValidation.ValidateAdress(user.Adress));

        //Postkod
        do
        {
            Console.WriteLine("Please Enter your Postal-code.");
            user.Postal = Console.ReadLine()!;
            if (!UserValidation.ValidatePostal(user.Postal))
            {
                Console.WriteLine("Your postal-code should only include numbers.");
            }
        } while (!UserValidation.ValidatePostal(user.Postal));

        //Ort
        do
        {
            Console.WriteLine("Please Enter your Locality.");
            user.Locality = Console.ReadLine()!;
            if (!UserValidation.ValidateLocality(user.Locality))
            {
                Console.WriteLine("Your locality must be atleast 2 letters long.");
            }
        } while (!UserValidation.ValidateLocality(user.Locality));

        //Telefonnummer
        do
        {
            Console.WriteLine("Please Enter your Phone number.");
            user.Phonenmbr = Console.ReadLine()!;
            if (!UserValidation.ValidatePhone(user.Phonenmbr))
            {
                Console.WriteLine("Your Phonenumber must only contain digits.");
            }
        } while (!UserValidation.ValidatePhone(user.Phonenmbr));

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
        if (user == null)
        {
            Console.WriteLine("User not found.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Edit user details. Leave blank to keep current information.");

        Console.WriteLine($"Current First Name: {user.FirstName}");
        string firstName = Console.ReadLine()!;
        user.FirstName = string.IsNullOrEmpty(firstName) ? user.FirstName : firstName;


        Console.WriteLine($"Current Last Name: {user!.LastName}");
        string lastName = Console.ReadLine()!;
        user.LastName = string.IsNullOrEmpty(lastName) ? user.LastName : lastName;

        Console.WriteLine($"Current Email: {user.Email}");
        string email = Console.ReadLine()!;
        user.Email = string.IsNullOrEmpty(email) ? user.Email : email;

        Console.WriteLine($"Current Adress: {user.Adress}");
        string adress = Console.ReadLine()!;
        user.Adress = string.IsNullOrEmpty(adress) ? user.Adress : adress;

        Console.WriteLine($"Current Postal Code: {user.Postal}");
        string postal = Console.ReadLine()!;
        user.Postal = string.IsNullOrEmpty(postal) ? user.Postal : postal;

        Console.WriteLine($"Current Locality: {user.Locality}");
        string locality = Console.ReadLine()!;
        user.Locality = string.IsNullOrEmpty(locality) ? user.Locality : locality;

        Console.WriteLine($"Current Phonenumber: {user.Phonenmbr}");
        string phoneNumber = Console.ReadLine()!;
        user.Phonenmbr = string.IsNullOrEmpty(phoneNumber) ? user.Phonenmbr : phoneNumber;


        if (!UserValidation.ValidateUser(user, out var errorMessage))
        {
            Console.WriteLine($"Something went wrong. {errorMessage}");
            Console.WriteLine("Press any key to try again.");
            Console.ReadKey();
            return;
        }

        _userService.EditUser(userId, user);
        Console.WriteLine("User updated successfully.");
        Console.ReadKey();
    }
    private void DeleteUser()
    {
        Console.WriteLine("Please enter the ID of the user you wish to delete.");
        string userId = Console.ReadLine()!;

        var user = _userService.GetUserById(userId);
        if (user == null)
        {
            Console.WriteLine("User not found.");
        }
        else
        {
            Console.WriteLine($"Are you sure you want to delete user {user.UserId} {user.FirstName} {user.LastName}? y/n");
            string confirm = Console.ReadLine()!;
            if (confirm.ToLower() == "y")
            {
                _userService.DeleteUser(userId);
                Console.WriteLine("User Deleted Successfully.");

            }
            else
            {
                Console.WriteLine("User deletion cancelled.");
            }
        }
        Console.ReadKey();
    }
}


//uppdelad från en hel "MenuService" 3 mindre services på rekomendation av ChatGPT.

