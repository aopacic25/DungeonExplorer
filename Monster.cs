namespace DungeonExplorer
{

    // Class representing hostile NPC entity that can attack damageable targets.
    // Inherits from Creature class with combat-specific extensions.
    public class Monster : Creature
    {
        // Base damage dealt by attacks
        public int Damage { get; }

        // Creates a new monster instance.
        public Monster(string name, int health, int damage) : base(name, health)
        {
        Damage = damage;
        }

        // Default death behaviour
        public override void Die()
        {
            Console.WriteLine($"{Name} has been defeated!");
        }

        // When true, supresses combat messages for clean test display purpose.
        public bool SilentTestMode { get; set;}

        // Attacks a target implementing IDamageable interface.
        public void Attack(IDamageable target)
        {
            if (!SilentTestMode)
                Console.WriteLine($"{Name} attacks for {Damage} damage!");
                
            target.TakeDamage(Damage);
        }
    }
}