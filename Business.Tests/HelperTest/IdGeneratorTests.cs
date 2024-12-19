

using Business.Helper;

namespace Business.Tests.HelperTest;

public class IdGeneratorTests
{
    [Fact]
    public void GenerateShorIdShoudReturnTrimmedId()
    {
        //act
        var id = IdGenerator.GenerateShortId(5);
        //assert
        Assert.NotNull(id);
        Assert.Equal(5, id.Length);
    }

    [Fact]
    public void GenerateShortIdShouldThrowExWhenLengthIsOutOfBound()
    {
        Assert.Throws<ArgumentException>(() => IdGenerator.GenerateShortId(0));
        Assert.Throws<ArgumentException>(() => IdGenerator.GenerateShortId(33));
    }
}
