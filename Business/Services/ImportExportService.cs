using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Services;

public class ImportExportService
{
    private readonly FileService _fileService;
    public ImportExportService(FileService fileService)
    {
        _fileService = fileService;
    }
    public void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Import/Export User Data:");
            Console.WriteLine("1. Export UserData");
            Console.WriteLine("2. Import UserData");
            Console.WriteLine("b. Back to Main Menu");
            Console.WriteLine("Please Choose an option.");

            var option = Console.ReadLine();
            switch (option!.ToLower())
            {
                case "1":
                    ExportUsers();
                    break;
                case "2":
                    ImportUsers();
                    break;
                case "b":
                    return;
                default:
                    Console.WriteLine("Please enter a valid option.");
                    Console.ReadKey();
                    break;

            }
        }
    }

    private void ExportUsers()
    {
        Console.Clear();
        Console.WriteLine("Exporting user data to text...");

        try
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, "users.txt");

            var users = new UserService().GetAll();
            var lines = users.Select(users => $"ID: {users.UserId}, Name: {users.FirstName} {users.LastName}, Email: {users.Email}");

            File.WriteAllLines(filePath, lines);

            Console.WriteLine($"Users Exported to {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while epoxting users, please try again.");
        }
        Console.WriteLine("press any key to return");
        Console.ReadKey();
    }


    private void ImportUsers()
    {
        Console.Clear();
        Console.WriteLine("Please enter the file name, including extention. (only .txt format");
        string fileName = Console.ReadLine()!;

        try
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, fileName);

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found. Make sure the File existst and that you include extentions.");
                Console.ReadKey();
                return;
            }
            var lines = File.ReadAllLines(filePath);
            var userService = new UserService();

            foreach (var line in lines)
            {
                var parts = line.Split(",");

                if (parts.Length == 3)
                {
                    User user = new()
                    {
                        FirstName = parts[0].Trim(),
                        LastName = parts[1].Trim(),
                        Email = parts[2].Trim(),
                    };
                    userService.Add(user);
                }
                else
                {
                    Console.WriteLine($"Invalid format in line: {line}. skipping");
                }
            }
            Console.WriteLine("User imported!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Something went wrong :(");
            Console.ReadKey();
        }
    }


}

//uppdelad från en hel "MenuService" 3 mindre services på rekomendation av ChatGPT.
