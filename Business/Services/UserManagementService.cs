﻿
using System.ComponentModel.DataAnnotations;
using Business.Helper;
using Business.Interfaces;
using Business.Models;

namespace Business.Services;

public class UserManagementService
{
    private readonly IUserService _userService;
    private readonly IUserFactory _userFactory;
    private readonly IUserValidation _userValidation;
    public UserManagementService(IUserService userService, IUserFactory userFactory, IUserValidation userValidation)
    {
        _userService = userService;
        _userFactory = userFactory;
        _userValidation = userValidation;
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
    
    private void CreateUser() // skapar användare. tilldelar Id direket.
    {
        Console.Clear();
        Console.WriteLine("Enter User details:");
        

        User inputUser = new()
        {
           UserId = IdGenerator.GenerateShortId(5),
        };


        //Förnamn
        do
        {
            Console.WriteLine("Please Enter your first name.");
            inputUser.FirstName = Console.ReadLine()!;
            if (!_userValidation.ValidateName(inputUser.FirstName))
            {
                Console.WriteLine("First name must contain at least 2 characters.");
            }
        } while (!_userValidation.ValidateName(inputUser.FirstName));

        //Efternamn

        do
        {
            Console.WriteLine("Please Enter your Last name.");
            inputUser.LastName = Console.ReadLine()!;
            if (!_userValidation.ValidateName(inputUser.LastName))
            {
                Console.WriteLine("Last name must contain at least 2 characters.");
            }
        } while (!_userValidation.ValidateName(inputUser.LastName));


        //Email
        do
        {
            Console.WriteLine("Please Enter your Email.");
            inputUser.Email = Console.ReadLine()!;
            if (!_userValidation.ValidateEmail(inputUser.Email))
            {
                Console.WriteLine("Your email adress is invalid. Please enter a valid one using the following format e.g Example@example.ex ");
            }
        } while (!_userValidation.ValidateEmail(inputUser.Email));
  
        //Adress
        do
        {
            Console.WriteLine("Please Enter your Adress.");
            inputUser.Adress = Console.ReadLine()!;
            if (!_userValidation.ValidateAdress(inputUser.Adress))
            {
                Console.WriteLine("Your adress is not valid.");
            }
        } while (!_userValidation.ValidateAdress(inputUser.Adress));

        //Postkod
        do
        {
            Console.WriteLine("Please Enter your Postal-code.");
            inputUser.Postal = Console.ReadLine()!;
            if (!_userValidation.ValidatePostal(inputUser.Postal))
            {
                Console.WriteLine("Your postal-code should only include numbers.");
            }
        } while (!_userValidation.ValidatePostal(inputUser.Postal));

        //Ort
        do
        {
            Console.WriteLine("Please Enter your Locality.");
            inputUser.Locality = Console.ReadLine()!;
            if (!_userValidation.ValidateLocality(inputUser.Locality))
            {
                Console.WriteLine("Your locality must be atleast 2 letters long.");
            }
        } while (!_userValidation.ValidateLocality(inputUser.Locality));

        //Telefonnummer
        do
        {
            Console.WriteLine("Please Enter your Phone number.");
            inputUser.Phonenmbr = Console.ReadLine()!;
            if (!_userValidation.ValidatePhone(inputUser.Phonenmbr))
            {
                Console.WriteLine("Your Phonenumber must only contain digits.");
            }
        } while (!_userValidation.ValidatePhone(inputUser.Phonenmbr));

        _userService.Add(inputUser);
        
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
    public void EditUser() //redigera användare genomm att ange identiskt ID. därav max 5 täcken på id.
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
                if (_userValidation.ValidateName(input))
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
            Console.WriteLine("Enter a new Last name. Or leave blank if you wish for it to remain the same.");
            string input = Console.ReadLine()!;
            if (!string.IsNullOrEmpty(input))
            {
                if (_userValidation.ValidateName(input))
                {
                    user.LastName = input;
                    break;
                }
                else
                {
                    Console.WriteLine("Your Last name must be aleast two characters long.");
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
                if (_userValidation.ValidateEmail(input))
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
                if (_userValidation.ValidateAdress(input))
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
                if (_userValidation.ValidatePostal(input))
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
                if (_userValidation.ValidateLocality(input))
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
                if (_userValidation.ValidatePhone(input))
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
    public void DeleteUser() //Tar bor användare med hjälp av id. 
    {
        Console.WriteLine("Please enter the ID of the user you wish to delete.");
        string userId = Console.ReadLine()!;

        var user = _userService.GetUserById(userId); //tar in ett userId och väljer därefter rätt userobjekt. 
        if (user == null)
        {
            Console.WriteLine("User not found.");
        }
        else
        {
            Console.WriteLine($"Are you sure you want to delete user {user.UserId} {user.FirstName} {user.LastName}? y/n"); //dubbelkolla om man verkligen vill ta bort sagd användare , visar För och efter samt id så man är extra säker på att rätt användare valts.
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

