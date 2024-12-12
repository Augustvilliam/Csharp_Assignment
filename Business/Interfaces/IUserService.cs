

using Business.Models;

namespace Business.Interfaces;

public interface IUserService
{
    void Add(User user);
    IEnumerable<User> GetAll();
}
