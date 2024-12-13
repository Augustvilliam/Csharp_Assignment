
using System.Numerics;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Business.Models;
namespace Business.Helper;

public static class UserValidation
{
    public static bool ValidateName(string name)
    {
        return !string.IsNullOrWhiteSpace(name) && name.Length >= 2; //måste innehålla 2 tecken
    }
    public static bool ValidateEmail(string email)
    { 
        return Regex.IsMatch(email, "^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$"); //följa enkelt format av x@x.xx
    }
    public static bool ValidatePhone(string phone)
    {
        return Regex.IsMatch(phone, "^\\d+$"); //endast siffror
    }
    public static bool ValidatePostal(string postal)
    {
        return Regex.IsMatch(postal, "^\\d+$"); //endast siffror
    }
    public static bool ValidateAdress(string adress)
    {
        return !string.IsNullOrWhiteSpace(adress);//Ej vara tom
    }
    public static bool ValidateLocality(string locality)
    {
        return !string.IsNullOrWhiteSpace(locality); //Ej vara tom 
    }

    public static bool ValidateUser(User user, out string errorMessage)
    {
        if(!ValidateName(user.FirstName))
        {
            errorMessage = "First name must containt atleast 2 characters.";
            return false;
        }
        if (!ValidateName(user.LastName))
        {
            errorMessage = "Last name must containt atleast 2 characters.";
            return false;
        }
        if (!ValidateName(user.Email))
        {
            errorMessage = "Your Email is invalid. Please enter a valid one. E.g (Exmaple@Example.ex) .";
            return false;
        }
        if (!ValidateName(user.Phonenmbr))
        {
            errorMessage = "Phone Number must only contain numbers";
            return false;
        }
        if (!ValidateName(user.Postal))
        {
            errorMessage = "Postal-Code must only contain numbers.";
            return false;
        }
        if (!ValidateName(user.Adress))
        {
            errorMessage = "Your adress cannot be empty";
            return false;
        }
        if (!ValidateName(user.Locality))
        {
            errorMessage = "Your Locality cannot be empty";
            return false;
        }

        errorMessage =string.Empty;
        return true;

    }
}
