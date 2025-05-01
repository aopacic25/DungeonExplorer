namespace DungeonExplorer
{

    // Represents the player character, with inventory management capabilities.
    // Inherits from Creature class and implements unique death behaviour.
    public class Player : Creature
    {
        // The player's inventory system tracking collected items.
        public Inventory Inventory { get; } = new Inventory();
        // Creates a new player instance.
        public Player(string name, int health) : base(name, health) {}
        // Handles player character death by terminating the game session.
        public override void Die()
        {
            Console.WriteLine("Your journey has ended.");
            Environment.Exit(0);
        }
    }
}