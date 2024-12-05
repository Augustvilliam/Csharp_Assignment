using Business.Models;


namespace Business.Services;

public class UserService
{
    private List<User> _users = new();
    private readonly FileService _fileService = new();



    public void Add(User user)
    {
        _users.Add(user);
        _fileService.SaveListToFile(_users);
    }
    public IEnumerable<User> GetAll() 
    {
        _users = _fileService.LoadList() ?? new List<User>();
        return _users; 
    }
}
