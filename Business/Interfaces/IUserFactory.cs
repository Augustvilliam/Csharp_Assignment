

using Business.Models;

namespace Business.Interfaces;

public interface IUserFactory
{
    User CreateUser(User inputUser);
}
