
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Moq;


namespace Business.Tests.ServicesTest;

public class UserServiceTests
{

    private readonly Mock<IFileService> _fileServiceMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _fileServiceMock = new Mock<IFileService>();
        _userService = new UserService(_fileServiceMock.Object);
    }

    [Fact]
    public void AddUser_shouldAddUserToList()
    {
        //Arrange
        var user = new User
        {
            UserId = Guid.NewGuid().ToString(),
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
        _fileServiceMock.Verify(fs => fs.SaveListToFile(It.IsAny<List<User>>()), Times.Once);

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
        _fileServiceMock.Setup(fs => fs.LoadList()).Returns(users);
        //act
        var result = _userService.GetAll();
        //assert
        Assert.Equal(users, result);
    }

    [Fact]
    public void GetUserById_Should_Return_Correct_User()
    {
        //arrange
        var users = new List<User>
        {
            new User {UserId = "1", FirstName = "test"},
            new User {UserId ="2", FirstName = "test2"}

        };
        _fileServiceMock.Setup(fs => fs.LoadList()).Returns(users);
        //act
        var user = _userService.GetUserById("1");
        //assert
        Assert.NotNull(user);
        Assert.Equal("test", user.FirstName);
    }

    [Fact]
    public void DeleteUserShouldRemoveUserFromList()
    {
        //Arrange
        var user = new User { UserId = "1", FirstName = "test" };
        var users = new List<User> { user };
        _fileServiceMock.Setup(fs => fs.LoadList()).Returns(users);


        //act
        _userService.DeleteUser("1");
        //assert
        _fileServiceMock.Verify(fs => fs.SaveListToFile(It.Is<List<User>>(u => u.Count == 0)), Times.Once);
    }

    [Fact]
    public void AddUser_ShouldThrowExceptionIfUserIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => _userService.Add(null));
    }
    [Fact]
    public void EditUser_shouldTrowExceptionIfUserDoesNotExist()
    {
        var user = new User { UserId = "nonexistant" };
        Assert.Throws<InvalidOperationException>(() => _userService.EditUser("nonexistant", user));
    }


    [Fact]
    public void DeleteUser_ShouldNotThrowIfUserDOesNotExist()
    {
        var exception = Record.Exception(() => _userService.DeleteUser("nonexistant"));
        Assert.Null(exception);
    }

    [Fact]
    public void GetAll_ShouldReturnEmptyListIfNoUserSaved()
    {
        _fileServiceMock.Setup(fs => fs.LoadList()).Returns(new List<User>());
        var users = _userService.GetAll();
        Assert.NotNull(users);
        Assert.Empty(users);
    }

}
