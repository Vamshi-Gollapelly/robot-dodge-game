using SplashKitSDK;

// Abstract base class for all robot types.
// It  Handles position, movement, and collision logic.

public abstract class Robot
{
    public double X { get; protected set; }
    public double Y { get; protected set; }

    protected Vector2D Velocity { get; set; }
    public Color MainColor { get; protected set; }

    public int Width => 50;
    public int Height => 50;

    public Robot(Window gameWindow, Player player)
    {
        MainColor = Color.RandomRGB(200);

        if (SplashKit.Rnd() < 0.5)
        {
            X = (SplashKit.Rnd() < 0.5) ? -Width : gameWindow.Width;
            Y = SplashKit.Rnd(gameWindow.Height);
        }
        else
        {
            X = SplashKit.Rnd(gameWindow.Width);
            Y = (SplashKit.Rnd() < 0.5) ? -Height : gameWindow.Height;
        }

        Point2D fromPt = new Point2D() { X = X, Y = Y };
        Point2D toPt = new Point2D() { X = player.X, Y = player.Y };
        Vector2D dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt, toPt));
        Velocity = SplashKit.VectorMultiply(dir, 3.5f);
    }

    public void Update()
    {
        X += Velocity.X;
        Y += Velocity.Y;
    }

    public bool IsOffscreen(Window screen)
    {
        return X < -Width || X > screen.Width || Y < -Height || Y > screen.Height;
    }

    public Circle CollisionCircle => SplashKit.CircleAt(X + Width / 2, Y + Height / 2, 20);

    public abstract void Draw();
}


// Boxy: a square-shaped robot with eyes and a mouth.

public class Boxy : Robot
{
    public Boxy(Window gameWindow, Player player) : base(gameWindow, player) { }

    public override void Draw()
    {
        double leftX = X + 12;
        double rightX = X + 27;
        double eyeY = Y + 10;
        double mouthY = Y + 30;

        SplashKit.FillRectangle(Color.Gray, X, Y, Width, Height);
        SplashKit.FillRectangle(MainColor, leftX, eyeY, 10, 10);
        SplashKit.FillRectangle(MainColor, rightX, eyeY, 10, 10);
        SplashKit.FillRectangle(MainColor, leftX, mouthY, 25, 10);
        SplashKit.FillRectangle(MainColor, leftX + 2, mouthY + 2, 21, 6);
    }
}


// Roundy: a round robot with a smile and expressive eyes.

public class Roundy : Robot
{
    public Roundy(Window gameWindow, Player player) : base(gameWindow, player) { }

    public override void Draw()
    {
        double leftX, midX, rightX;
        double midY, eyeY, mouthY;

        leftX = X + 17;
        midX = X + 25;
        rightX = X + 33;
        midY = Y + 25;
        eyeY = Y + 20;
        mouthY = Y + 35;

        SplashKit.FillCircle(Color.White, midX, midY, 25);
        SplashKit.DrawCircle(Color.Gray, midX, midY, 25);
        SplashKit.FillCircle(MainColor, leftX, eyeY, 5);
        SplashKit.FillCircle(MainColor, rightX, eyeY, 5);
        SplashKit.FillEllipse(Color.Gray, X, eyeY, 50, 30);
        SplashKit.DrawLine(Color.Black, X, mouthY, X + 50, Y + 35);
    }
}
