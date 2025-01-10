

using Business.Helper;
using Business.Interfaces;
using Business.Models;

namespace Business.Tests.ServicesTest;

public class UserValidationTests
{
    private readonly IUserValidation _userValidation;

    public UserValidationTests()
    {
        _userValidation = new UserValidation();
    }




    [Fact]
    public void ValidateShouldReturnTrueForValidUser() //kollar så att användaren valideras.
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
        var result = _userValidation.ValidateUser(user, out var errorMessage);
        //assert
        Assert.True(result);
        Assert.Equal(string.Empty, errorMessage);
    }
    
    [Fact]
    public void ValidateShouldReturnFalseForInvalidEmail() //kollar så att användaren blir invalid ifall Email inte ska vara rätt 
    {
        //arrange
        var user = new User
        {
            FirstName = "test",
            LastName = "test",
            Email = "BrasseFEL.jpg", //FEL FEL FEL FEL
            Adress = "test",
            Locality= "test",
            Postal= "1111",
            Phonenmbr = "000000000"
            
        };
       //act
        var result = _userValidation.ValidateUser(user, out var errorMessage); //error när den ser att det är brasse som är i emailen och inte en emailadress
        //assert
        Assert.False(result);
        Assert.Equal("Invalid email format. Please enter a valid one (e.g., example@example.com).", errorMessage);
    }


    [Fact]
    public void ValidateShouldReturnFalseIfRequiredFieldsAreMissing() //terstar så den felar rätt om saker inte är ifylda.
    {
       
        var user = new User
        {
            FirstName = "",
            LastName = "",
        };
        var result = _userValidation.ValidateUser(user, out var errorMessage);
        Assert.False(result);
    }

    [Fact]
    public void ValidateShouldReturnFalseIfPhoneInvalid() //samma som ovan fast med telefonnummer och inte email.
    {
        //arrange
        var user = new User
        {
            FirstName = "test",
            LastName = "test",
            Email = "xx@xx.xx",
            Phonenmbr = "BrasseFel.jpg",
        };
        //act
        var result = _userValidation.ValidateUser(user, out var errorMessage);

        //assaert
        Assert.False(result);
        Assert.Equal("Phone number must only contain numbers.", errorMessage);
    }
    [Fact]
    public void ValidateShouldReturnFalseIfInvalidPostal()  // samma är fast med postkod 
    {
        //arrange
        var user = new User
        {
            FirstName = "test",
            LastName = "test",
            Email = "xx@xx.xx",
            Postal = "BrasseFEL.jpg",
            Phonenmbr = "909909090",
        };
        ///act
        var result = _userValidation.ValidateUser(user, out var errorMessage);
        //asset
        Assert.False(result);
        Assert.Equal("Postal code must only contain numbers.", errorMessage);
    }
    [Fact]
    public void ValidateShouldReturnFalseIfFieldEmpty() //Samma här fast med tomma fält.  
    {
        //assert
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
        var result = _userValidation.ValidateUser(user, out var errorMessage);
        //asset
        Assert.False(result);
        Assert.Equal("Address cannot be empty.", errorMessage);
    }
}

