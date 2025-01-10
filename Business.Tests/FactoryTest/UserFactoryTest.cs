

using Business.Factory;
using Business.Models;

namespace Business.Tests.FactoryTest;

public class UserFactoryTest
{
    private readonly UserFactory _userFactory; //testar att den faktiskt tar in användardata och slänger på ett id för att skapa en komplett användare. Grundstruktur Genererard av chatGPT

    public UserFactoryTest()
    {
        _userFactory = new UserFactory();
    }
    [Fact]
    public void CreateUserShouldReturnUserWithId()
    {

        //arrange
        var inputUser = new User
        {
            FirstName = "Test",
            LastName = "Test",
            Email = "ex@ex.ex",
            Adress = "test",
            Postal = "00000",
            Locality = "test",
            Phonenmbr = "00000000",
        };
        //act
        var createdUser = _userFactory.CreateUser(inputUser);

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
