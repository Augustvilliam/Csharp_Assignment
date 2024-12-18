

using Business.Helper;
using Business.Models;

namespace Business.Tests.ServicesTest;

public class UserValidationTests
{
    [Fact]
    public void ValidateShouldReturnTrueForValidUser()
    {
        //arrange
        var validator = new UserValidator();
        new User
        {
            UserId = "",
            FirstName = "test",
            LastName = "test",
            Email = "test@test.test",
            Adress = "test",
            Postal = "31111",
            Locality = "test",
            Phonenmbr = "0000000000",
        };

        //Act
        var result = validator.ValidateUser(User);
        //assert
        Assert.True(result);
    }
    
    [Fact]
    public void ValidateShouldReturnFalseForInvalidEmail()
    {
        var validator = new UserValidator();
        var user = new User
        {
            Email = "test@test.test"
        };
        var result = validator.ValidateUser(user);

        Assert.False(result);
    }


    [Fact]
    public void ValidateShouldReturnFalseIfRequiredFieldsAreMissing()
    {
        var validator = new UserValidator();
        var User = new User
        {
            FirstName = "",
            LastName = "",
        };
        var result = validator.ValidateUser(User);
        Assert.False(result);
    }
}

