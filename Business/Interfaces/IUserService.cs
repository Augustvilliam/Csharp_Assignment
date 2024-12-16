

using Business.Models;

namespace Business.Interfaces;

public interface IUserService
{
    void Add(User user);
    IEnumerable<User> GetAll();
    User GetUserById(string UserId);
    void EditUser(string UserId, User updateUser);
    void DeleteUser(string UserId);
}
