
using System.Text.Json;
using Business.Interfaces;
using Business.Models;

namespace Business.Services;

public class ImportExportService : IImportExportService //reultatet av en dampig hjärna som fick för sig "AtT DeTtA VaR En BrA IdE!! 
{

    private readonly IUserService _userService;
    private readonly IFileService _fileService;
    

    public ImportExportService(IUserService userService, IFileService fileService)
    {
        _userService = userService;
        _fileService = fileService;
    }

    public List<T> LoadListFromFile<T>(string fileName)
    {
        if (!File.Exists(fileName))
        {
            return new List<T>();
        }
        var json = File.ReadAllText(fileName); 
        return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
    } //tar in en jsonlista från en extern fil, oklart om den faktiskt funkar precis som den ska. 

    public void SaveListToFile<T>(List<T> list, string fileName)
    {
        var json = JsonSerializer.Serialize(list);
        File.WriteAllText(fileName, json);
    } //sparar ner den impoterade listan. 

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
    } //menyalternativ för servicen. 

    private void ExportUsers() // Skicka ut en Json fil till skrivbordet om det så önskar, varför kan man undra, jag undrar varför inte....
    {
        Console.Clear();
        Console.WriteLine("Exporting user data to text...");

        try
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, "users.txt");

            var users = _fileService.LoadList();
            if (users != null && users.Any())
            {
                var lines = users.Select(users => $"ID: {users.UserId}, Name: {users.FirstName} {users.LastName}, Email: {users.Email} Adress: {users.Adress} Postal: {users.Postal} Locality: {users.Locality} Phone Number: {users.Phonenmbr}");

                File.WriteAllLines(filePath, lines);

                Console.WriteLine($"Users Exported to {filePath}");
            }
            else
            {
                Console.WriteLine("No users found to export.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while exporting users: {ex.Message}");
        }
        Console.WriteLine("press any key to return");
        Console.ReadKey();
    }


    private void ImportUsers() // för att läsa in en EXTERN JSON fil från skrivbordet. Varför? Jag hade tråkigt. den är förövrigt fullkomligt väderlös om man inte copypastar en exakt rätt formaterad fil.
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
                    _userService.Add(user);

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
            Console.WriteLine($"Something went wrong: {ex.Message}");
            Console.ReadKey();
        }
    }


}

//uppdelad från en hel "MenuService" 3 mindre services på rekomendation av ChatGPT.
