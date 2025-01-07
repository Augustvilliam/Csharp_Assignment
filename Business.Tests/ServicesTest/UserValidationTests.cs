

using Business.Helper;
using Business.Models;

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
        //arrange
        var user = new User
        {
            FirstName = "test",
            LastName = "test",
            Email = "BasseNEJ.jpg",
            Adress = "test",
            Locality= "test",
            Postal= "1111",
            Phonenmbr = "000000000"
            
        };
       //act
        var result = UserValidation.ValidateUser(user, out var errorMessage);
        //assert
        Assert.False(result);
        Assert.Equal("Invalid email format. Please enter a valid one (e.g., example@example.com).", errorMessage);
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

    [Fact]
    public void ValidateShouldReturnFalseIfPhoneInvalid()
    {
        //arrange
        var user = new User
        {
            FirstName = "test",
            LastName = "test",
            Email = "xx@xx.xx",
            Phonenmbr = "BasseFEL.jpg",
        };
        //act
        var result = UserValidation.ValidateUser(user, out var errorMessage);

        //assaert
        Assert.False(result);
        Assert.Equal("Phone number must only contain numbers.", errorMessage);
    }
    [Fact]
    public void ValidateShouldReturnFalseIfInvalidPostal()
    {
        var user = new User
        {
            FirstName = "test",
            LastName = "test",
            Email = "xx@xx.xx",
            Postal = "BasseFEL.jpg",
            Phonenmbr = "909909090",
        };
        ///act
        var result = UserValidation.ValidateUser(user, out var errorMessage);
        //asset
        Assert.False(result);
        Assert.Equal("Postal code must only contain numbers.", errorMessage);
    }
    [Fact]
    public void ValidateShouldReturnFalseIfFieldEmpty()
    {
        var user = new User
        {
            FirstName = "test",
            LastName = "test",
            Email = "xx@xx.xx",
            Adress = "",
            Phonenmbr = "0909909",
            Postal = "0909",
        };
        ///act
        var result = UserValidation.ValidateUser(user, out var errorMessage);
        //asset
        Assert.False(result);
        Assert.Equal("Address cannot be empty.", errorMessage);
    }
}

