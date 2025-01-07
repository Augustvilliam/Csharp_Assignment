

using Business.Factory;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Moq;

namespace Business.Tests.ServicesTest;

public class UserManagementServiceTests
{
    
    private readonly UserFactory _userFactory;
    private readonly Mock<IUserService> _mockUserService;
    private readonly Mock<IUserFactory> _mockUserFactory;
    private readonly UserManagementService _userManagementService;


    public UserManagementServiceTests()
    {
        _userFactory = new UserFactory();
        _mockUserService = new Mock<IUserService>();
        _userManagementService = new UserManagementService(_mockUserService.Object, _mockUserFactory.Object);
        _mockUserFactory = new Mock<IUserFactory>();
    }
    [Fact]
    public void ShowMenuShouldHandleInvalidOption()
    {
        using var stringReader = new StringReader("invalid\nb\n");
        Console.SetIn(stringReader);
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        _userManagementService.ShowMenu();

        var output = stringWriter.ToString();
        Assert.Contains("Please choose a valid option.", output);
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

        // act
        var user = _userFactory.CreateUser(firstName, lastName, email, adress, postal, locality, phonenmbr);
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
    [Fact]

    public void ViewAllUsersShouldDisplayAllUsers()
    {
        // Arrange
        var users = new List<User>
    {
        new User {UserId = "1", FirstName = "Sven", LastName = "Svensson"},
        new User {UserId = "2", FirstName = "Anders", LastName = "Andersson"}
    };
        _mockUserService.Setup(s => s.GetAll()).Returns(users);

        using var stringReader = new StringReader("2\nb\n"); // Menyval 2 = Visa alla användare, b = tillbaka
        Console.SetIn(stringReader);

        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        // Act
        _userManagementService.ShowMenu();

        // Assert
        var output = stringWriter.ToString();
        Assert.Contains("Sven Svensson", output);
        Assert.Contains("Anders Andersson", output);
    }

}
