
using System.Text.Json;
using Business.Models;
using Business.Services;
using Business.Interfaces;
using Moq;
using Xunit;

namespace Business.Tests.ServicesTest;

public class ImportExportServiceTests 
{
    private readonly IImportExportService _importExportService;
    private readonly string _testFileName = "test.json";

    public ImportExportServiceTests()
    {
        var mockUserService = new Mock<IUserService>();
        var mockFileService = new Mock<IFileService>();

        // Ge mockade beroenden till ImportExportService
        _importExportService = new ImportExportService(mockUserService.Object, mockFileService.Object);
    }

    [Fact]
    public void SaveListToFile_ShouldCreateJsonFileWithCorrectContent()
    {
        //arrange
        var testList = new List<User>
        {
            new User { UserId = Guid.NewGuid().ToString(), FirstName = "Villiam", LastName ="Fagrelius", Email ="Villiam@example.se" }
        };
        
        //act
        _importExportService.SaveListToFile(testList,  _testFileName);
       
        //assert
        Assert.True(File.Exists(_testFileName));
        var json = File.ReadAllText(_testFileName);
        var deserializedList = JsonSerializer.Deserialize<List<User>>(json);   
        Assert.NotNull(deserializedList);
        Assert.Single(deserializedList);
        Assert.Equal("Villiam", deserializedList[0].FirstName);

        //Clean
        File.Delete(_testFileName);

    }
    [Fact]
    public void LoadListFromFile_ShouldReturnDeserializedList_whenFileExists()
    {
        //arrange
        var testList = new List<User>
        {
            new User { UserId = Guid.NewGuid().ToString(), FirstName = "Villiam", LastName ="Fagrelius", Email ="Villiam@example.se" }
        };
        var json = JsonSerializer.Serialize(testList);
        File.WriteAllText(_testFileName, json);
        
        //act
        var result = _importExportService.LoadListFromFile<User>(_testFileName);
        
        //assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Villiam", result[0].FirstName);

        //clean
        File.Delete(_testFileName);
    }

    [Fact]
    public void LoadListFromFile_ShouldReturnEmpty_WhenFileDoesNotExist()
    {

        //Arrange
        var nonExistantFileName = "nonexistent.json";

        //Act
        var result = _importExportService.LoadListFromFile<User>(nonExistantFileName);

        //Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        //detta test genererat med ChatGPT
    }


}
