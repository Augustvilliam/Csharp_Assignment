

using Business.Models;

namespace Business.Interfaces;

public interface IUserFactory
{
    User CreateDefultUser();
    User CreateUser(string firstName, string lastName, string email, string adress, string postal, string locality, string phonenmbr);
}
