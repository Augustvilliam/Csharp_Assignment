

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
    private readonly UserManagementService _userManagementService;


    public UserManagementServiceTests()
    {
        _mockUserService = new Mock<IUserService>();
        _mockUserFactory = new Mock<IUserFactory>();
        _userManagementService = new UserManagementService(_mockUserService.Object, _mockUserFactory.Object);
        
    }
  
    
    [Fact]
     public void CreateUserShouldReturnValidUser()
    {
        //arrange 
        string firstName = "Sven";
        string lastName = "Svensson";
        string email = "ex@ex.ex";
        string adress = "gatan";
        string postal = "00000";
        string locality = "stad";
        string phonenmbr = "0000000000";

        var expectedUser = new User
        {
            UserId = Guid.NewGuid().ToString(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Adress = adress,
            Postal = postal,
            Locality = locality,
            Phonenmbr = phonenmbr
        };


        _mockUserFactory.Setup(f => f.CreateUser(firstName, lastName, email, adress, postal, locality, phonenmbr))
            .Returns(expectedUser);

        // act
        var user = _mockUserFactory.Object.CreateUser(firstName, lastName, email, adress, postal, locality, phonenmbr);
        //assert
        Assert.NotNull(user);
        Assert.Equal(firstName, user.FirstName);
        Assert.Equal(lastName, user.LastName);  
        Assert.Equal(email, user.Email);    
        Assert.Equal(adress, user.Adress);
        Assert.Equal(postal, user.Postal);
        Assert.Equal(locality, user.Locality);
        Assert.Equal(phonenmbr, user.Phonenmbr);
    }

}
