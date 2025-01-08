

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
    private readonly UserManagementService _userManagementService;
    private readonly Mock<UserService> _userService;


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

    /* [Fact] Crashar för xunit inte pallar med Console.readline eller Conosle.Clear
     public void EditUserShouldUpdateUserWhenUserExists()
     {
         //Arrange
         var existingUser = new User
         {
             UserId = "1",
             FirstName = "firstName",
             LastName = "lastName",
             Email = "email",
             Adress = "adress",
             Postal = "99999",
             Locality = "locality",
             Phonenmbr = "0000001"
         };
         var updatedUser = new User
         {
             UserId = "1",
             FirstName = "firstNameE",
             LastName = "lastNameE",
             Email = "emailE",
             Adress = "adressE",
             Postal = "10000",
             Locality = "localityY",
             Phonenmbr = "0000002"
         };

         _mockUserService.Setup(s => s.GetUserById(existingUser.UserId)).Returns(existingUser);

         using var stringReader = new StringReader(existingUser.UserId + "\nNewName\nNewLastName\nnew@example.com\nNew Street\n54321\nNewCity\n0987654321\n");
         Console.SetIn(stringReader);
         //act
         _userManagementService.EditUser();

         _mockUserService.Verify(s => s.EditUser(existingUser.UserId, It.Is<User>(u =>
             u.FirstName == updatedUser.FirstName &&
             u.LastName == updatedUser.LastName &&
             u.Email == updatedUser.Email &&
             u.Adress == updatedUser.Adress &&
             u.Postal == updatedUser.Postal &&
             u.Locality == updatedUser.Locality &&
             u.Phonenmbr == updatedUser.Phonenmbr
         )), Times.Once);
     }
    */

    /*
    [Fact] Crashar för xunit inte pallar med Console.readline eller Conosle.Clear
    public void DeleteUserShouldRemoveUser()
    {
        //arrange
        var userToDelete = new User
        {
            UserId = "1",
            FirstName = "Sven",
            LastName = "Svensson"
        };

        _mockUserService.Setup(s => s.GetUserById(userToDelete.UserId)).Returns(userToDelete);

        using var stringReader = new StringReader(userToDelete.UserId + "\ny\n");
        Console.SetIn(stringReader);
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        //act
        _userManagementService.DeleteUser();
       //assert
       _mockUserService.Verify(s => s.DeleteUser(userToDelete.UserId), Times.Once);

        var output = stringWriter.ToString();
        Assert.Contains("Please enter the ID of the user you wish to delete.", output);
        Assert.Contains($"Are you sure you want to delete user {userToDelete.UserId} {userToDelete.FirstName} {userToDelete.LastName}? y/n", output);
        Assert.Contains("User Deleted Successfully.", output);
    }

    [Fact]
    public void DeleteUserShouldNotRemoveUserIfUserIsNotFound()
    {
        //arrange
        const string nonExistingId = "666";
        _mockUserService.Setup(s => s.GetUserById(nonExistingId)).Returns((User)null);

        using var stringReader = new StringReader(nonExistingId + "\n");
        Console.SetIn(stringReader);
        //act
        _userManagementService.DeleteUser();
        //assert
        _mockUserService.Verify(s => s.DeleteUser(It.IsAny<string>()), Times.Never);

    }
*/
}
