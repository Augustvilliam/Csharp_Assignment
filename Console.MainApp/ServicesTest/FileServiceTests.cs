
using System.ComponentModel.DataAnnotations;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Xunit;

namespace Business.Tests.ServicesTest;

public class FileServiceTests
{
    [Fact]
    public void SaveUsersToFile_ShouldCreateFileWithUsers()
    {
        //arrange
        var directroyPath = "testData";
        var fileName = "test_users.json";
        var fileService = new fileService(directroyPath, fileName);
        
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
        fileService.SaveListToFile(users);
        //assert
        var filePath = Path.Combine(directroyPath, fileName);
        Assert.True(File.Exists(filePath));
        Assert.Contains ("test", File.ReadAllText (filePath));
        //clean
        Directory.Delete (directroyPath, true);
    }

  
     
    [Fact]
    public void LoadUserFromFile_shouldLoadUsersCorrectly()
    {
        //arrange
        var directoryPath = "TestData";
        var fileName = "test_users.json";
        var fileService = new fileService (directoryPath, fileName);
        
        var users = new List<User>
         {
            new User
            {
                UserId = Guid.NewGuid().ToString(),
                FirstName = "test",
                LastName = "test",
                Email = "test@test.test",
                Adress = "test",
                Postal = "31111",
                Locality = "test",
                Phonenmbr = "0000000000",
            }

        };
        fileService.SaveListToFile(users);
        //act
        
        var loadedUsers = fileService.LoadList();

        //assert
        Assert.Single(loadedUsers);
        Assert.Equal("test", loadedUsers[0].FirstName);
       
        //clean
        Directory.Delete(directoryPath, true);
    }


    [Fact]
    public void LoadUsersFromFile_shouldHandleEmptyFile()
    {
        //arrange
        var directoryPath = "testData";
        var fileName = "empty.json";
        var filePath = Path.Combine(directoryPath, fileName);

        Directory.CreateDirectory(directoryPath);
        File.WriteAllText(filePath, string.Empty);

        var fileService = new fileService(directoryPath, fileName);
        //act

        var loadedUsers = fileService.LoadList();
        //assert

        Assert.Empty(loadedUsers);
        //clean

        Directory.Delete(directoryPath, true);
    }

}
