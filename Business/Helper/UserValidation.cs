
using System.Numerics;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Business.Interfaces;
using Business.Models;
namespace Business.Helper;

public  class UserValidation : IUserValidation
{
    public  bool ValidateName(string name)
    {
        return !string.IsNullOrWhiteSpace(name) && name.Length >= 2; //måste innehålla 2 tecken
    }
    public  bool ValidateEmail(string email)
    { 
        if (string.IsNullOrWhiteSpace(email)) return false;
        return Regex.IsMatch(email, "^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$"); //följa enkelt format av x@x.xx
    }
    public  bool ValidatePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone)) return false;
        return Regex.IsMatch(phone, "^\\d+$"); //endast siffror
    }
    public  bool ValidatePostal(string postal)
    {
        if (string.IsNullOrWhiteSpace(postal)) return false;
        return Regex.IsMatch(postal, "^\\d+$"); //endast siffror
    }
    public  bool ValidateAdress(string adress)
    {
        return !string.IsNullOrWhiteSpace(adress);//Ej vara tom
    }
    public bool ValidateLocality(string locality)
    {
        return !string.IsNullOrWhiteSpace(locality); //Ej vara tom 
    }

    public bool ValidateUser(User user, out string errorMessage)
    {
        if (!ValidateName(user.FirstName))
        {
            errorMessage = "First name must contain at least 2 characters.";
            return false;
        }
        if (!ValidateName(user.LastName))
        {
            errorMessage = "Last name must contain at least 2 characters.";
            return false;
        }
        if (!ValidateEmail(user.Email))
        {
            errorMessage = "Invalid email format. Please enter a valid one (e.g., example@example.com).";
            return false;
        }
        if (!ValidatePhone(user.Phonenmbr))
        {
            errorMessage = "Phone number must only contain numbers.";
            return false;
        }
        if (!ValidatePostal(user.Postal))
        {
            errorMessage = "Postal code must only contain numbers.";
            return false;
        }
        if (!ValidateAdress(user.Adress))
        {
            errorMessage = "Address cannot be empty.";
            return false;
        }
        if (!ValidateLocality(user.Locality))
        {
            errorMessage = "Locality cannot be empty.";
            return false;
        }

        errorMessage = string.Empty;
        return true;
    }

}
