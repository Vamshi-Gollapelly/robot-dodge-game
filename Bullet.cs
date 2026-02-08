using SplashKitSDK;
using System.Collections.Generic;


// It Represents a bullet fired by the player. Moves toward the mouse and destroys robots on contact.

public class Bullet
{
    public double X { get; private set; }
    public double Y { get; private set; }

    private Vector2D Velocity;
    private const double SPEED = 8.0;
    private const int RADIUS = 5;

    
    // It Creates a bullet starting at the given position, heading toward the target point.
    
    public Bullet(double startX, double startY, Point2D target)
    {
        X = startX;
        Y = startY;

        Point2D from = new Point2D() { X = X, Y = Y };
        Vector2D direction = SplashKit.UnitVector(SplashKit.VectorPointToPoint(from, target));
        Velocity = SplashKit.VectorMultiply(direction, SPEED);
    }

    
    // It Moves the bullet based on its direction and speed.
   
    public void Update()
    {
        X += Velocity.X;
        Y += Velocity.Y;
    }

    
    // It Draws the bullet as a small red circle.
    
    public void Draw()
    {
        SplashKit.FillCircle(Color.Red, X, Y, RADIUS);
    }

   
    // It Returns true if the bullet has left the screen boundaries.
   
    public bool IsOffscreen(Window screen)
    {
        return X < 0 || X > screen.Width || Y < 0 || Y > screen.Height;
    }

   
    //It is a circular hitbox used to check for collisions with robots.
    
    public Circle CollisionCircle => SplashKit.CircleAt(X, Y, RADIUS);
}
