using System;


namespace DungeonExplorer
{

    // Main program entry point for the game.
    // Handles initialisation and test execution.
    internal class Program
    {
        // Application entry point
        static void Main(string[] args)
        {
            // Runs tests before starting game
            GameTest.RunTests();
            
            // Initialise and start the main game
            Game game = new Game();
            game.Start();
            // Keep console window open until key press
            Console.ReadKey();
        }
    }
}
