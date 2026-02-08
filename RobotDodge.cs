using SplashKitSDK;
using System;
using System.Collections.Generic;


// It Controls the Robot Dodge game: player, bullets, robots, and scoring.

public class RobotDodge
{
    private Player _Player;
    private Window _GameWindow;
    private List<Robot> _Robots;
    private List<Bullet> _Bullets;
    private SplashKitSDK.Timer _GameTimer;
    private int _Score;
    private Bitmap _Heart;

    public RobotDodge(Window gameWindow)
    {
        _GameWindow = gameWindow;
        _Player = new Player(_GameWindow);
        _Robots = new List<Robot>();
        _Bullets = new List<Bullet>();
        _Score = 0;

        try
        {
            _Heart = new Bitmap("heart", "Heart.png");
        }
        catch
        {
            Console.WriteLine("Heart.png not found. Falling back to text hearts.");
            _Heart = null;
        }

        _GameTimer = new SplashKitSDK.Timer("scoreTimer");
        _GameTimer.Start();
    }

    public bool Quit => _Player.Quit;

  
    // It Handles input and fires bullets on mouse click.
 
    public void HandleInput()
    {
        _Player.HandleInput();

        if (SplashKit.MouseClicked(MouseButton.LeftButton))
        {
            _Bullets.Add(new Bullet(_Player.Center.X, _Player.Center.Y, SplashKit.MousePosition()));
        }
    }

   
    //It  Updates the game state including player, bullets, robots, and collisions.
    
    public void Update()
    {
        _Player.StayOnWindow(_GameWindow);

        foreach (Robot r in _Robots) r.Update();
        foreach (Bullet b in _Bullets) b.Update();

        if (SplashKit.Rnd() < 0.02)
        {
            _Robots.Add(RandomRobot()); 
        }

        _Score = (int)(_GameTimer.Ticks / 1000);
        CheckCollisions();
    }

    
    // It Draws all visual elements to the screen.

    public void Draw()
    {
        _GameWindow.Clear(Color.White);

        foreach (Robot r in _Robots) r.Draw();
        foreach (Bullet b in _Bullets) b.Draw();
        _Player.Draw();

        // It Draws lives
        for (int i = 0; i < _Player.Lives; i++)
        {
            if (_Heart != null)
                SplashKit.DrawBitmap(_Heart, 10 + i * (_Heart.Width + 5), 10);
            else
                SplashKit.DrawText("♥", Color.Red, "Arial", 24, 10 + i * 30, 10);
        }

        //It Draws score
        SplashKit.DrawText($"Score: {_Score}", Color.Black, "Arial", 18, _GameWindow.Width - 120, 10);
        _GameWindow.Refresh(60);
    }

   
    // It Randomly returns one of three robot types.
    
    private Robot RandomRobot()
{
    if (SplashKit.Rnd() < 0.5)
        return new Boxy(_GameWindow, _Player);
    else
        return new Roundy(_GameWindow, _Player);
}


    
    // It Handles all collisions between player, bullets, and robots.
    
    private void CheckCollisions()
    {
        List<Robot> toRemove = new List<Robot>();
        List<Bullet> bulletsToRemove = new List<Bullet>();

        foreach (Robot r in _Robots)
        {
            if (_Player.CollidedWith(r))
            {
                _Player.LoseLife();
                toRemove.Add(r);
                if (_Player.Lives == 0)
                {
                    _Player.QuitGame();
                }
            }

            foreach (Bullet b in _Bullets)
            {
                if (SplashKit.CirclesIntersect(b.CollisionCircle, r.CollisionCircle))
                {
                    toRemove.Add(r);
                    bulletsToRemove.Add(b);
                    break;
                }
            }
        }

        foreach (Robot r in toRemove) _Robots.Remove(r);
        foreach (Bullet b in bulletsToRemove) _Bullets.Remove(b);
        _Bullets.RemoveAll(b => b.IsOffscreen(_GameWindow));
    }
}
