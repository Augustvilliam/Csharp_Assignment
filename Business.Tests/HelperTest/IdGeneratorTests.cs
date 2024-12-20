

using Business.Helper;

namespace Business.Tests.HelperTest;

public class IdGeneratorTests
{
    [Fact]
    public void GenerateShorIdShoudReturnTrimmedId()
    {
        //act
        var id = IdGenerator.GenerateShortId(5); //lätt att byta trimmat (5 täcken) till längre vid brist eller om det skulle uppstå dubbletter.
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

//Testar att id faktiskt trimmas.