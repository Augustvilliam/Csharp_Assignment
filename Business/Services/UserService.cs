using Business.Interfaces;
using Business.Models;


namespace Business.Services;

public class UserService : IUserService
{
    private List<User> _users = new();
    private readonly IFileService _fileService;

    public UserService(IFileService fileService)
    {
        _fileService = fileService;
    }
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
