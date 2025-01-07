

using Business.Helper;
using Business.Models;
using Xunit;

namespace Business.Tests.ServicesTest;

public class UserValidationTests
{
    [Fact]
    public void ValidateShouldReturnTrueForValidUser()
    {
        //arrange
        var user = new User
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
        var result = UserValidation.ValidateUser(user, out var errorMessage);
        //assert
        Assert.True(result);
        Assert.Equal(string.Empty, errorMessage);
    }
    
    [Fact]
    public void ValidateShouldReturnFalseForInvalidEmail()
    {
        
        var user = new User
        {
            Email = "test@test.test"
        };
        var result = UserValidation.ValidateUser(user, out var errorMessage);

        Assert.False(result);
    }


    [Fact]
    public void ValidateShouldReturnFalseIfRequiredFieldsAreMissing()
    {
       
        var user = new User
        {
            FirstName = "",
            LastName = "",
        };
        var result = UserValidation.ValidateUser(user, out var errorMessage);
        Assert.False(result);
    }
}

