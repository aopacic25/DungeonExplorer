using System;


namespace DungeonExplorer
{
    // The entry point of the application.
    internal class Program
    {
        // Starts the game.
        static void Main(string[] args)
        {
            
            // Create a new Game object.
            Game game = new Game();
            // Start the game.
            game.Start();
            Console.ReadKey();
        }
    }
}
