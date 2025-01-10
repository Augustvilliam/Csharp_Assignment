using Business.Interfaces;
using Business.Models;


namespace Business.Services;

public class UserService : IUserService //userService. Här hanteras saker som add, lägga till i listan, hämta upp en User via I, edit och delete 
{
    private List<User> _users = new();
    private readonly IFileService _fileService;

    public UserService(IFileService fileService)
    {
        _fileService = fileService;
    }
    public void Add(User user)
    {
        if (user == null) 
            throw new ArgumentNullException(nameof(user), "User Cannot Be Null");

        _users.Add(user); 
        _fileService.SaveListToFile(_users);
    }
    public IEnumerable<User> GetAll() 
    {
        _users = _fileService.LoadList() ?? new List<User>();
        return _users; 
    }

    public User GetUserById(string id)
    {
        _users = _fileService.LoadList() ?? new List<User>();
        return _users.FirstOrDefault(u => u.UserId == id);
    }

    public void EditUser(string id, User updateUser)
    {
        if (updateUser == null)
            throw new ArgumentNullException(nameof (updateUser), "Updated User Cannot Be Null");
       
        var user = GetUserById(id);
        if (user == null)
            throw new InvalidOperationException($"User with ID {id} Does not exist.");
        
        user.FirstName = updateUser.FirstName;
        user.LastName = updateUser.LastName;
        user.Email = updateUser.Email;
        user.Adress = updateUser.Adress;
        user.Postal = updateUser.Postal;
        user.Locality = updateUser.Locality;
        user.Phonenmbr = updateUser.Phonenmbr;

        _fileService.SaveListToFile(_users);
        
    }

    public void DeleteUser(string id)
    {
        _users = _fileService.LoadList() ?? new List<User>();
        var user = _users.FirstOrDefault(u => u.UserId == id);
        if (user != null )
        {
            _users.Remove(user);  
            _fileService.SaveListToFile(_users);
        }
    }
}
