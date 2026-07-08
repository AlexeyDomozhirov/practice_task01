using Xunit;
using task04;

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
    public void Fighter_ShouldHaveCorrectStats()
    {
        ISpaceship fighter = new Fighter();
        Assert.Equal(100, fighter.Speed);
        Assert.True(fighter.FirePower < 100);
    }

    [Fact]
    public void Spaceships_ShouldImplementInterface()
    {
        ISpaceship ship1 = new Cruiser();
        ISpaceship ship2 = new Fighter();
        
        Assert.IsType<Cruiser>(ship1);
        Assert.IsType<Fighter>(ship2);
    }

    [Fact]
    public void MoveForward_ChangesPositionCorrectly()
    {
        void TestMoveForward(ISpaceship ship, double expectedX, double expectedY,
                             Func<ISpaceship, double> getX, Func<ISpaceship, double> getY)
        {
            ship.MoveForward();
            Assert.Equal(expectedX, getX(ship), Tolerance);
            Assert.Equal(expectedY, getY(ship), Tolerance);
        }
    
        TestMoveForward(new Cruiser(0, 0, 0), 50, 0,
                        s => ((Cruiser)s).X, s => ((Cruiser)s).Y);
    
        TestMoveForward(new Fighter(0, 0, 90), 0, 100,
                        s => ((Fighter)s).X, s => ((Fighter)s).Y);
    }
    
    [Fact]
    public void Rotate_ChangesAngleCorrectly()
    {
        void TestRotate(ISpaceship ship, Func<ISpaceship, double> getAngle)
        {
            ship.Rotate(80);
            Assert.Equal(90, getAngle(ship));
            ship.Rotate(-200);
            Assert.Equal(250, getAngle(ship));
        }
    
        TestRotate(new Cruiser(0, 0, 10), s => ((Cruiser)s).Angle);
        TestRotate(new Fighter(0, 0, 10), s => ((Fighter)s).Angle);
    }
    
    [Fact]
    public void Fire_DoesNotThrow()
    {
        void TestFire(ISpaceship ship)
        {
            var exception = Record.Exception(() => ship.Fire());
            Assert.Null(exception);
        }
    
        TestFire(new Cruiser());
        TestFire(new Fighter());
    }
}
