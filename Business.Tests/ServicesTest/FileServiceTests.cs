
using Business.Models;
using Business.Services;

namespace Business.Tests.ServicesTest;

public class FileServiceTests
{
    [Fact]
    public void SaveUsersToFile_ShouldCreateFileWithUsers()
    {
        //arrange

        var filePath = "test_users.json";
        var fileService = new FileService();
        var users = new List<User>
        {
            new User
            {
                UserId = "",
                FirstName = "test",
                LastName = "test",
                Email = "test@test.test",
                Adress = "test",
                Postal = "31111",
                Locality = "test",
                Phonenmbr = "0000000000",
            }

        };
        //act
        fileService.SaveListToFile(users, filePath);
        //assert
        Assert.True(File.Exists(filePath));
        Assert.Contains ("Test", File.ReadAllText (filePath));
        //clean
        File.Delete(filePath);
    }

  
     
    [Fact]
    public void LoadUserFromFile_shouldLoadUsersCorrectly()
    {
        //arrange
        var filePath = "test_users.json";
        var fileService = new FileService();
        var users = new List<User>
         {
            new User
            {
                UserId = "",
                FirstName = "test",
                LastName = "test",
                Email = "test@test.test",
                Adress = "test",
                Postal = "31111",
                Locality = "test",
                Phonenmbr = "0000000000",
            }

        };
        //act
        fileService.SaveListToFile(users, filePath);

        var loadedUsers = fileService.LoadList(filePath);
        //assert
        Assert.Single(loadedUsers);
        Assert.Equal("test", loadedUsers[0].FirstName);
        //clean
        File.Delete(filePath);
    }


    [Fact]
    public void LoadUsersFromFile_shouldHandleEmptyFile()
    {
        //arrange

        var filePath = "empty.json";
        File.WriteAllText(filePath, string.Empty);
        var fileService = new FileService();
        //act

        var loadedUsers = fileService.LoadList(filePath);
        //assert

        Assert.Empty(loadedUsers);
        //clean

        File.Delete(filePath);
    }

}
