namespace DungeonExplorer
{

    public class Player : Creature
    {
        public Inventory Inventory { get; } = new Inventory();
        public Player(string name, int health) : base(name, health) {}
        public override void Die()
        {
            Console.WriteLine("Your journey has ended.");
            Environment.Exit(0);
        }
    }
}