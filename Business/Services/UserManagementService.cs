
using Business.Helper;
using Business.Interfaces;
using Business.Models;

namespace Business.Services;

public class UserManagementService
{
    private readonly IUserService _userService;
    private readonly IUserFactory _userFactory;
    public UserManagementService(IUserService userService, IUserFactory userFactory)
    {
        _userService = userService;
        _userFactory = userFactory; 
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
            Console.WriteLine("5. Create Defult User");
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
                case "5":
                    CreateDefaultUser();
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
    
    private void CreateUser() // skapar användare. tilldelar Id direket.
    {
        Console.Clear();
        Console.WriteLine("Enter User details:");
        

        User user = new()
        {
           UserId = IdGenerator.GenerateShortId(5),
        };


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
    private void ViewAllUsers() // se alla användare. med fullständig info.
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
    private void EditUser() //redigera användare genomm att ange identiskt ID. därav max 5 täcken på id.
    {
        Console.Clear();
        Console.WriteLine("Entar a valid User ID to edit.");
        string userId = Console.ReadLine()!;

        var user = _userService.GetUserById(userId);
        if (user == null)
        {
            Console.WriteLine("User not found.");
            Console.ReadKey();
            return;
        }
        Console.Clear();
        Console.WriteLine($"Edit user details. Leave blank to keep current information. Current user: {user.UserId}");
        do
        {
            Console.WriteLine($"Current First Name: {user.FirstName}");
            Console.WriteLine("Enter a new first name. Or leave blank if you wish for it to remain the same.");
            string input = Console.ReadLine()!;
            if (!string.IsNullOrEmpty(input))
            {
                if (UserValidation.ValidateName(input))
                {
                    user.FirstName = input;
                    break;
                }
                else
                {
                    Console.WriteLine("Your first name must be aleast two characters long.");
                }
            }
            else
            {
                break;
            }
        }
        while (true);

        do
        {
            Console.WriteLine($"Current Last Name: {user.LastName}");
            Console.WriteLine("Enter a new first name. Or leave blank if you wish for it to remain the same.");
            string input = Console.ReadLine()!;
            if (!string.IsNullOrEmpty(input))
            {
                if (UserValidation.ValidateName(input))
                {
                    user.LastName = input;
                    break;
                }
                else
                {
                    Console.WriteLine("Your first name must be aleast two characters long.");
                }
            }
            else
            {
                break;
            }
        }
        while (true);

        do
        {
            Console.WriteLine($"Current Email: {user.Email}");
            Console.WriteLine("Enter a new Email. Or leave blank if you wish for it to remain the same.");
            string input = Console.ReadLine()!;
            if (!string.IsNullOrEmpty(input))
            {
                if (UserValidation.ValidateEmail(input))
                {
                    user.FirstName = input;
                    break;
                }
                else
                {
                    Console.WriteLine("Your Email must adhear to the following format xx@xx.xx");
                }
            }
            else
            {
                break;
            }
        }
        while (true);

        do
        {
            Console.WriteLine($"Current Adress: {user.Adress}");
            Console.WriteLine("Enter a new Adress. Or leave blank if you wish for it to remain the same.");
            string input = Console.ReadLine()!;
            if (!string.IsNullOrEmpty(input))
            {
                if (UserValidation.ValidateAdress(input))
                {
                    user.FirstName = input;
                    break;
                }
                else
                {
                    Console.WriteLine("Your Adress must be aleast two characters long.");
                }
            }
            else
            {
                break;
            }
        }
        while (true);

        do
        {
            Console.WriteLine($"Current Postal-code: {user.Postal}");
            Console.WriteLine("Enter a new postal-code. Or leave blank if you wish for it to remain the same.");
            string input = Console.ReadLine()!;
            if (!string.IsNullOrEmpty(input))
            {
                if (UserValidation.ValidatePostal(input))
                {
                    user.FirstName = input;
                    break;
                }
                else
                {
                    Console.WriteLine("Your postal-code may only contain didgets");
                }
            }
            else
            {
                break;
            }
        }
        while (true);

        do
        {
            Console.WriteLine($"Current locality: {user.Locality}");
            Console.WriteLine("Enter a new Locality. Or leave blank if you wish for it to remain the same.");
            string input = Console.ReadLine()!;
            if (!string.IsNullOrEmpty(input))
            {
                if (UserValidation.ValidateLocality(input))
                {
                    user.FirstName = input;
                    break;
                }
                else
                {
                    Console.WriteLine("Your Locality must be aleast two characters long.");
                }
            }
            else
            {
                break;
            }
        }
        while (true);

        do
        {
            Console.WriteLine($"Current Phonenumber: {user.Phonenmbr}");
            Console.WriteLine("Enter a new Phonenumber. Or leave blank if you wish for it to remain the same.");
            string input = Console.ReadLine()!;
            if (!string.IsNullOrEmpty(input))
            {
                if (UserValidation.ValidatePhone(input))
                {
                    user.FirstName = input;
                    break;
                }
                else
                {
                    Console.WriteLine("Your Phonenumber may only contain didgets.");
                }
            }
            else
            {
                break;
            }
        }
        while (true);

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
    private void CreateDefaultUser()
    {
        var defaultUser = _userFactory.CreateDefultUser();
        _userService.Add(defaultUser);
        Console.WriteLine($"Default User created: {defaultUser.FirstName }{defaultUser.LastName}");
        Console.ReadKey();


    }
}


//uppdelad från en hel "MenuService" 3 mindre services på rekomendation av ChatGPT.

