
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Moq;


namespace Business.Tests.ServicesTest;

public class UserServiceTests
{

    private readonly Mock<IFileService> _fileserviceMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _fileserviceMock = new Mock<IFileService>();
        _userService = new UserService(_fileserviceMock.Object);
    }

    [Fact]
    public void AddUser_shouldAddUserToList()
    {
        //Arrange
        var user = new User 
        {
            UserId = Guid.NewGuid(),
            FirstName = "test",
            LastName = "test",
            Email = "test@test.test",
            Postal = "123",
            Adress = "test",
            Locality = "test",
            Phonenmbr = "123",

        };

        //Act
        _userService.Add(user);

        //Assert
        _fileserviceMock.Verify(fs => fs.SaveListToFile(It.IsAny<List<User>>()), Times.Once);

    }

    [Fact]
    public void GetAll_ShouldReturnUsers()
    {
        //arrange
        var users = new List<User>
            {
            new User
                { 
                    FirstName = "test",
                    LastName = "test",
                    Email = "test@test.test",
                    Postal = "123",
                    Adress = "test",
                    Locality = "test",
                    Phonenmbr = "123",

                }

            };
        _fileserviceMock.Setup(fs => fs.LoadList()).Returns(users);
        //act
        var result = _userService.GetAll();
        //assert
        Assert.Equal(users, result);
    }

    [Fact]
    public void GetUserById_Should_Return_Correct_User()
    {
        var userService = new UserService();
        var user = new User { UserId = Guid.NewGuid(), FirstName = "test" };
        userService.AddUser(user);

        var retrievedUser = userService.GetUserById(user.UserId);

        Assert.NotNull(retrievedUser);
        Assert.Equal("test", retrievedUser.FirstName);
    }

    [Fact]
    public void DeleteUser_Should_Remove_User_From_List()
    {
        //Arrange
        var userService = new UserService();
        var user = new User { UserId = Guid.NewGuid() };
        userService.AddUser(user);
        //act
        userService.DeleteUser(user.UserId);
        //assert
        Assert.Empty(userService.GetAllUsers());
    }
}
