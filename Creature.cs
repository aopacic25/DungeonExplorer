namespace DungeonExplorer
{
    public abstract class Creature : IDamageable
    {
        public string Name { get; protected set; }
        public int Health { get; protected set; }

        protected Creature(string name, int health)
        {
            Name = name;
            Health = health;
        }

        public void Heal(int amount)
        {
            Health += amount;
        }

        public virtual void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0) Die();
        }

        public abstract void Die();
    }
}