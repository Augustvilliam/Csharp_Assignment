using System.Diagnostics;
using System.Text.Json;
using Business.Interfaces;
using Business.Models;


namespace Business.Services;

public class fileService : IFileService
{
    private readonly string _directoryPath;
    private readonly string _filePath;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public fileService(string directoryPath = null, string fileName = "list.json")
    {
        _directoryPath = directoryPath ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyAppData"); //Kolla längst ner tack. 
        _filePath = Path.Combine(_directoryPath, fileName);
        _jsonSerializerOptions = new JsonSerializerOptions { WriteIndented = true };
    }

    public void SaveListToFile<T>(List<T> list)
    {
        try
        {
            if (!Directory.Exists(_directoryPath))
                Directory.CreateDirectory(_directoryPath);

            var json = JsonSerializer.Serialize(list, _jsonSerializerOptions);

            File.WriteAllText(_filePath, json);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);

        }
        Debug.WriteLine($"Saving to: {_filePath}");
    }
    public List<User> LoadList()
    {

        try
        {
            if (!File.Exists(_filePath))
                return new List<User>();

            var json = File.ReadAllText(_filePath);
            var list = JsonSerializer.Deserialize<List<User>>(json, _jsonSerializerOptions);
            Debug.WriteLine($"Loaded {_filePath} with {list.Count} users.");
            return list ?? new List<User>();
            

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new List<User>();

        }

    }
}
///Sparas i "My Documents" eftersom att mitt Code använder sin egna datamap i sys32 som defult sparplats. Detta Gav mig ungefär 7 dagar av fucking huvudvärk och nu ÄNTLIGEN tack vare en bra plaserad (TACK CHATGPT) debug.writeline visade det sig att jag inte hade behörighet att spara där. Jag har nu ett förakt för mänskligheten igen.