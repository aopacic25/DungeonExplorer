using System;


namespace DungeonExplorer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameTest.RunTests();
            return;
            
            Game game = new Game();
            game.Start();
            Console.ReadKey();
        }
    }
}
