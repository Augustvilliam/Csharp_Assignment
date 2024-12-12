
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
        var user = new User { FirstName = "Villiam", LastName = "Fagrelius", Email = "villiam@example.com" };

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
            new User { FirstName = "Villiam", LastName = "Fagrelius", Email = "villiam@example.com"}

            };
        _fileserviceMock.Setup(fs => fs.LoadList()).Returns(users);
        //act
        var result = _userService.GetAll();
        //assert
        Assert.Equal(users, result);
    }

}
