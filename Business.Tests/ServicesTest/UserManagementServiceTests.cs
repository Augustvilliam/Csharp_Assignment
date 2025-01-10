

using System.IO;
using Business.Factory;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Moq;

namespace Business.Tests.ServicesTest;

public class UserManagementServiceTests
{
    private readonly Mock<IUserService> _mockUserService;
    private readonly Mock<IUserFactory> _mockUserFactory;
    private readonly Mock<IUserValidation> _mockUserValidation;
    private readonly UserManagementService _userManagementService;
    private readonly Mock<UserService> _userService;


    public UserManagementServiceTests()
    {
        _mockUserService = new Mock<IUserService>();
        _mockUserFactory = new Mock<IUserFactory>();
        _mockUserValidation = new Mock<IUserValidation>();
        _userManagementService = new UserManagementService(_mockUserService.Object, _mockUserFactory.Object, _mockUserValidation.Object);
        
        
    }
  
    
    [Fact]
     public void CreateUserShouldReturnValidUser() //kollar så att CreateUser gör ett korrekt objekt med datan den får in. Grunden gjort med chatGPT
    {
        //arrange 
        var inputUser = new User
        {
            FirstName = "Sven",
            LastName = "Svensson",
            Email = "ex@ex.ex",
            Adress = "gatan",
            Postal = "00000",
            Locality = "stad",
            Phonenmbr = "0000000000",
        };

        var expectedUser = new User
        {
            UserId = Guid.NewGuid().ToString(),
            FirstName = inputUser.FirstName,
            LastName = inputUser.LastName,
            Email = inputUser.Email,
            Adress = inputUser.Adress,
            Postal = inputUser.Postal,
            Locality = inputUser.Locality,
            Phonenmbr = inputUser.Phonenmbr
        };


        _mockUserFactory.Setup(f => f.CreateUser(It.Is<User>(u =>
            u.FirstName == inputUser.FirstName &&
            u.LastName == inputUser.LastName &&
            u.Email == inputUser.Email)))
            .Returns(expectedUser);

        // act
        var createdUser = _mockUserFactory.Object.CreateUser(inputUser);
        //assert
        Assert.NotNull(createdUser);
        Assert.NotNull(createdUser.UserId);
        Assert.Equal(inputUser.FirstName, createdUser.FirstName);
        Assert.Equal(inputUser.LastName, createdUser.LastName);  
        Assert.Equal(inputUser.Email, createdUser.Email);    
        Assert.Equal(inputUser.Adress, createdUser.Adress);
        Assert.Equal(inputUser.Postal, createdUser.Postal);
        Assert.Equal(inputUser.Locality, createdUser.Locality);
        Assert.Equal(inputUser.Phonenmbr, createdUser.Phonenmbr);
    }

}
