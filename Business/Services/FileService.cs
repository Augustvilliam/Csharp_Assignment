using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Services;

internal class FileService
{
    private readonly string _filePath = "users.json";

    internal List<User> _users = [];

    internal void Add(User user)
        {
        _users.Add(user);
        SaveToFile();
        }
    internal IEnumerable<User> GetAll()
    {
        return _users;
    }

    internal void SaveToFile()
    {
        var json = JsonSerializer.Serialize(_users);
        File.WriteAllText(_filePath, json);
    }

    internal void LoadFromFile()
    {
        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);
            _users = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }
    }
}
