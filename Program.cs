using SplashKitSDK;


// This is the main entry point for the Robot Dodge game.
// It creates the window and starts the main game loop.

public class GameRunner
{
    public static void Main()
    {
        // It Creates the game window
        Window gameWindow = new Window("Robot Dodge", 800, 600);

        // It Starts the game
        RobotDodge game = new RobotDodge(gameWindow);

        //It Runs the main game loop
        while (!game.Quit && !gameWindow.CloseRequested)
        {
            SplashKit.ProcessEvents();  // Handle user input
            game.HandleInput();         // Handle keyboard and mouse input
            game.Update();              // Update player, bullets, and robots
            game.Draw();                // Draw everything on screen
        }

        // It Closes the game window when done
        gameWindow.Close();
    }
}
