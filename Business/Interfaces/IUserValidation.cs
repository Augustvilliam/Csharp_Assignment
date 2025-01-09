

using Business.Models;

namespace Business.Interfaces;

public interface IUserValidation
{
    bool ValidateName(string name);
    bool ValidateEmail(string email);
    bool ValidateAdress(string adress);
    bool ValidatePostal(string postal);
    bool ValidateLocality(string locality); 
    bool ValidatePhone(string phone);
    bool ValidateUser(User user, out string errorMessage);
}
