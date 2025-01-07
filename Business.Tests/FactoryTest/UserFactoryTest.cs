

using Business.Factory;

namespace Business.Tests.FactoryTest;

public class UserFactoryTest
{
    private readonly UserFactory _userFactory;

    public UserFactoryTest()
    {
        _userFactory = new UserFactory();
    }
    [Fact]
    public void CreateDefultUser_ShouldReturnDefaultValueUser()
    {
        //act
        var user = _userFactory.CreateDefultUser();
        //assert
        Assert.NotNull(user);
        Assert.NotNull(user.UserId);
        Assert.Equal("sven", user.FirstName);
        Assert.Equal("Svensson", user.LastName);
        Assert.Equal("ex@ex.ex", user.Email);
        Assert.Equal("gatan", user.Adress);
        Assert.Equal("00000", user.Postal);
        Assert.Equal("stad", user.Locality);
        Assert.Equal("0000000000", user.Phonenmbr);
    }

}
