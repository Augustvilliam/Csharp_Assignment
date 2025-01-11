using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;

namespace Business.Services;

public class MainMenuService //huvudmeny , leder till import och user menyn 
{
    private readonly UserManagementService _userManagmentService;
    private readonly IImportExportService _importExportService;
    public MainMenuService(UserManagementService userManagementService, IImportExportService importExportService)
    {
        _userManagmentService = userManagementService;
        _importExportService = importExportService;
    }
    public void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the user admin. Please choose one of the options!");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("1. User Menu");
            Console.WriteLine("2. Import/Export Menu");
            Console.WriteLine("q. Exist the Admin Tool");
            Console.WriteLine("-------------------------------------------");
            var option = Console.ReadLine();

            switch (option!.ToLower())
            {
                case "1":
                    _userManagmentService.ShowMenu();
                    break;
                case "2":
                    _importExportService.ShowMenu();
                    break;
                case "q":
                    Console.WriteLine("Okey, Goodbye!");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Please choose a valid option.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}

//uppdelad från en hel "MenuService" 3 mindre services på rekomendation av ChatGPT.


