using Xunit;

public class SpaceshipTests
{
    private const double Tolerance = 1e-9;

    [Fact]
    public void Cruiser_ShouldHaveCorrectStats()
    {
        ISpaceship cruiser = new Cruiser();
        Assert.Equal(50, cruiser.Speed);
        Assert.Equal(100, cruiser.FirePower);
    }

    [Fact]
    public void Fighter_ShouldBeFasterThanCruiser()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        Assert.True(fighter.Speed > cruiser.Speed);
    }

    [Fact]
    public void MoveForward_ChangesPositionCorrectly()
    {
        var cruiser = new Cruiser(0, 0, 0);
        cruiser.MoveForward();
        Assert.Equal(50, cruiser.X, Tolerance);
        Assert.Equal(0, cruiser.Y, Tolerance);

        var fighter = new Fighter(0, 0, 90);
        fighter.MoveForward();
        Assert.Equal(0, fighter.X, Tolerance);
        Assert.Equal(100, fighter.Y, Tolerance);
    }

    [Fact]
    public void Rotate_ChangesAngleCorrectly()
    {
        var ship = new Cruiser(0, 0, 10);
        ship.Rotate(80);
        Assert.Equal(90, ship.Angle);
        ship.Rotate(-200);
        Assert.Equal(250, ship.Angle);
    }

    [Fact]
    public void Fire_DoesNotThrow()
    {
        var cruiser = new Cruiser();
        var exception = Record.Exception(() => cruiser.Fire());
        Assert.Null(exception);
    }

    [Fact]
    public void MoveRotateMove_UpdatesPositionCorrectly()
    {
        var cruiser = new Cruiser(0, 0, 0);
        cruiser.MoveForward();
        cruiser.Rotate(90);
        cruiser.MoveForward();

        Assert.Equal(50, cruiser.X, Tolerance);
        Assert.Equal(50, cruiser.Y, Tolerance);
        Assert.Equal(90, cruiser.Angle);
    }
}
