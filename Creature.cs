namespace DungeonExplorer
{
    // Abstract base class that represents any living entity in the game.
    // Implements IDamageable interface for combat interactions.
    public abstract class Creature : IDamageable
    {
        // Displays name of the creature
        public string Name { get; protected set; }
        // Current health points
        public int Health { get; protected set; }
        // Maximum possible health points value
        public const int MaxHealth = 100;

        // Base constructor for creatures.
        protected Creature(string name, int health)
        {
            // Display name
            Name = name;
            // Starting health points value
            Health = health;
        }

        // Restores health points without going over MaxHealth.
        public void Heal(int amount)
        {
            Health = Math.Min(Health + amount, MaxHealth);
            Console.WriteLine($"Healed {amount} HP. Current health: {Health}/{MaxHealth}");
        }

        // Default damage handling logic which reduces health and triggers death if HP <= 0.
        public virtual void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0) Die();
        }

        // Abstract method which defines death behaviour and must be implemented by derived classes.
        public abstract void Die();
    }
}