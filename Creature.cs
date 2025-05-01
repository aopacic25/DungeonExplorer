namespace DungeonExplorer
{
    public abstract class Creature : IDamageable
    {
        public string Name { get; protected set; }
        public int Health { get; protected set; }
        public const int MaxHealth = 100;

        protected Creature(string name, int health)
        {
            Name = name;
            Health = health;
        }

        public void Heal(int amount)
        {
            Health = Math.Min(Health + amount, MaxHealth);
            Console.WriteLine($"Healed {amount} HP. Current health: {Health}/{MaxHealth}");
        }

        public virtual void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0) Die();
        }

        public abstract void Die();
    }
}