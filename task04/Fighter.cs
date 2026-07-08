namespace task04;

public class Fighter : ISpaceship
{
    public int Speed => 100;
    public int FirePower => 30;

    public double X { get; private set; }
    public double Y { get; private set; }
    public int Angle { get; private set; }

    public Fighter(double startX = 0, double startY = 0, int startAngle = 0)
    {
        X = startX;
        Y = startY;
        Angle = startAngle % 360;
        if (Angle < 0) Angle += 360;
    }

    public void MoveForward()
    {
        double rad = Angle * Math.PI / 180.0;
        X += Speed * Math.Cos(rad);
        Y += Speed * Math.Sin(rad);
    }

    public void Rotate(int deltaAngle)
    {
        Angle = (Angle + deltaAngle) % 360;
        if (Angle < 0) Angle += 360;
    }

    public void Fire() { }
}
