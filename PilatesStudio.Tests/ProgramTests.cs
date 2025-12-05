namespace PilatesStudio.Tests;

public class ProgramTests
{
    [Fact]
    public void Program_ClassExists()
    {
        // Arrange & Act
        var programType = typeof(Program);

        // Assert
        Assert.NotNull(programType);
        Assert.Equal("Program", programType.Name);
    }
}