using Business.Models;


namespace Business.Services;

public class UserService
{
    internal List<User> _users = [];

    internal void Add(User user)
    {
        _users.Add(user);
    }
    internal IEnumerable<User> GetAll() 
    { 
        return _users; 
    }
}
