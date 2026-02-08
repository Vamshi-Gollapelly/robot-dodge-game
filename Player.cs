using SplashKitSDK;


// It Represents the player in the Robot Dodge game.

public class Player
{
    private Bitmap _PlayerBitmap;           // It holds Player image
    private const int SPEED = 5;            // It holds Player movement speed
    private const int GAP = 10;             // It holds Gap from window edges

    public double X { get; private set; }   // It holds X-coordinate of the player
    public double Y { get; private set; }   // It holds Y-coordinate of the player
    public bool Quit { get; private set; }  // It holds Whether the player has quit the game
    public int Lives { get; private set; }  //It holds  Number of remaining lives

    public int Width => _PlayerBitmap.Width;
    public int Height => _PlayerBitmap.Height;

   
    // It Initializes the player at the center of the game window.
    
    public Player(Window gameWindow)
    {
        _PlayerBitmap = new Bitmap("player", "Player.png");
        X = (gameWindow.Width - Width) / 2;
        Y = (gameWindow.Height - Height) / 2;
        Quit = false;
        Lives = 5; // Start with 5 lives
    }

    
    // It Draws the player image.
   
    public void Draw()
    {
        SplashKit.DrawBitmap(_PlayerBitmap, X, Y);
    }

  
    // It Handles arrow key movement and escape to quit.
    
    public void HandleInput()
    {
        if (SplashKit.KeyDown(KeyCode.LeftKey)) X -= SPEED;
        if (SplashKit.KeyDown(KeyCode.RightKey)) X += SPEED;
        if (SplashKit.KeyDown(KeyCode.UpKey)) Y -= SPEED;
        if (SplashKit.KeyDown(KeyCode.DownKey)) Y += SPEED;

        if (SplashKit.KeyTyped(KeyCode.EscapeKey)) Quit = true;
    }

   
    // It  Prevents the player from leaving the window area.
    
    public void StayOnWindow(Window gameWindow)
    {
        if (X < GAP) X = GAP;
        if (X > gameWindow.Width - Width - GAP) X = gameWindow.Width - Width - GAP;
        if (Y < GAP) Y = GAP;
        if (Y > gameWindow.Height - Height - GAP) Y = gameWindow.Height - Height - GAP;
    }

   
    // It  Checks if the player collides with a robot using circle collision.
    
    public bool CollidedWith(Robot other)
    {
        return _PlayerBitmap.CircleCollision(X, Y, other.CollisionCircle);
    }


    // It Reduces player lives by one.

    public void LoseLife()
    {
        Lives--;
    }

    // It Ends the game by setting Quit to true.
   
    public void QuitGame()
    {
        Quit = true;
    }

   
    // It Returns the center point of the player.
   
    public Point2D Center => new Point2D() { X = X + Width / 2, Y = Y + Height / 2 };
}
